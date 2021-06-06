import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Campus} from "../models/campus";
import {City} from "../models/city";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  constructor(private httpClient : HttpClient) { }
  public getAll() : Observable<any>{
    return this.httpClient.get("/api/cities");
  }
  public create(city: City): Observable<void> {
    return this.httpClient.post<void>("/api/cities", city);
  }
}
