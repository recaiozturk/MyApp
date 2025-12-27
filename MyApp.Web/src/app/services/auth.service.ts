import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { LoginRequest, RegisterRequest, AuthResponse, User } from '../models/auth.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;
  private currentUserSubject = new BehaviorSubject<User | null>(this.getUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    // Check if token is expired on service initialization
    this.checkTokenExpiration();
  }

  login(loginRequest: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, loginRequest).pipe(
      tap(response => {
        this.setAuthData(response);
      })
    );
  }

  register(registerRequest: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, registerRequest).pipe(
      tap(response => {
        this.setAuthData(response);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) {
      return false;
    }

    // Check if token is expired
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expiration = new Date(payload.exp * 1000);
      return expiration > new Date();
    } catch {
      return false;
    }
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  getUserRoles(): string[] {
    const token = this.getToken();
    if (!token) {
      return [];
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const roles: string[] = [];
      
      // Check 'role' claim (custom claim)
      if (payload.role) {
        if (Array.isArray(payload.role)) {
          roles.push(...payload.role);
        } else {
          roles.push(payload.role);
        }
      }
      
      // Check 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' claim (ClaimTypes.Role)
      const roleClaimKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
      if (payload[roleClaimKey]) {
        if (Array.isArray(payload[roleClaimKey])) {
          payload[roleClaimKey].forEach((role: string) => {
            if (!roles.includes(role)) {
              roles.push(role);
            }
          });
        } else if (!roles.includes(payload[roleClaimKey])) {
          roles.push(payload[roleClaimKey]);
        }
      }
      
      return roles.filter(r => r);
    } catch {
      return [];
    }
  }

  isSuperAdmin(): boolean {
    const roles = this.getUserRoles();
    return roles.includes('SuperAdmin');
  }

  hasRole(role: string): boolean {
    const roles = this.getUserRoles();
    return roles.includes(role);
  }

  private setAuthData(response: AuthResponse): void {
    localStorage.setItem('token', response.token);
    const user: User = {
      userId: response.userId,
      userName: response.userName,
      email: response.email,
      firstName: response.firstName,
      lastName: response.lastName
    };
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  private getUserFromStorage(): User | null {
    const userStr = localStorage.getItem('user');
    if (userStr) {
      try {
        return JSON.parse(userStr);
      } catch {
        return null;
      }
    }
    return null;
  }

  private checkTokenExpiration(): void {
    if (!this.isAuthenticated()) {
      this.logout();
    }
  }
}


