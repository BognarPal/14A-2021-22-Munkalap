import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //TODO
  allowedOperations: string[] = [  
    'empl_assign',
    'work_start',
    'work_finish',
    'work_check',
    'failure_delete'
  ]

  constructor() { }
}
