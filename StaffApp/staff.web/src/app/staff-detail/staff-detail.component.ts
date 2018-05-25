import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Employee } from '../model/employee.data';
import { StaffService } from '../model/staff.service';

import { AppSettings } from '../app.settings'

@Component({
  selector: 'app-staff-detail',
  templateUrl: './staff-detail.component.html'
})
export class StaffDetailComponent implements OnInit {
  employee$: Observable<Employee>;

  webApiUrl: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private staffService: StaffService,
    private appSettings: AppSettings
  ) {
    this.webApiUrl = appSettings.webApiUrl;
  }

  ngOnInit(): void {
    this.employee$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.staffService.getDetail(params.get('id')))
    );
  }
}
