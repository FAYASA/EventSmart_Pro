// login.component.ts (standalone Angular 17+)
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService, LoginDto } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="container mt-5" style="max-width: 400px">
      <h2 class="text-center">Login</h2>
      <form (ngSubmit)="onSubmit()">
        <div class="mb-3">
          <label>Email</label>
          <input type="email" class="form-control" [(ngModel)]="model.email" name="email" required>
        </div>
        <div class="mb-3">
          <label>Password</label>
          <input type="password" class="form-control" [(ngModel)]="model.password" name="password" required>
        </div>
        <button type="submit" class="btn btn-primary w-100">Login</button>
        <div class="text-danger mt-2" *ngIf="error">{{ error }}</div>
      </form>
    </div>
  `
})
export class LoginComponent {
  model: LoginDto = { email: '', password: '' };
  error: string | null = null;

  constructor(private auth: AuthService) {}

  onSubmit() {
    this.auth.login(this.model).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token);
        window.location.href = '/';
      },
      error: (err) => {
        this.error = err?.error || 'Login failed';
      }
    });
  }
}
