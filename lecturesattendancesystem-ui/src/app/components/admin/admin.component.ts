import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {UserForAdminPanelDto} from "../../models/userForAdminPanelDTO";
import {AuthService} from "../../services/auth/auth.service";
import {Observable} from "rxjs";
import {RoleDto} from "../../models/roleDTO";
import {UserService} from "../../services/user/user.service";
import {RoleService} from "../../services/role/role.service";
import {LessonDTO} from "../../models/lessonDTO";
import {LessonService} from "../../services/lesson/lesson.service";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  createUserForm: FormGroup;
  userDtos?: UserForAdminPanelDto[];
  roles$: Observable<RoleDto[]>;
  lessonDtos$: Observable<LessonDTO[]>;

  constructor(
    private readonly authService: AuthService,
    private readonly builder: FormBuilder,
    private readonly userService: UserService,
    private readonly roleService: RoleService,
    private readonly lessonService: LessonService
  ) {
    this.createUserForm = this.builder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      firstName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(20)]],
      roleId: ['', Validators.required]
    });
    this.roles$ = this.roleService.getAllRoles();
    this.userService.getAllUsers()
      .subscribe(data => {
        this.userDtos = data;
        this.userDtos.sort((a, b) =>
          a.fullName.localeCompare(b.fullName));
      });
    this.lessonDtos$ = lessonService.getLessons();
  }

  searchUser(input: HTMLInputElement): void {
    let searchString: string = input.value;
    if (searchString !== '') {
      this.userService.getAllUsers()
        .subscribe(data => {
          this.userDtos = data;
          this.userDtos.sort((a, b) =>
            a.fullName.localeCompare(b.fullName));
          if (searchString.match(/^[a-zA-Z0-9]*$/i)) {
            this.userDtos = this.userDtos?.filter(u =>
              u.name.toLowerCase().includes(searchString.toLowerCase()));
          }
          else {
            this.userDtos = this.userDtos?.filter(u =>
              u.fullName.toLowerCase().includes(searchString.toLowerCase()));
          }
          this.userDtos?.sort((a, b) =>
            a.fullName.localeCompare(b.fullName));
        });
    }
  }

  resetResults(): void {
    this.userService.getAllUsers()
      .subscribe(data => {
        this.userDtos = data;
        this.userDtos.sort((a, b) =>
          a.fullName.localeCompare(b.fullName));
      });
  }

  createUser(value: any): void {
    this.userService.createUser(value)
      .subscribe(() => {
        window.location.reload();
      });
  }

  deleteUser(userId: number): void {
    if (confirm("Are you sure to delete this user? It couldn't be reverted!")) {
      this.userService.deleteUser(userId)
        .subscribe();
    }
  }

  isMe(username: string): boolean {
    return this.authService.parseJwt()?.name === username;
  }

  deleteLesson(id: number) {
    if (confirm("Are you sure to delete this lesson? It couldn't be reverted!")) {
      this.lessonService.deleteLesson(id)
        .subscribe(() => {
          window.location.reload();
        });
    }
  }
}
