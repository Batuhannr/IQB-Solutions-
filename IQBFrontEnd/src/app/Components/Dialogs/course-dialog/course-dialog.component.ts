import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Course } from 'src/app/Models/Course';
import { ApiService } from 'src/app/Services/apiService';

@Component({
  selector: 'app-course-dialog',
  templateUrl: './course-dialog.component.html',
  styleUrls: ['./course-dialog.component.css']
})
export class CourseDialogComponent implements OnInit {

  DialogHeader: string = "";
  newData : Course = new Course();
  operation : string = "";
  frm !: FormGroup ;
  constructor(
    public dialogRef : MatDialogRef<CourseDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild : FormBuilder,
    public apiService: ApiService,
  ) {

    this.operation = data.operation;
    this.newData = data.data;
    if (this.operation == 'add') {
      this.DialogHeader = 'Add new Course'
    }
    else if (this.operation == 'update'){
      this.DialogHeader ='Update Course' 
    }
    this.frm = this.FormCreate();
  } 

  ngOnInit(): void {
  }

  FormCreate(){
    return this.frmBuild.group({
      Id : [this.newData.Id],
      Name: [this.newData.Name],
    })
  }
}
