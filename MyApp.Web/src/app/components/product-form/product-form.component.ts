import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  isEditMode = false;
  productId: number | null = null;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      stock: ['', [Validators.required, Validators.min(0)]],
      category: ['']
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.productId = +params['id'];
        this.loadProduct(this.productId);
      }
    });
  }

  loadProduct(id: number): void {
    this.productService.getProduct(id).subscribe({
      next: (product) => {
        this.productForm.patchValue({
          name: product.name,
          description: product.description,
          price: product.price,
          stock: product.stock,
          category: product.category
        });
      },
      error: (error) => {
        console.error('Error loading product:', error);
        this.errorMessage = 'Ürün yüklenemedi.';
      }
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.errorMessage = '';
      const productData: Product = this.productForm.value;
      
      if (this.isEditMode && this.productId) {
        this.productService.updateProduct(this.productId, productData).subscribe({
          next: () => {
            this.router.navigate(['/products']);
          },
          error: (error) => {
            console.error('Error updating product:', error);
            this.errorMessage = 'Ürün güncellenemedi. Lütfen tekrar deneyin.';
          }
        });
      } else {
        this.productService.createProduct(productData).subscribe({
          next: () => {
            this.router.navigate(['/products']);
          },
          error: (error) => {
            console.error('Error creating product:', error);
            this.errorMessage = 'Ürün oluşturulamadı. Lütfen tekrar deneyin.';
          }
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
