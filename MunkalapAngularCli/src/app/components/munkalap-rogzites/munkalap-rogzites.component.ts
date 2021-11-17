import { Component, OnInit } from '@angular/core';
import { FailureModel } from 'src/app/models/failure-model';
import { FailureService } from 'src/app/services/failure.service';

@Component({
  selector: 'app-munkalap-rogzites',
  templateUrl: './munkalap-rogzites.component.html',
  styleUrls: ['./munkalap-rogzites.component.css']
})
export class MunkalapRogzitesComponent implements OnInit {

  failure = new FailureModel()
  isEmpty = {
    issuer: false,
    room: false,
    description: false
  };
  isLoading = false;
  errorMessage = '';

  constructor(private failureService: FailureService) { }

  ngOnInit(): void {    
  }

  sendFailure(): void {
    if (this.requiredFieldsCheck()) { 
      this.isLoading = true;     
      this.failureService.createFailure(this.failure).subscribe(
        result => {
          this.failure = new FailureModel();
          alert('Sikeres rögzítés');
          this.isLoading = false;
        },
        error => {
          console.log(error);
          this.isLoading = false;
          this.errorMessage = error.error.errorMessage;
        }
      );
    }
    else {
      console.log('Hamis');
    }
  }

  requiredFieldsCheck(): boolean {
    this.isEmpty.issuer = !this.failure.issuer;
    this.isEmpty.room = !this.failure.room;
    this.isEmpty.description = !this.failure.description;

    return !this.isEmpty.issuer && !this.isEmpty.room && !this.isEmpty.description;
  }


}
