import { Component, OnInit } from '@angular/core';
import {City} from "../../models/city";
import {CitiesService} from "../../services/cities.service";

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.scss']
})
export class CitiesListComponent implements OnInit {

  public cities : City[] = [];
  public city : City = {};

  constructor(private citiesService : CitiesService) { }

  ngOnInit(): void {
    this.initCities();
  }

  public initCities(): void {
    this.citiesService
      .getAll()
      .subscribe(cities => this.cities = cities);
  }

  public create(): void {
    this.citiesService
      .create(this.city)
      .subscribe(_ => this.initCities());
  }
}
