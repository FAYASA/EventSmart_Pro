// auth.service.ts - Handles login, register, logout
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
//         ^--- two dots because 'services' is inside 'app'
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  email: string;
  password: string;
  fullName: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly api = environment.apiUrl + '/auth';

  constructor(private http: HttpClient, private router: Router) {}

  login(data: LoginDto) {
    return this.http.post<{ token: string }>(`${this.api}/login`, data);
  }

// auth.service.ts (only register method)
register(data: RegisterDto) {
  return this.http.post(`${this.api}/register`, data, { responseType: 'text' });
}


  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
    return window.localStorage.getItem('token');
   }
   return null;
  }


  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }

  getUser(): any {
    const token = this.getToken();
    return token ? jwtDecode(token) : null;
  }

  getUserRole(): string | null {
    const user = this.getUser();
    if (user && user['role']) return user['role'];
    if (user) return user['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return null;
  }
}
