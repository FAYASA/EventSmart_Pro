import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment';

interface VenueDto {
  id: number;
  name: string;
}

interface CategoryDto {
  id: number;
  name: string;
}

interface EventItem {
  id: number;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  imageUrl: string;
  category: CategoryDto;
  venue: VenueDto;
}

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './event-list.html',
})
export class EventListComponent implements OnInit {
  events: EventItem[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<EventItem[]>(`${environment.apiUrl}/event`).subscribe({
      next: (data) => (this.events = data),
      error: (err) => console.error('Failed to load events', err),
    });
  }
}
