import { Component, OnInit } from '@angular/core';
import {LessonService} from "../../services/lesson/lesson.service";
import {AuthService} from "../../services/auth/auth.service";
import {UserModel} from "../../models/userModel";
import {Observable} from "rxjs";
import {WorkWeekDTO} from "../../models/workWeekDTO";
import {Router} from "@angular/router";

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {

  schedule$: Observable<WorkWeekDTO[]>;

  constructor(
    private readonly lessonService: LessonService,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    let user: UserModel | null = authService.parseJwt();
    let userId: number | undefined = undefined;
    if (user && user.role !== 'administrator') {
      userId = user.id;
    }
    else if (user?.role === 'administrator') {
      this.router.navigateByUrl('/admin');
    }
    this.schedule$ = lessonService.getSchedule(userId);
  }
}
