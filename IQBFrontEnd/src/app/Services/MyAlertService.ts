import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MyAlertDialogComponent } from '../Components/Dialogs/my-alert-dialog/my-alert-dialog.component';
import { ResultModel } from '../Models/ResultModel';


@Injectable({
  providedIn: 'root'
})
export class MyAlertService {
  private alertDialogRef?: MatDialogRef<MyAlertDialogComponent>;
  constructor(
    private matDialog: MatDialog
  ) { }

  AlertUygula(s: ResultModel) {
    var header = "";
    if (s.Result) {
        header = "Success";
    } else {
        header = "Error";
    }

    this.alertDialogRef = this.matDialog.open(MyAlertDialogComponent, {
      width: '300px'
    });
    this.alertDialogRef.componentInstance.dialogHeader = header;
    this.alertDialogRef.componentInstance.dialogMessage = s.ResultMessages;
    this.alertDialogRef.componentInstance.dialogOperation = s.Result;

    this.alertDialogRef.afterClosed().subscribe(d => {
      this.alertDialogRef != null;
    });
  }

}