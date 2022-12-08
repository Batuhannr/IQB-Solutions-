import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { refreshBegin } from '@syncfusion/ej2-angular-richtexteditor';
import { Observable, finalize } from 'rxjs';
import { Course } from 'src/app/Models/Course';
import { ExamResult } from 'src/app/Models/ExamResult';
import { ResultModel } from 'src/app/Models/ResultModel';
import { Student } from 'src/app/Models/Student';
import { ApiService } from 'src/app/Services/apiService';

@Component({
  selector: 'app-add-exam-result',
  templateUrl: './add-exam-result.component.html',
  styleUrls: ['./add-exam-result.component.css']
})
export class AddExamResultComponent implements OnInit {
  @Input() post: any;
  StudentId!: number;
  ExamResult: ExamResult = new ExamResult();
  courses: Course[] = [];
  student : Student = new Student();
  toppings = new FormControl('');
  id!: number;
  constructor(
    public apiServis: ApiService,
    private router: Router,
    private route: ActivatedRoute
  ) { }
  ngOnInit(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.StudentId = this.route.snapshot.params['id'];
    this.GetCourse();
    this.GetStudent();
    
  }
  
  GetCourse() {
    this.apiServis.getCourse().
      subscribe((result: ResultModel) => {
        this.courses = result.ResultObject as Course[];
      });
  }
  GetStudent() {
    this.apiServis.getStudentById(this.StudentId).
      subscribe((result: ResultModel) => {
        this.student = result.ResultObject as Student;
      });
  }
  btnSaveClick( courseId: number, score: string) {
    this.ExamResult.StudentId = this.StudentId;
    this.ExamResult.CourseId = courseId;
    this.ExamResult.Score =  score as unknown as number;
    this.apiServis.addExamResult(this.ExamResult).subscribe((s: ResultModel) => {
      alert(s.ResultMessages)
      if(s.Result){
        window.location.reload();
      }
    })
  }
}