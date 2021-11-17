import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EmployeeModel } from 'src/app/models/employee-model';
import { FailureModel } from 'src/app/models/failure-model';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmService } from 'src/app/services/confirm.service';
import { FailureService } from 'src/app/services/failure.service';

@Component({
  selector: 'app-failure-details',
  templateUrl: './failure-details.component.html',
  styleUrls: ['./failure-details.component.css']
})
export class FailureDetailsComponent implements OnInit {
  @Input() failure = new FailureModel();
  @Input() employees: EmployeeModel[] = [];
  @Output() deletedFailure = new EventEmitter();

  assign = {
    id: 0,
    comment: ''
  };

  assigning = false;
  finishing = false;
  finishComment = '';

  constructor(
    public authService: AuthService,
    private failureService: FailureService,
    private confirmSercice: ConfirmService) { }

  ngOnInit(): void {
    
  }

  assignEmployee(): void {
    this.assigning = true;
    this.assign.id = 0;
    this.assign.comment = '';
  }

  saveAssign(): void {
    const f: FailureModel = JSON.parse(JSON.stringify(this.failure));
    f.assignedId = Number(this.assign.id);
    f.assignComment = this.assign.comment;

    this.failureService.assignFailure(f).subscribe(
      result => {
        this.failure = new FailureModel(result);
        this.assigning = false;
      },
      error => console.log(error)
    );
  }

  modifyAssign(): void {
    this.assigning = true;
    this.assign.id = Number(this.failure.assignedId);
    this.assign.comment = this.failure.assignComment;
  }

  workStarted(): void {
    this.failureService.startFailure(this.failure).subscribe(
      result => this.failure = new FailureModel(result),
      error => console.log(error)
    );
  }

  workFinished(): void {
    const f: FailureModel = JSON.parse(JSON.stringify(this.failure));
    f.finishComment = this.finishComment;
    this.failureService.finishFailure(f).subscribe(
      result => {
        this.failure = new FailureModel(result);
        this.finishing = false;
      },
      error => console.log(error)
    );
  }

  workCheck(): void {
    this.confirmSercice.confirm('Biztos szeretné <b>lezárni</b> a munkalapot?').subscribe(
      result => {
        if( result ) {
          this.failureService.checkFailure(this.failure).subscribe(
            response => this.failure = new FailureModel(response),
            error => console.log(error)
          );
        }
      },
      error => console.log(error)
    )
  }

  deleteFailure(): void {
    this.confirmSercice.confirm('<span class="text-danger">Biztos szeretné törölni?</span>').subscribe(
      result => {
        if( result ) {
          this.failureService.deleteFailure(this.failure).subscribe(
            response => this.deletedFailure.emit(this.failure),
            error => console.log(error)
          );
        }
      },
      error => console.log(error)
    )
  }
}
