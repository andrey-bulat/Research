import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { CookieService } from 'ngx-cookie-service';

import { Observable, of, Subject, BehaviorSubject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { AppSettings } from '../app.settings'

import { LoginInfo, UserInfo } from "./user.data"
import { stringify } from '@angular/compiler/src/util';
import { HttpErrorHandler, HandleError } from '../http-error-handler.service';
import { AlertService } from "../alert"

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  public onChange: EventEmitter<string> = new EventEmitter<string>();

  webApiUrl: string;
  private handleError: HandleError;

  private subject: BehaviorSubject<UserInfo>;

  loginUrl = 'Login/SignIn';
  logoutUrl = 'Login/SignOut';

  constructor(private http: HttpClient, private appSettings: AppSettings,
    private httpErrorHandler: HttpErrorHandler, private alertService: AlertService,
    private cookieService: CookieService) {
    this.webApiUrl = appSettings.webApiUrl;
    this.handleError = httpErrorHandler.createHandleError('AuthenticationService');

    const user = JSON.parse(localStorage.getItem('currentUser')) as UserInfo;
    this.subject = new BehaviorSubject<UserInfo>(user);
  }

  getMessage(): Observable<any> {
    return this.subject.asObservable();
  }

  logIn(userName: string, password: string): Observable<UserInfo> {
    var loginInfo = new LoginInfo;
    loginInfo.login = userName;
    loginInfo.password = password;

    return this.http
      .post(this.webApiUrl + "/" + this.loginUrl, JSON.stringify(loginInfo), httpOptions)
      .pipe(map(
        (user: UserInfo) => {
          localStorage.setItem("currentUser", JSON.stringify(user));

          // console.log("logIn", user);
          this.subject.next(user)
          return user;
        })
      );
  }

  logOut() {
    this.http.post(this.webApiUrl + "/" + this.logoutUrl, JSON.stringify(localStorage.getItem('currentUser')), httpOptions)
      .subscribe(
        r => {
          localStorage.removeItem('currentUser');
          this.onChange.emit("");

          // console.log("logOut");
          this.subject.next(null)
        },
        (err: HttpErrorResponse) => {
          this.alertService.error("Server error");
        }
      );
  }

  isAuthenticated(): boolean {
    return this.cookieService.check('.AspNetCore.Cookies') && localStorage.getItem('currentUser') != null;
  }

  getCurrentUser(): UserInfo {
    return this.isAuthenticated
      ? JSON.parse(localStorage.getItem('currentUser')) as UserInfo
      : null;
  }
}
