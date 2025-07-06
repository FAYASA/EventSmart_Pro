import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-category-create',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './category-create.html'
})
export class CategoryCreateComponent {
  category = {
    name: ''
  };

  constructor(private http: HttpClient) {}

  submitCategory(): void {
    this.http.post(`${environment.apiUrl}/category`, this.category).subscribe({
      next: () => {
        alert('Category created successfully!');
        // ✅ Clear the form
        this.category = { name: '' };
      },
      error: (err) => {
        console.error('Category creation failed', err);
        alert('Failed to create category: ' + err.message);
      }
    });
  }
}
