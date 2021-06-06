import {City} from "./city";

export interface Place {
  id: string;
  name: string;
  street: string;
  cityId: string;
  city: City;
}
