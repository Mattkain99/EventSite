import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Campus} from "../models/campus";

@Injectable({
  providedIn: 'root'
})
export class CampusService {

  constructor(private httpClient : HttpClient) { }

  public getAll(): Observable<Campus[]> {
    return this.httpClient.get<Campus[]>("/api/campus");
  }

  public create(camp: Campus): Observable<any> {
    return this.httpClient.post<any>("/api/campus", camp);
  }
}
