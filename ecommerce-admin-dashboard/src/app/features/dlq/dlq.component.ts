import { Component } from '@angular/core';

@Component({
  selector: 'app-dlq',
  template: `
    <h2>💀 Dead Letter Queue</h2>
    <p>Failed events will appear here</p>
  `,
})
export class DlqComponent {}
