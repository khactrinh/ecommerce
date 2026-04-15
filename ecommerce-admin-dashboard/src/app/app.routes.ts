import { Routes } from '@angular/router';
import { EventTimelineComponent } from './features/events/event-timeline/event-timeline.component';
import { DlqComponent } from './features/dlq/dlq.component';
import { RetryComponent } from './features/retry/retry.component';

export const routes: Routes = [
  { path: '', component: EventTimelineComponent },
  { path: 'dlq', component: DlqComponent },
  { path: 'retry', component: RetryComponent },
];
