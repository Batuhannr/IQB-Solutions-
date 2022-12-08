import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Student } from 'src/app/Models/Student';
import { ApiService } from 'src/app/Services/apiService';

@Component({
  selector: 'app-student-dialog',
  templateUrl: './student-dialog.component.html',
  styleUrls: ['./student-dialog.component.css']
})
export class StudentDialogComponent implements OnInit {

  DialogHeader: string = "";
  newData : Student = new Student();
  operation : string = "";
  frm !: FormGroup ;
  constructor(
    public dialogRef : MatDialogRef<StudentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild : FormBuilder,
    public apiService: ApiService,
  ) {

    this.operation = data.operation;
    this.newData = data.data;
    if (this.operation == 'add') {
      this.DialogHeader = 'Add new Student'
    }
    else if (this.operation == 'update'){
      this.DialogHeader ='Update Student' 
    }
    this.frm = this.FormCreate();
  } 

  ngOnInit(): void {
  }

  FormCreate(){
    return this.frmBuild.group({
      Id : [this.newData.Id],
      Email: [this.newData.Email],
      FullName: [this.newData.FullName],
      GsmNumber: [this.newData.GsmNumber],
      Number: [this.newData.Number],

    })
  }
}
