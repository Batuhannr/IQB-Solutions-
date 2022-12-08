import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-my-alert-dialog',
  templateUrl: './my-alert-dialog.component.html',
  styleUrls: ['./my-alert-dialog.component.css']
})
export class MyAlertDialogComponent implements OnInit {
  dialogHeader: string = "";
  dialogMessage?: string[];
  dialogOperation?: boolean;
  constructor(
    public dialogRef: MatDialogRef<MyAlertDialogComponent>
  ) { }

  ngOnInit() {
  }
}
