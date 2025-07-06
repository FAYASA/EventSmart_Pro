import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment';

interface EventItem {
  id: number;
  title: string;
}

@Component({
  selector: 'app-event-registration',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-register.html',
})
export class EventRegistrationComponent implements OnInit {
  events: EventItem[] = [];
  selectedEventId: number | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<EventItem[]>(`${environment.apiUrl}/event`).subscribe({
      next: (data) => (this.events = data),
      error: (err) => console.error('Failed to load events', err)
    });
  }

  register(): void {
    if (!this.selectedEventId) return;

    this.http.post(`${environment.apiUrl}/Registration/register/${this.selectedEventId}`, {}).subscribe({
      next: () => alert('Successfully registered.'),
      error: (err) => console.error('Failed to load events', err)
    });
  }
}
