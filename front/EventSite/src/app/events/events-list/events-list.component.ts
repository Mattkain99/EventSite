import { Component, OnInit } from '@angular/core';
import {EventsService} from "../../services/events.service";
import {CampusService} from "../../services/campus.service";
import {Campus} from "../../models/campus";
import {EventFilter} from "../../models/event-filter";
import {IEvent} from "../../models/event";
import {Router} from "@angular/router";

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.scss']
})
export class EventsListComponent implements OnInit {

  public events : IEvent[] = [];
  public campus : Campus[] = [];
  public filter: EventFilter = {
    includeSubscribedEvent: false,
    includePastEvent: false,
    includeNotSubscribedEvent: false,
    includeCreator: false
  };

  constructor(private eventsService : EventsService,
              private campusService : CampusService,
              private router: Router) { }

  ngOnInit(): void {
    this.campusService.getAll()
      .subscribe(campus => {
        console.log(campus);
        this.campus = campus
      });

    this.refresh();
  }

  public refresh(): void {
    this.eventsService
      .getAll(this.filter)
      .subscribe(events => this.events = events);
  }

  public create(): void {
    this.router.navigate(['/events/creation']);
  }
}
