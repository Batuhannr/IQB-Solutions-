import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/Components/Dialogs/confirm-dialog/confirm-dialog.component';
import { StudentDialogComponent } from 'src/app/Components/Dialogs/student-dialog/student-dialog.component';
import { StudentInfoDialogComponent } from 'src/app/Components/Dialogs/student-info-dialog/student-info-dialog.component';
import { ExamResult } from 'src/app/Models/ExamResult';
import { ResultModel } from 'src/app/Models/ResultModel';
import { Student } from 'src/app/Models/Student';
import { StudentAverage } from 'src/app/Models/StudentAverage';
import { ApiService } from 'src/app/Services/apiService';
import { MyAlertService } from 'src/app/Services/MyAlertService';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  dialogRef: any = MatDialogRef<StudentDialogComponent>;
  dialogRefInfo: any = MatDialogRef<StudentInfoDialogComponent>;
  confirmDialogRef: any = MatDialogRef<ConfirmDialogComponent>;
  // result: ResultModel = new ResultModel();
  Students: Student[] = [];
  Average: StudentAverage= new StudentAverage();
  dataSource: any;
  StudentExamResults : ExamResult[]=[];
  displayedColumns = ['Id', 'Email', 'FullName', 'GsmNumber', 'Number', 'operations']
  @ViewChild(MatSort) sort: MatSort = new MatSort();
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  constructor(public apiServis: ApiService, public matDialog: MatDialog, private router: Router, public alert: MyAlertService) { }

  ngOnInit(): void {
    this.GetStudent();
  }
  StudentFilter(e: any) {
    var deger = e.target.value;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.dataSource.filter = deger.trim().toLowerCase();
    console.log(this.dataSource.filter)
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }

  }
  navigateToPost(data: Student) {
    this.router.navigate([`AddExamResult/${data.Id}`])
  }
  GetStudent() {
    this.apiServis.getStudent().
      subscribe((result: ResultModel) => {
        this.Students = result.ResultObject as Student[];
        this.dataSource = new MatTableDataSource(this.Students);
        this.dataSource.paginator = this.paginator;
        console.log(this.Students)
      });
  }
  StudentAdd() {
    var newData: Student = new Student();
    this.dialogRef = this.matDialog.open(StudentDialogComponent, {
      width: '300px',
      data: {
        data: newData,
        operation: 'add'
      }
    });
    this.dialogRef.afterClosed().subscribe((d: any) => {
      if (d) {
        this.apiServis.addStudent(d).subscribe((s: ResultModel) => {
          this.GetStudent();
        })
      }
    })
  }

  StudentUpdate(data: Student) {
    this.dialogRef = this.matDialog.open(StudentDialogComponent, {
      width: '400px',
      data: {
        data: data,
        DialogHeader: 'update'
      }
    });
    this.dialogRef.afterClosed().subscribe((d: any) => {
      if (d) {

        data.FullName = d.FullName;
        data.GsmNumber = d.GsmNumber;
        data.Email = d.Email;
        data.Number = d.Number;
        this.apiServis.StudentUpdate(data, d.Id).subscribe((s: ResultModel) => {
          this.alert.AlertUygula(s);
          if (s.Result) {
            this.GetStudent();
          }
        });
      }
    })

  }

  StudentAverage(Id : number){
    this.apiServis.StudentAverage(Id).subscribe((d:ResultModel)=>{
      if(d.Result){
        const Average = d.ResultObject as StudentAverage;
        this.StudentExamResult(Id,Average);
      }
    })
  }
  StudentInfo(data: Student) {
    this.StudentAverage(data.Id);
  }
  StudentExamResult(Id :number,StudentAverage: StudentAverage){
    this.apiServis.getExamResultByStudentId(Id).subscribe((d:ResultModel)=>{
      if(d.Result){
        const StudentExamResults = d.ResultObject as ExamResult[];
        this.InfoDialogOpen(StudentAverage,StudentExamResults);
      } 
    })
  }

  InfoDialogOpen(data : StudentAverage, examResult : ExamResult[]){
    this.dialogRefInfo = this.matDialog.open(StudentInfoDialogComponent, {
      width: '400px',
      data: {
        data: data,
        examResult : examResult,
        DialogHeader: 'info'
      }
    });
  }
  StudentRemove(data: Student) {
    this.confirmDialogRef = this.matDialog.open(ConfirmDialogComponent, {
      width: '500px'
    });
    this.confirmDialogRef.componentInstance.dialogMesaj = data.FullName + " Removed Are You Sure ?"
    this.confirmDialogRef.afterClosed().subscribe((d: any) => {
      if (d) {

        this.apiServis.StudentRemove(data.Id as number).subscribe((s: ResultModel) => {
          this.alert.AlertUygula(s);
          if (s.Result) {
            this.GetStudent();
          }
        })
      }
    })
  }
}