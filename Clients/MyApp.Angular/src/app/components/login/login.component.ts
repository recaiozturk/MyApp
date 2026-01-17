import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  returnUrl = '/admin';
  hidePassword = true;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/admin']);
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/admin';
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.error = '';

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.login(this.loginForm.value)
      .subscribe({
        next: () => {
          // SuperAdmin ise yeni sekmede admin paneli aç
          if (this.authService.isSuperAdmin()) {
            window.open('/admin', '_blank');
            this.router.navigate(['/']);
          } else {
            // Normal kullanıcı ana sayfaya yönlendir
            this.router.navigate(['/']);
          }
          this.loading = false;
        },
        error: (error) => {
          this.error = error.error?.message || 'Giriş başarısız. Lütfen bilgilerinizi kontrol edin.';
          this.loading = false;
        }
      });
  }
}
