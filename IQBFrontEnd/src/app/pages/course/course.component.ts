import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ResultModel } from 'src/app/Models/ResultModel';
import { Course } from 'src/app/Models/Course';
import { ApiService } from 'src/app/Services/apiService';
import { MyAlertService } from 'src/app/Services/MyAlertService';
import { CourseDialogComponent } from 'src/app/Components/Dialogs/course-dialog/course-dialog.component';
import { ConfirmDialogComponent } from 'src/app/Components/Dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {
  dialogRef : any = MatDialogRef<CourseDialogComponent> ;
  confirmDialogRef: any = MatDialogRef<ConfirmDialogComponent>;
  // result: ResultModel = new ResultModel();
  Courses : Course[] = [];
  dataSource: any;
  displayedColumns =['Id', 'Name','operations']
  @ViewChild(MatSort) sort: MatSort = new MatSort();
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  constructor(public apiServis : ApiService,public matDialog : MatDialog, public alert : MyAlertService) { }

  ngOnInit(): void {
    this.GetCourse(); 
  }
  CourseFilter(e: any){
    var deger = e.target.value;
    this.dataSource.filter = deger.trim().toLowerCase();
    console.log(this.dataSource.filter)
    if(this.dataSource.paginator){
      this.dataSource.paginator.firstPage();
    }
    
  }
  GetCourse() {
    this.apiServis.getCourse().
      subscribe((result: ResultModel) => {
        this.Courses = result.ResultObject as Course[];
        this.dataSource = new MatTableDataSource( this.Courses);
      this.dataSource.paginator = this.paginator;
      console.log(this.Courses)
      });
  }
  CourseAdd(){
    var newData : Course = new Course();
    this.dialogRef = this.matDialog.open(CourseDialogComponent,{
      width: '300px',
      data:{
        data:newData,
        operation: 'add'
      }
    });
    this.dialogRef.afterClosed().subscribe((d:any)=>{
      if (d) {
        this.apiServis.addCourse(d).subscribe((s:ResultModel)=>{
          this.GetCourse();
        })
      }
    })
  }

  CourseUpdate(data:Course){
    this.dialogRef= this.matDialog.open(CourseDialogComponent,{
      width : '400px',
      data:{
        data:data,
        DialogHeader: 'update'
      }
    });
    this.dialogRef.afterClosed().subscribe((d : any)=>{
      if(d){

         data.Name = d.Name;
        this.apiServis.CourseUpdate(data,d.Id).subscribe((s:ResultModel)=>{
          this.alert.AlertUygula(s);
          if (s.Result){
            this.GetCourse();
          }
        });
      }
    })

  }
  CourseRemove(data: Course){
    this.confirmDialogRef = this.matDialog.open(ConfirmDialogComponent,{
      width : '500px'
    });
    this.confirmDialogRef.componentInstance.dialogMesaj=data.Name + " Removed Are You Sure ?"
    this.confirmDialogRef.afterClosed().subscribe((d:any)=>{
      if(d){
        
        this.apiServis.CourseRemove(data.Id as number).subscribe((s:ResultModel) =>{
          this.alert.AlertUygula(s);
          if (s.Result){
            this.GetCourse();
          }
        })
      }
    })
  }
}