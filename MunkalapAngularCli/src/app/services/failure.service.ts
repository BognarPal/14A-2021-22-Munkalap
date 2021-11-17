import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FailureModel } from '../models/failure-model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class FailureService {

  constructor(private http: HttpClient) { }

  createFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.put<FailureModel>(`${environment.ApiURL}/failure`, failure);
  }

  getFailures(): Observable<FailureModel[]> {
    return this.http.get<FailureModel[]>(`${environment.ApiURL}/failure`);
  }

  updateFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.post<FailureModel>(`${environment.ApiURL}/failure/update`, failure);
  }

  assignFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.post<FailureModel>(`${environment.ApiURL}/failure/assign`, failure);
  }

  finishFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.post<FailureModel>(`${environment.ApiURL}/failure/finish`, failure);
  }

  startFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.post<FailureModel>(`${environment.ApiURL}/failure/start`, failure);
  }

  checkFailure(failure: FailureModel): Observable<FailureModel> {
    return this.http.post<FailureModel>(`${environment.ApiURL}/failure/check`, failure);
  }

  deleteFailure(failure: any): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: failure
    }
    return this.http.delete<any>(`${environment.ApiURL}/failure`, options);
  }
}
