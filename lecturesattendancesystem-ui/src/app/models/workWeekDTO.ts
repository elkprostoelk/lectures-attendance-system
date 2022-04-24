import {CompactLessonDTO} from "./compactLessonDTO";

export interface WorkWeekDTO {
  lessonTime: Date,
  mondayLesson: CompactLessonDTO,
  tuesdayLesson: CompactLessonDTO,
  wednesdayLesson: CompactLessonDTO,
  thursdayLesson: CompactLessonDTO,
  fridayLesson: CompactLessonDTO
}
