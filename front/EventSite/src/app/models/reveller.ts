import {Campus} from "./campus";

export interface Reveller {
  id: string;
  userName: string;
  lastName: string;
  firstName: string;
  isAdmin: boolean;
  isActive: boolean;
  campusId: string;
  campus: Campus;
}
