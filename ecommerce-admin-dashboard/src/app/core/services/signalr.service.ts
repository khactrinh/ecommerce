import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SignalRService {
  private hub!: signalR.HubConnection;

  public events$ = new Subject<any>();

  startConnection() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(environment.signalrUrl)
      .withAutomaticReconnect()
      .build();

    this.hub
      .start()
      .then(() => console.log('SignalR connected'))
      .catch((err) => console.error(err));

    this.hub.on('EventReceived', (data) => {
      this.events$.next(data);
    });
  }
}
