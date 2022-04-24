import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {WorkWeekDTO} from "../../models/workWeekDTO";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  private readonly lessonPath: string;

  constructor(
    private http: HttpClient) {
    this.lessonPath = 'lesson/';
  }

  getSchedule(userId: number | undefined): Observable<WorkWeekDTO[]> {
    let path: string = environment.apiPath + this.lessonPath + 'schedule/';
    if (userId !== undefined) {
      path += userId.toString();
    }
    return this.http.get<WorkWeekDTO[]>(path);
  }
}
