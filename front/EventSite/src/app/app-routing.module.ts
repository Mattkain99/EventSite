import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {CampusListComponent} from "./campus/campus-list/campus-list.component";
import {EventsListComponent} from "./events/events-list/events-list.component";

const routes: Routes = [
  {
    path : "login",
    component : LoginComponent
  },
  {
    path : "campus",
    component : CampusListComponent
  },
  {
    path : "events",
    component : EventsListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
