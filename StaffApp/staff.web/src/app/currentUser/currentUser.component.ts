import { Component, OnInit, OnDestroy } from '@angular/core';

import { UserInfo } from "../login/user.data"
import { AuthenticationService } from '../login/authenticationService.service';

import { Router } from "@angular/router"
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './currentUser.component.html'
})
export class CurrentUserComponent implements OnInit, OnDestroy {
  user:UserInfo;

  private subscription: Subscription;

  private serviceSubscription;

  constructor(private authenticationService: AuthenticationService,
    private router: Router) {
      this.subscription = this.authenticationService.getMessage()
      .subscribe(user => {
        // console.log(user);
        this.user=user;
      });
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  logOut() {
    this.authenticationService.logOut();
    this.router.navigate(['/home']);
  }
}
