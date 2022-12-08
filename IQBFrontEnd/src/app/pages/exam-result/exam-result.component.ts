import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmDialogComponent } from 'src/app/Components/Dialogs/confirm-dialog/confirm-dialog.component';
import { AllList } from 'src/app/Models/AllList';
import { ExamResult } from 'src/app/Models/ExamResult';
import { ResultModel } from 'src/app/Models/ResultModel';
import { ApiService } from 'src/app/Services/apiService';
import { MyAlertService } from 'src/app/Services/MyAlertService';

@Component({
  selector: 'app-exam-result',
  templateUrl: './exam-result.component.html',
  styleUrls: ['./exam-result.component.css']
})

export class ExamResultComponent implements OnInit {
  confirmDialogRef: any = MatDialogRef<ConfirmDialogComponent>;
  // result: ResultModel = new ResultModel();
  ExamResults: ExamResult[] = [];
  AllLists: AllList[] = [];

  dataSource!: MatTableDataSource<any>;
  displayedColumns = ['ExamResultId', 'StudentName', 'StudentNumber', 'StudentMail', 'StudentGs', 'CourseName', 'Score', 'operations']
  @ViewChild(MatSort) sort: MatSort = new MatSort();
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  constructor(public apiServis: ApiService, public matDialog: MatDialog, public alert: MyAlertService) { }

  ngOnInit(): void {
    this.GetExamResult();
  }
  ExamResultFilter(e: any) {
    var deger = e.target.value;
    this.dataSource.filter = deger.trim().toLowerCase();
    console.log(this.dataSource)
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }

  }
  GetExamResult() {
    this.apiServis.getExamResult().
      subscribe((result: ResultModel) => {
        this.ExamResults = result.ResultObject as ExamResult[];
        this.ExamResults.forEach(element => {
          const newAllist = new AllList();
          newAllist.CourseName = element.Courses?.Name;
          newAllist.ExamResultId = element.Id;
          newAllist.Score = element.Score;
          newAllist.StudentGs = element.Students?.GsmNumber;
          newAllist.StudentMail = element.Students?.Email;
          newAllist.StudentName = element.Students?.FullName;
          newAllist.StudentNumber = element.Students?.Number;
          this.AllLists.push(newAllist);
        });
        this.dataSource = new MatTableDataSource(this.AllLists);
        this.dataSource.paginator = this.paginator;
      });
  }

  ExamResultRemove(data: ExamResult) {
    this.confirmDialogRef = this.matDialog.open(ConfirmDialogComponent, {
      width: '500px'
    });
    this.confirmDialogRef.componentInstance.dialogMesaj = data.Students!.FullName + data.Courses!.Name + " Removed Are You Sure ?"
    this.confirmDialogRef.afterClosed().subscribe((d: any) => {
      if (d) {

        this.apiServis.ExamResultRemove(data.Id as number).subscribe((s: ResultModel) => {
          this.alert.AlertUygula(s);
          if (s.Result) {
            this.GetExamResult();
          }
        })
      }
    })
  }
  calculateStyles(score: number) {
    if (score < 3) {
      return "background-color: green; color: white;";
    } else {
      return "background-color: white; color: black;";
    }
  }
}