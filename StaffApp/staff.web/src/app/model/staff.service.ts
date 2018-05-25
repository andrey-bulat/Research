import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Employee } from './employee.data'
import { StaffItem } from './staffItem.data'
import { StaffPage } from './staffpage.data'

import { HttpErrorHandler, HandleError } from '../http-error-handler.service';
import { AppSettings } from '../app.settings'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    //'Authorization': 'my-auth-token'
  })
};

@Injectable({ providedIn: 'root' })
export class StaffService {
  webApiUrl: string;

  detailsUrl = 'api/StaffData/GetEmployee';
  pageUrl = 'api/StaffData/GetPage';
  private handleError: HandleError;

  constructor(private http: HttpClient, private appSettings: AppSettings, httpErrorHandler: HttpErrorHandler) {
    this.webApiUrl = appSettings.webApiUrl;
    this.handleError = httpErrorHandler.createHandleError('StaffData');
  }

  getPage(page: any, pageSize: any): Observable<StaffPage> {
    var num = 0;
    var psize = 10;

    if (page != null) {
      num = Number(page);
      if (num == NaN) num = 0;
    }

    if (pageSize != null) {
      psize = Number(pageSize);
      if (psize == NaN) psize = 0;
    }

    var apiUrl = this.webApiUrl + '/' + this.pageUrl + '/' + num + '/' + psize;

    var errPage = new StaffPage;
    errPage.staffItems = [];

    return this.http
      .get<StaffPage>(apiUrl, httpOptions)
      .pipe(catchError(this.handleError("getPage", errPage)));
  }

  private handlePageError(error: HttpErrorResponse): Observable<StaffPage> {
    var errItem = new StaffPage;
    errItem.staffItems = [];
    return of(errItem);
  }

  getDetail(id: any): Observable<Employee> {
    var apiUrl = this.webApiUrl + '/' + this.detailsUrl + '/' + Number(id);

    var errItem = new Employee;
    errItem.employeeId = -1;
    return this.http
      .get<Employee>(apiUrl, httpOptions)
      .pipe(catchError(this.handleError("getDetail", errItem)));
  }
}

