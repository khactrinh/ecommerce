import { Component } from '@angular/core';
import { EventService } from '../../core/services/event.service';

@Component({
  selector: 'app-retry',
  template: `
    <h2>🔁 Retry Queue</h2>
    <button (click)="retryAll()">Retry All</button>
  `,
})
export class RetryComponent {
  constructor(private eventService: EventService) {}

  retryAll() {
    // call backend retry endpoint
    console.log('retry triggered');
  }
}
