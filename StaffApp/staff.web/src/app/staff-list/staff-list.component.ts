import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { Observable, of, range } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

import { StaffItem } from '../model/staffItem.data';
import { StaffService } from '../model/staff.service';
import { StaffPage } from '../model/staffPage.data';

import { AppSettings } from '../app.settings'

@Component({
  selector: 'app-staff-list',
  templateUrl: './staff-list.component.html'
})
export class StaffListComponent implements OnInit {
  page$: Observable<StaffPage>;
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
    this.page$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.staffService.getPage(params.get('id'), params.get('count')))
    );
  }

  getPagesNum(n:number):number[]{
    return Array(n).fill(0).map((x,i)=>i);
  }
}
