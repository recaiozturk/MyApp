import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  isEditMode = false;
  productId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      stock: ['', [Validators.required, Validators.min(0)]]
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
    // TODO: Implement product loading from service
    console.log('Loading product with ID:', id);
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const productData: Product = this.productForm.value;
      
      if (this.isEditMode) {
        // TODO: Implement update logic
        console.log('Updating product:', productData);
      } else {
        // TODO: Implement create logic
        console.log('Creating product:', productData);
      }
      
      this.router.navigate(['/products']);
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
