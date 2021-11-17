import { Component, OnInit } from '@angular/core';
import { EmployeeModel } from 'src/app/models/employee-model';
import { FailureModel } from 'src/app/models/failure-model';
import { EmployeeService } from 'src/app/services/employee.service';
import { FailureService } from 'src/app/services/failure.service';

@Component({
  selector: 'app-failure-list',
  templateUrl: './failure-list.component.html',
  styleUrls: ['./failure-list.component.css']
})
export class FailureListComponent implements OnInit {

  failures: FailureModel[] = [];
  employees: EmployeeModel[] = [];

  constructor(
    private failureService: FailureService,
    private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.failureService.getFailures().subscribe(
      result => {
        this.failures = [];
        result.forEach( r => this.failures.push(  new FailureModel(r))  );
      },
      error => {
        console.log(error);
      }
    );

    this.employeeService.getEmployees(false).subscribe(
      result => this.employees = result,
      error => console.log(error)
    );
  }

  deletedFailure(failure: FailureModel): void {
    // const index = this.failures.findIndex(f => f.id == failure.id);
    const index = this.failures.indexOf(failure);
    this.failures.splice(index, 1);
  }

}
