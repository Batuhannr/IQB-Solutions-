import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainNavComponent } from './Components/main-nav/main-nav.component';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FormsModule } from '@angular/forms';
import { StudentsComponent } from './pages/students/students.component';
import { StudentDialogComponent } from './Components/Dialogs/student-dialog/student-dialog.component';
import { MyAlertDialogComponent } from './Components/Dialogs/my-alert-dialog/my-alert-dialog.component';
import { CourseComponent } from './pages/course/course.component';
import { ExamResultComponent } from './pages/exam-result/exam-result.component';
import { CourseDialogComponent } from './Components/Dialogs/course-dialog/course-dialog.component';
import { ConfirmDialogComponent } from './Components/Dialogs/confirm-dialog/confirm-dialog.component';
import { AddExamResultComponent } from './pages/add-exam-result/add-exam-result.component';
import { StudentInfoDialogComponent } from './Components/Dialogs/student-info-dialog/student-info-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    StudentsComponent,
    StudentDialogComponent,
    MyAlertDialogComponent,
    CourseComponent,
    ExamResultComponent,
    CourseDialogComponent,
    ConfirmDialogComponent,
    AddExamResultComponent,
    StudentInfoDialogComponent,
    

  ],
  imports: [
    MaterialModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    EditorModule,
    FormsModule,
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
