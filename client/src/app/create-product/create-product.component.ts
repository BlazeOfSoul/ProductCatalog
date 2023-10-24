import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../shared/services/product.service';
import { ProductModel } from '../shared/models/product.model';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss'],
})
export class CreateProductComponent {
  productForm!: FormGroup;

  constructor(
    private productService: ProductService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.createForm();
  }

  createForm() {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      priceInRubles: [0, [Validators.required, Validators.min(0)]],
      generalNote: ['', Validators.required],
      specialNote: ['', Validators.required],
      categoryName: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.productForm && this.productForm.valid) {
      const newProduct: ProductModel = {
        ...this.productForm.value,
      };
      this.productService.addProducts(newProduct).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}
