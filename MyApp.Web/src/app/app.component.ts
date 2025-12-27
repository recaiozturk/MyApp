import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { User } from './models/auth.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'MyApp';
  currentUser: User | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  isSuperAdmin(): boolean {
    return this.authService.isSuperAdmin();
  }

  openAdminPanel(): void {
    // Admin paneli yeni sekmede aç
    const url = this.router.serializeUrl(this.router.createUrlTree(['/admin']));
    window.open(url, '_blank');
  }
}
