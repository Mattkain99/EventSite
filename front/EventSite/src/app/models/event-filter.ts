export interface EventFilter {
  campusId? : string;
  eventName? : string;
  startDate? : Date;
  endDate? : Date;
  includeCreator: boolean;
  includeSubscribedEvent : boolean;
  includeNotSubscribedEvent : boolean;
  includePastEvent: boolean;
}
