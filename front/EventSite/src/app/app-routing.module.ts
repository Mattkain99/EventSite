import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {CampusListComponent} from "./campus/campus-list/campus-list.component";
import {EventsListComponent} from "./events/events-list/events-list.component";
import {MenuComponent} from "./menu/menu.component";
import {CitiesListComponent} from "./cities/cities-list/cities-list.component";

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
    path : "cities",
    component : CitiesListComponent
  },
  {
    path : "events",
    component : EventsListComponent
  },
  {
    path : "",
    component : MenuComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
