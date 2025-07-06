import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

interface EventItem {
  id: number;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  imageUrl?: string;
  categoryId?: number;
  venueId?: number;
}

interface Category {
  id: number;
  name: string;
}

interface Venue {
  id: number;
  name: string;
}

@Component({
  selector: 'app-event-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-edit.html',
})
export class EventEditComponent implements OnInit {
  event: EventItem = {
    id: 0,
    title: '',
    description: '',
    startDate: '',
    endDate: '',
    imageUrl: '',
    categoryId: undefined,
    venueId: undefined
  };

  categories: Category[] = [];
  venues: Venue[] = [];

  error: string | null = null;
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.error = 'Invalid event ID';
      return;
    }
    this.loading = true;

    // Load categories
    this.http.get<Category[]>(`${environment.apiUrl}/category`).subscribe({
      next: data => {
        this.categories = data;
        console.log('Loaded categories:', data);
      },
      error: err => console.error('Failed to load categories', err),
    });

    //Load venues
    this.http.get<Venue[]>(`${environment.apiUrl}/venue`).subscribe({
      next: data => {
        this.venues = data;
        console.log('Loaded venues:', data);
      },
      error: err => console.error('Failed to load venues', err),
    });

    //Load event data
    this.http.get<EventItem>(`${environment.apiUrl}/event/${id}`).subscribe({
      next: data => {
        this.event = {
          ...data,
          startDate: data.startDate?.split('T')[0],
          endDate: data.endDate?.split('T')[0]
        };
        this.loading = false;
      },
      error: err => {
        console.error('Failed to load event', err);
        this.error = 'Failed to load event data';
        this.loading = false;
      },
    });
  }

  save(): void {
    this.error = null;
    this.loading = true;

    this.http.put(`${environment.apiUrl}/event/${this.event.id}`, this.event).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/events', this.event.id]);
      },
      error: err => {
        console.error('Failed to save event', err);
        this.error = 'Failed to save event. Please check your data and try again.';
        this.loading = false;
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/events', this.event.id]);
  }
}
