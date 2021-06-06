import {Place} from "./place";
import {Campus} from "./campus";
import {Reveller} from "./reveller";
import {Status} from "./status";

export interface IEvent {
  id?: string;
  name?: string;
  beginTime?: Date;
  duration?: string;
  subscribedDeadline?: Date;
  isFull?: boolean;
  maxMembers?: number;
  infos?: string;
  status?: Status;
  placeId?: string;
  place?: Place;
  campusId?: string;
  campus?: Campus;
  creatorId?: string;
  creator?: Reveller;
}
