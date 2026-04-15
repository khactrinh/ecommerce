import { Component, OnInit } from '@angular/core';
import { EventService, EventMessage } from '../../../core/services/event.service';
import { SignalRService } from '../../../core/services/signalr.service';

@Component({
  selector: 'app-event-timeline',
  templateUrl: './event-timeline.component.html',
})
export class EventTimelineComponent implements OnInit {
  events: EventMessage[] = [];

  constructor(
    private eventService: EventService,
    private signalR: SignalRService,
  ) {}

  ngOnInit(): void {
    this.load();

    this.signalR.startConnection();

    this.signalR.events$.subscribe((event) => {
      this.events.unshift(event);
    });
  }

  load() {
    this.eventService.getEvents().subscribe((res) => (this.events = res));
  }
}
