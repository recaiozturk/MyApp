import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    const isAuthenticated = this.authService.isAuthenticated();

    // Debug: Token kontrolü (sadece development'ta)
    if (token) {
      console.log('AuthInterceptor - Token exists, Is Authenticated:', isAuthenticated);
      console.log('AuthInterceptor - Request URL:', request.url);
    } else {
      console.warn('AuthInterceptor - No token found in localStorage');
    }

    // Token varsa ve geçerliyse ekle
    if (token && isAuthenticated) {
      const clonedRequest = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      console.log('AuthInterceptor - Authorization header added to request');
      return next.handle(clonedRequest);
    }

    // Token yoksa veya geçersizse, normal request'i gönder
    // API tarafında 401 dönecek ve kullanıcı login sayfasına yönlendirilecek
    console.warn('AuthInterceptor - Request sent without Authorization header');
    return next.handle(request);
  }
}


