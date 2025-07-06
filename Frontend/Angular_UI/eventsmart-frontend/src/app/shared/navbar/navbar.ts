// navbar.component.ts (standalone + ready for app.component.ts integration)
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl : './navbar.html',
})
export class NavbarComponent {
  constructor(private auth: AuthService) {}

  isLoggedIn() {
    return this.auth.isLoggedIn();
  }

  logout() {
    this.auth.logout();
  }

  getUser() {
    return this.auth.getUser();
  }

  isOrganizer() {
    return this.auth.getUserRole() === 'Organizer';
  }

    isAdmin() {
    return this.auth.getUserRole() === 'Admin';
  }

      isAttendee() {
    return this.auth.getUserRole() === 'Attendee';
  }

        isVendor() {
    return this.auth.getUserRole() === 'Vendor';
  }
}
