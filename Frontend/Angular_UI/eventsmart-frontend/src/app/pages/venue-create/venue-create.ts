import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-venue-create',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './venue-create.html'
})
export class VenueCreateComponent {
  venue = {
    name: '',
    address: ''
  };

  constructor(private http: HttpClient) {}

  submitVenue(): void {
    this.http.post(`${environment.apiUrl}/venue`, this.venue).subscribe({
      next: () => {
        alert('Venue created successfully!');
        // Clear the form
        this.venue = {
          name: '',
          address: ''
        };
      },
      error: (err) => {
        console.error('Venue creation failed', err);
        alert('Failed to create venue: ' + err.message);
      }
    });
  }
}
