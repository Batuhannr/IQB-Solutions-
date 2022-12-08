import { Course } from "./Course";
import { Student } from "./Student";

export class ExamResult{
    Id!: number;
    StudentId!: number;
    CourseId!: number;
    Score!: number;
    Students?: Student;
    Courses?: Course;
}