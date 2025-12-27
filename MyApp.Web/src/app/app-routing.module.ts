import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  
  // Public routes - Giriş gerektirmez
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  
  // Protected routes - Giriş gerektirir
  // Her route'a ayrı ayrı AuthGuard ekliyoruz
  // Alternatif: Parent route kullanabilirsiniz ama bu durumda router-outlet yapısı değişir
  { 
    path: 'products', 
    component: ProductListComponent,
    canActivate: [AuthGuard] // /products için guard
  },
  { 
    path: 'products/new', 
    component: ProductFormComponent,
    canActivate: [AuthGuard] // /products/new için guard
  },
  { 
    path: 'products/:id/edit', 
    component: ProductFormComponent,
    canActivate: [AuthGuard] // /products/:id/edit için guard
  },
  
  // Admin panel - Lazy loading
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
