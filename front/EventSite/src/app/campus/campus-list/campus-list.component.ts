import { Component, OnInit } from '@angular/core';
import {CampusService} from "../../services/campus.service";
import {Campus} from "../../models/campus";

@Component({
  selector: 'app-campus-list',
  templateUrl: './campus-list.component.html',
  styleUrls: ['./campus-list.component.scss']
})
export class CampusListComponent implements OnInit {

  public campus: Campus[] = [];
  public camp: Campus = {};

  constructor(private campusService: CampusService) { }

  ngOnInit(): void {
    this.initCampus()
  }

  public initCampus(): void {
    this.campusService
      .getAll()
      .subscribe(campus => this.campus = campus);
  }

  public create(): void {
    console.log(this.camp);
    this.campusService
      .create(this.camp)
      .subscribe(_ => this.initCampus());
  }
}
