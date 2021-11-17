import { Component,  OnInit, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {
  confirmHTML = '';
  afterClosed = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  cancelClick(): void {
    this.afterClosed.emit(false);
  }

  okClick(): void {
    this.afterClosed.emit(true);
  }
}
