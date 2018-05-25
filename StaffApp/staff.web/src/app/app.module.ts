import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';
import { HttpClientXsrfModule } from '@angular/common/http';

import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app.component';
import { StaffDetailComponent } from './staff-detail/staff-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { StaffListComponent } from './staff-list/staff-list.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component'
import { CurrentUserComponent } from "./currentUser/currentUser.component"

import { MessageService } from './message.service';
import { HttpErrorHandler } from './http-error-handler.service';
import { AuthGuard } from './auth.guard';

import { AlertComponent, AlertService } from "./alert"
import { AuthenticationService } from "./login/authenticationService.service"

import { AppSettings } from './app.settings'
import { AppRoutingModule } from './app-routing.module';

import { httpInterceptorProviders } from "./interceptors"

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    StaffDetailComponent,
    StaffListComponent,
    MessagesComponent,
    LoginComponent,
    HomeComponent,
    CurrentUserComponent,
    AlertComponent
  ],
  providers: [
    CookieService,
    AuthGuard,
    AlertService,
    HttpErrorHandler,
    AuthenticationService,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
