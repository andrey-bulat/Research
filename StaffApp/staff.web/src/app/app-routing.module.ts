import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StaffDetailComponent } from './staff-detail/staff-detail.component';
import { StaffListComponent } from './staff-list/staff-list.component';
import { LoginComponent } from './login/login.component'
import { HomeComponent } from "./home/home.component"
import { AuthGuard } from './auth.guard'

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'detail/:id', component: StaffDetailComponent, canActivate: [AuthGuard] },
  { path: 'staff/:id/:count', component: StaffListComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
