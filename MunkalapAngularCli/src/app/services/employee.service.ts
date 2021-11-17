import { Injectable } from '@angular/core';
import { EmployeeModel } from '../models/employee-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  // public employees: EmployeeModel[] = [
  //   { id: 1, name: 'Béla', isDeleted: false},
  //   { id: 2, name: 'Géza', isDeleted: false},
  //   { id: 3, name: 'Boldizsár', isDeleted: true}
  // ]

  constructor( private http: HttpClient ) { }

  getEmployees(withDeleted: boolean): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(`${environment.ApiURL}/employee?withDeleted=${withDeleted}`);
  }

  createEmployee(employee: EmployeeModel): Observable<EmployeeModel> {      
    return this.http.put<EmployeeModel>(`${environment.ApiURL}/employee`, employee);
  }

  modifyEmployee(employee: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post<EmployeeModel>(`${environment.ApiURL}/employee`, employee);
  }

  deleteEmployee(employee: any): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: employee
    }
    return this.http.delete<any>(`${environment.ApiURL}/employee`, options);
  }


}
