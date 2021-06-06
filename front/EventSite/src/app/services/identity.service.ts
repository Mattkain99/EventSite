import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Credentials} from "../models/credentials";
import {IdentityResponse} from "../models/IdentityResponse";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private httpClient : HttpClient) { }

  public getLogin(credentials : Credentials) : Observable<IdentityResponse>
  {
    return this.httpClient.post<IdentityResponse>("/api/identity/login", credentials);
  }
}


