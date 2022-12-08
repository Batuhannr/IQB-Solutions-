import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Course } from 'src/app/Models/Course';
import { ExamResult } from 'src/app/Models/ExamResult';
import { StudentAverage } from 'src/app/Models/StudentAverage';
import { ApiService } from 'src/app/Services/apiService';
import { CourseDialogComponent } from '../course-dialog/course-dialog.component';

@Component({
  selector: 'app-student-info-dialog',
  templateUrl: './student-info-dialog.component.html',
  styleUrls: ['./student-info-dialog.component.css']
})
export class StudentInfoDialogComponent implements OnInit {

  DialogHeader: string = "";
  newData : StudentAverage = new StudentAverage();
  operation : string = "";
  frm !: FormGroup ;
  examResult ?: ExamResult[];
  dataSource: any;
  aver : string = "";
  displayedColumns =[ 'Name','score']
  @ViewChild(MatSort) sort: MatSort = new MatSort();
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  constructor(
    public dialogRef : MatDialogRef<CourseDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild : FormBuilder,
    public apiService: ApiService,
  ) {

    this.operation = data.operation;
    this.newData = data.data;
    this.examResult = data.examResult;
    if (this.operation == 'info') {
      this.DialogHeader = this.newData.Students?.FullName + " Info";
    }
  } 

  ngOnInit(): void {
    this.dataSource = this.examResult;
    this.aver = this.newData.average.toFixed(1);
  }
  
}
