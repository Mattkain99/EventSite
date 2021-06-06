import { Component, OnInit } from '@angular/core';
import {IdentityService} from "../services/identity.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public isAuthenticated : boolean = false;
  public email : string = "";
  public password : string = "";

  constructor(private identityService : IdentityService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
  }
  public login (): void {
    this.identityService
      .getLogin({userName: this.email, password: this.password})
      .subscribe(response => {
        if (response.succeeded) {
          this.router.navigate(['/']);
        }
      });
  }
}
