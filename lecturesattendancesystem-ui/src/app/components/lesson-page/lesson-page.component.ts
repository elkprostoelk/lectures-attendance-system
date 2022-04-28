import { Component } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {LessonWithStudentsDTO} from "../../models/lessonWithStudentsDTO";
import {LessonService} from "../../services/lesson/lesson.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-lesson-page',
  templateUrl: './lesson-page.component.html',
  styleUrls: ['./lesson-page.component.css']
})
export class LessonPageComponent {

  lesson$: Observable<LessonWithStudentsDTO>

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly lessonService: LessonService
  ) {
    this.lesson$ = new Observable<LessonWithStudentsDTO>();
    activatedRoute.params.subscribe(params => {
      let lessonId: number = params['id'];
      this.lesson$ = lessonService.getLesson(lessonId);
  });
}

}
