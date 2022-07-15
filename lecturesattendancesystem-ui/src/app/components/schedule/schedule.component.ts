import { Component } from '@angular/core';
import {LessonService} from "../../services/lesson/lesson.service";
import {AuthService} from "../../services/auth/auth.service";
import {UserModel} from "../../models/userModel";
import {Observable} from "rxjs";
import {WorkWeekDTO} from "../../models/workWeekDTO";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
  providers: [DatePipe]
})
export class ScheduleComponent {

  scheduleDatesForm: FormGroup;
  schedule$: Observable<WorkWeekDTO[]>;
  today: Date;

  constructor(
    private readonly lessonService: LessonService,
    private readonly authService: AuthService,
    private readonly router: Router,
    private readonly builder: FormBuilder,
    private readonly datePipe: DatePipe
  ) {
    this.today = new Date();
    this.scheduleDatesForm = builder.group({
      startDate: [datePipe.transform(this.countStartDate().toISOString(), 'yyyy-MM-dd'), Validators.required],
      endDate: [datePipe.transform(this.countEndDate().toISOString(), 'yyyy-MM-dd'), Validators.required]
    });
    let user: UserModel | null = authService.parseJwt();
    let userId: number | undefined = undefined;
    if (user && user.role !== 'administrator') {
      userId = user.id;
    }
    else if (user?.role === 'administrator') {
      this.router.navigateByUrl('/admin');
    }
    let value = this.scheduleDatesForm.value;
    this.schedule$ = lessonService.getSchedule(userId, value.startDate, value.endDate);
  }

  getSchedule(value: any): void {
    let user: UserModel | null = this.authService.parseJwt();
    if (user) {
      this.schedule$ = this.lessonService.getSchedule(user.id, value.startDate, value.endDate);
    }
  }

  private countStartDate(): Date {
    let date = new Date();
    if (date.getDay() === 6) {
      date.setDate(date.getDate() + 2);
    }
    if (date.getDay() === 7) {
      date.setDate(date.getDate() + 1);
    }
    while (date.getDay() !== 1) {
      date.setDate(date.getDate() - 1);
    }
    return date;
  }

  private countEndDate(): Date {
    let date = new Date();
    if (date.getDay() === 6) {
      date.setDate(date.getDate() + 2);
    }
    if (date.getDay() === 7) {
      date.setDate(date.getDate() + 1);
    }
    while (date.getDay() !== 5) {
      date.setDate(date.getDate() + 1);
    }
    return date;
  }
}
