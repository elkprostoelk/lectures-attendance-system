import {ShortUserDTO} from "./shortUserDTO";

export interface LessonWithStudentsDTO {
  id: number,
  name: string,
  lessonType: string,
  teacher: string,
  scheduledOn: Date,
  participants: ShortUserDTO[]
}
