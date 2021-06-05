import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {IdentityService} from "./services/identity.service";
import { MenuComponent } from './menu/menu.component';
import { EventsListComponent } from './events/events-list/events-list.component';
import { EventFormComponent } from './events/event-form/event-form.component';
import { EventCreationComponent } from './events/event-creation/event-creation.component';
import { EventEditionComponent } from './events/event-edition/event-edition.component';
import { CitiesListComponent } from './cities/cities-list/cities-list.component';
import { CampusListComponent } from './campus/campus-list/campus-list.component';
import {CampusService} from "./services/campus.service";
import {CitiesService} from "./services/cities.service";
import {EventsService} from "./services/events.service";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MenuComponent,
    EventsListComponent,
    EventFormComponent,
    EventCreationComponent,
    EventEditionComponent,
    CitiesListComponent,
    CampusListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    IdentityService,
    CampusService,
    CitiesService,
    EventsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
