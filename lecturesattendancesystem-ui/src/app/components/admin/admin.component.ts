import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {UserForAdminPanelDto} from "../../models/userForAdminPanelDTO";
import {AuthService} from "../../services/auth/auth.service";
import {Observable} from "rxjs";
import {RoleDto} from "../../models/roleDTO";
import {UserService} from "../../services/user/user.service";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  createUserForm: FormGroup;
  userDtos?: UserForAdminPanelDto[];
  roles$: Observable<RoleDto[]>;

  constructor(
    private authService: AuthService,
    private builder: FormBuilder,
    private userService: UserService
  ) {
    this.createUserForm = this.builder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(20)]],
      roleId: ['', Validators.required]
    });
    this.roles$ = new Observable<RoleDto[]>();
    this.userService.getAllUsers()
      .subscribe(data => {
        this.userDtos = data;
        this.userDtos.sort((a, b) =>
          a.fullName.localeCompare(b.fullName));
      });
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
      .subscribe();
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
}
