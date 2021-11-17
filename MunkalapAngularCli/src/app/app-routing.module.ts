import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { 
  EmployeeListComponent, 
  FailureDetailsComponent,
  MunkalapRogzitesComponent,
  FailureListComponent
} from './components';

const routes: Routes = [
  {path: 'new', component: MunkalapRogzitesComponent},
  {path: 'employees', component: EmployeeListComponent},
  {path: 'failures', component: FailureListComponent},

  
  {path: 'test', component: FailureDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
