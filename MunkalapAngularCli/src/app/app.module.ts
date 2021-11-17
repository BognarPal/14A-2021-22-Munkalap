import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MunkalapRogzitesComponent } from './components/munkalap-rogzites/munkalap-rogzites.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { FailureDetailsComponent } from './components/failure-details/failure-details.component';
import { FailureListComponent } from './components/failure-list/failure-list.component';
import { ConfirmComponent } from './components/confirm/confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    MunkalapRogzitesComponent,
    EmployeeListComponent,
    FailureDetailsComponent,
    FailureListComponent,
    ConfirmComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
