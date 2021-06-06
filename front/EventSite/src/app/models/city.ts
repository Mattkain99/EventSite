import {Place} from "./place";

export interface City {
  id?: string;
  name?: string;
  zipCode?: string;
  places?: Place[];
}
