import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { environment } from '../../../environments/environment';

interface Venue {
  id: number;
  name: string;
}

interface Category {
  id: number;
  name: string;
}

@Component({
  selector: 'app-event-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './event-create.html',
})
export class EventCreate implements OnInit {
  eventForm: FormGroup;
  venues: Venue[] = [];
  categories: Category[] = [];
  submitting = false;
  errorMessage = '';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.eventForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      imageUrl: [''],
      venueId: [null, Validators.required],
      categoryId: [null, Validators.required],
    });
  }

  ngOnInit() {
    // Load venues and categories for dropdowns
    this.http.get<Venue[]>(`${environment.apiUrl}/venue`).subscribe({
      next: (data) => (this.venues = data),
      error: (err) => console.error('Failed to load venues', err),
    });

    this.http.get<Category[]>(`${environment.apiUrl}/category`).subscribe({
      next: (data) => (this.categories = data),
      error: (err) => console.error('Failed to load categories', err),
    });
  }

  onSubmit() {
    if (this.eventForm.invalid) {
      this.errorMessage = 'Please fill in all required fields.';
      return;
    }
    this.submitting = true;
    this.errorMessage = '';

    this.http.post(`${environment.apiUrl}/event`, this.eventForm.value).subscribe({
      next: () => {
        alert('Event created successfully!');
        this.eventForm.reset();
        this.submitting = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to create event.';
        console.error(err);
        this.submitting = false;
      },
    });
  }
}
