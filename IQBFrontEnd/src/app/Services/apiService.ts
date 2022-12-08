import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResultModel } from '../Models/ResultModel';
import { Course } from '../Models/Course';
import { Student } from '../Models/Student';
import { ExamResult } from '../Models/ExamResult';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public url = environment.publicUrl;

  constructor(
    private http: HttpClient
  ) { }

  public getStudent(): Observable<ResultModel> {
    return this.http.get<ResultModel>(this.url + "Student/get");
  }
  public getStudentById(Id : number): Observable<ResultModel> {
    return this.http.get<ResultModel>(this.url + "Student/get/"+Id);
  }
  public addStudent(student: Student): Observable<ResultModel> {
    return this.http.post<ResultModel>(this.url + "Student/addStudent", student);
  }
  public StudentUpdate(student: Student, studentId: number) {
    return this.http.put(this.url + "Student/updateStudent/" + studentId, student);
  }
  public StudentRemove(studentId: number) {
    return this.http.delete(this.url + "Student/removeStudent/" + studentId);
  }
  public getCourse(): Observable<ResultModel> {
    return this.http.get<ResultModel>(this.url + "Course/get");
  }
  public addCourse(Course: Course): Observable<ResultModel> {
    return this.http.post<ResultModel>(this.url + "Course/addCourse", Course);
  }
  public CourseUpdate(Course: Course, CourseId: number) {
    return this.http.put(this.url + "Course/updateCourse/" + CourseId, Course);
  }
  public CourseRemove(CourseId: number) {
    return this.http.delete(this.url + "Course/removeCourse/" + CourseId);
  }
  public getExamResult(): Observable<ResultModel> {
    return this.http.get<ResultModel>(this.url + "ExamResult/get");
  }
  public getExamResultByStudentId(Id : number): Observable<ResultModel> {
    return this.http.get<ResultModel>(this.url + "ExamResult/get/"+Id);
  }
  public addExamResult(ExamResult: ExamResult): Observable<ResultModel> {
    return this.http.post<ResultModel>(this.url + "ExamResult/addExamResult", ExamResult);
  }
  public ExamResultUpdate(ExamResult: ExamResult, ExamResultId: number) {
    return this.http.put(this.url + "ExamResult/updateExamResult/" + ExamResultId, ExamResult);
  }
  public ExamResultRemove(ExamResultId: number) {
    return this.http.delete(this.url + "ExamResult/removeExamResult/" + ExamResultId);
  }
  public StudentAverage(Id: number){
    return this.http.get(this.url + "ExamResult/studentAverage/"+ Id);
  }

}