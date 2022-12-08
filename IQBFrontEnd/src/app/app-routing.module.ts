import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddExamResultComponent } from './pages/add-exam-result/add-exam-result.component';
import { CourseComponent } from './pages/course/course.component';
import { ExamResultComponent } from './pages/exam-result/exam-result.component';
import { StudentsComponent } from './pages/students/students.component';

const routes: Routes = [
  { path: "", component: StudentsComponent },
  { path: "Student", component: StudentsComponent },
  { path: "Course", component: CourseComponent },
  { path: "ExamResult", component: ExamResultComponent },
  { path: "AddExamResult/:id", component: AddExamResultComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
