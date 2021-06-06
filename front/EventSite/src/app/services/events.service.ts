import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {EventFilter} from "../models/event-filter";
import {IEvent} from "../models/event";

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  constructor(private httpClient : HttpClient) { }

  public getAll(filter: EventFilter) : Observable<IEvent[]>{
    return this.httpClient.post<IEvent[]>("/api/events/filteredEvents", filter);
  }

  public create(event: IEvent) : Observable<void>{
    return this.httpClient.post<void>("/api/events", event);
  }
}
