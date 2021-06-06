import {Component, Input, OnInit} from '@angular/core';
import {EventsService} from "../../services/events.service";
import {IEvent} from "../../models/event";
import {Status} from "../../models/status";
import {CitiesService} from "../../services/cities.service";
import {City} from "../../models/city";
import {Place} from "../../models/place";
import {Campus} from "../../models/campus";
import {IdentityService} from "../../services/identity.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss']
})
export class EventFormComponent implements OnInit {

  @Input() public event: IEvent = {};
  @Input() public campus: Campus | undefined;

  public cities: City[] = [];
  public city: City | undefined;
  public place: Place | undefined;

  constructor(private eventService: EventsService,
              private citiesService: CitiesService,
              private identiyService: IdentityService,
              private router: Router) { }

  ngOnInit(): void {
    this.citiesService
      .getAll()
      .subscribe(cities => this.cities = cities);
  }
}
