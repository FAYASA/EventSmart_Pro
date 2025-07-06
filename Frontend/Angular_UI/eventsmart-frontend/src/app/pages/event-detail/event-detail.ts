import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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
  category?: { name: string };
  venue?: { name: string };
}

@Component({
  selector: 'app-event-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './event-detail.html',
})
export class EventDetailComponent implements OnInit {
  event: EventItem | null = null;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      console.error('Invalid event ID');
      return;
    }

    this.http.get<EventItem>(`${environment.apiUrl}/event/${id}`).subscribe({
      next: (data) => (this.event = data),
      error: (err) => {
        console.error('Failed to load event', err);
        alert('Failed to load event');
      },
    });
  }

  getImageUrl(imagePath?: string): string {
    return imagePath ? `${environment.apiUrl}/uploads/${imagePath}` : '';
  }

  deleteEvent(): void {
    if (!this.event) return;

    if (confirm('Are you sure you want to delete this event?')) {
      this.http.delete(`${environment.apiUrl}/event/${this.event.id}`).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => {
          console.error('Delete failed', err);
          alert('Delete failed: ' + err.message);
        }
      });
    }
  }

  editEvent(): void {
    if (this.event) {
      this.router.navigate(['/events/edit', this.event.id]);
    }
  }
}
