import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private http: HttpClient) { }
  private apiUrl = '/api/v1/openai/stream-chat';

  streamChat() {
    return this.http.get(this.apiUrl, {
      observe: 'events',
      responseType: 'text',
      reportProgress: true,
    });
  }
}