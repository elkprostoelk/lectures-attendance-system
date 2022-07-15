import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {WorkWeekDTO} from "../../models/workWeekDTO";
import {environment} from "../../../environments/environment";
import {LessonDTO} from "../../models/lessonDTO";
import {LessonWithStudentsDTO} from "../../models/lessonWithStudentsDTO";

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  private readonly lessonPath: string;

  constructor(
    private http: HttpClient) {
    this.lessonPath = 'lesson/';
  }

  getSchedule(userId: number | undefined, startDate: Date, endDate: Date): Observable<WorkWeekDTO[]> {
    let path: string = `${environment.apiPath}${this.lessonPath}schedule/${startDate}/${endDate}/`;
    if (userId !== undefined) {
      path += userId.toString();
    }
    return this.http.get<WorkWeekDTO[]>(path);
  }

  getLessons(): Observable<LessonDTO[]> {
    return this.http.get<LessonDTO[]>(environment.apiPath + this.lessonPath);
  }

  deleteLesson(id: number): Observable<void> {
    return this.http.delete<void>(environment.apiPath + this.lessonPath + id);
  }

  createLesson(value: any): Observable<void> {
    return this.http.post<void>(environment.apiPath + this.lessonPath, {
      name: value.name,
      scheduledOn: value.scheduledOn,
      lessonType: value.lessonType,
      participantIds: value.participantIds
    });
  }

  getLesson(lessonId: number): Observable<LessonWithStudentsDTO> {
    return this.http.get<LessonWithStudentsDTO>(environment.apiPath + this.lessonPath + lessonId);
  }
}
