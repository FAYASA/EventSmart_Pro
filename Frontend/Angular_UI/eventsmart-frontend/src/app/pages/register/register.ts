// register.component.ts (standalone Angular 17+)
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService, RegisterDto } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="container mt-5" style="max-width: 500px">
      <h2 class="text-center">Register</h2>
      <form (ngSubmit)="onSubmit()">
        <div class="mb-3">
          <label>Full Name</label>
          <input type="text" class="form-control" [(ngModel)]="model.fullName" name="fullName" required>
        </div>
        <div class="mb-3">
          <label>Email</label>
          <input type="email" class="form-control" [(ngModel)]="model.email" name="email" required>
        </div>
        <div class="mb-3">
          <label>Password</label>
          <input type="password" class="form-control" [(ngModel)]="model.password" name="password" required>
        </div>
        <div class="mb-3">
          <label>Role</label>
          <select class="form-select" [(ngModel)]="model.role" name="role" required>
            <option value="Attendee">Attendee</option>
            <option value="Organizer">Organizer</option>
            <option value="Vendor">Vendor</option>
            <option value="Admin">Admin</option>
          </select>
        </div>
        <button type="submit" class="btn btn-success w-100">Register</button>
        <div class="text-danger mt-2" *ngIf="error">{{ error }}</div>
      </form>
    </div>
  `
})
export class RegisterComponent {
  model: RegisterDto = { fullName: '', email: '', password: '', role: 'Attendee' };
  error: string | null = null;

  constructor(private auth: AuthService) {}

  onSubmit() {
    this.auth.register(this.model).subscribe({
      next: () => window.location.href = '/login',
      error: (err) => {
        this.error = err?.error || 'Registration failed';
      }
    });
  }
}