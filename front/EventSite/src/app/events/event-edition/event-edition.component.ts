import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {Status} from "../../models/status";
import {EventsService} from "../../services/events.service";
import {CitiesService} from "../../services/cities.service";
import {IdentityService} from "../../services/identity.service";
import {IEvent} from "../../models/event";

@Component({
  selector: 'app-event-edition',
  templateUrl: './event-edition.component.html',
  styleUrls: ['./event-edition.component.scss']
})
export class EventEditionComponent implements OnInit {

  public event: IEvent = {};

  constructor(private eventService: EventsService,
              private citiesService: CitiesService,
              private identiyService: IdentityService,
              private router: Router) { }

  ngOnInit(): void {
  }

  public save(): void {
    this.eventService.create(this.event);
  }

  public publish(): void {
    this.event.status = Status.Open;
    this.eventService.create(this.event);
  }

  public cancel(): void {
    this.router.navigate(['/events']);
  }
}
