import { Component, OnInit } from '@angular/core';
import { EmployeeModel } from 'src/app/models/employee-model';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  underDeleteEmployee: any = null;

  status = {
    isNew: false,
    newName: '',
    underModify: null,
    modifiedName: '',
    errorMessage: ''
  };
  
  employees: EmployeeModel[] = []

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {    
    this.load(false);
  }

  deleteConfirmed(): void {
    this.employeeService.deleteEmployee(this.underDeleteEmployee).subscribe(
      result => {
        this.underDeleteEmployee.isDeleted = true;
        this.underDeleteEmployee = null;
      },
      error => {
        console.log(error);
      }
    )
  }

  delete(employee:any): void {
    this.underDeleteEmployee = employee;
  }

  modify(employee: any): void {
    this.status.underModify = employee.id;
    this.status.errorMessage = '';
  }

  new(): void {
    this.status.isNew = true;
    this.status.newName = '';
    this.status.errorMessage = '';
  }

  saveUpdate(employee: any): void {
    this.status.errorMessage = '';
    const modifiedEmployee = {
      id: Number(this.status.underModify),
      name: this.status.modifiedName,
      isDeleted: false
    }
    this.employeeService.modifyEmployee(modifiedEmployee).subscribe(
      result => {
        employee.name = result.name;
        this.status.underModify = null;
      },
      error => {
        console.log(error);
        this.status.errorMessage = error.error.errorMessage;
      }
    );
  }

  nameModified(event: any): void {
    this.status.modifiedName = event.target.value;
  }

  newNameChanged(event: any): void {
    this.status.newName = event.target.value;
  }

  saveNew(): void {
    this.status.errorMessage = '';
    const newEmployee = {
      id: 0,
      name: this.status.newName,
      isDeleted: false
    }
    this.employeeService.createEmployee(newEmployee).subscribe(
      result => {
        this.employees.push(result);
        this.status.isNew = false;
      },
      error => {
        console.log(error);
        this.status.errorMessage = error.error.errorMessage;
      }
    );
  }

  reload(event: any): void {
    this.load(event.target.checked)
  }

  load(withDeleted: boolean): void {
    this.employeeService.getEmployees(withDeleted).subscribe(
      result => {
        this.employees = result;
      },
      error => {
        console.log(error);
      }
    )
  }
}
