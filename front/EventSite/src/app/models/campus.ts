import {Reveller} from "./reveller";

export interface Campus {
  id?: string;
  name?: string;
  revellers?: Reveller[];
  events?: Event[];
}
