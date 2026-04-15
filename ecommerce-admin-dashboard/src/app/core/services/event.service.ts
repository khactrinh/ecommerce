import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface EventMessage {
  messageId: string;
  type: string;
  occurredOn: string;
  data: any;
}

@Injectable({ providedIn: 'root' })
export class EventService {
  private baseUrl = `${environment.apiUrl}/api/events`;

  constructor(private http: HttpClient) {}

  getEvents(): Observable<EventMessage[]> {
    return this.http.get<EventMessage[]>(this.baseUrl);
  }

  retryEvent(id: string) {
    return this.http.post(`${this.baseUrl}/retry/${id}`, {});
  }

  sendToDLQ(id: string) {
    return this.http.post(`${this.baseUrl}/dlq/${id}`, {});
  }
}
