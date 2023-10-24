import { Component } from '@angular/core';
import { ProductService } from '../shared/services/product.service';
import { ProductModel } from '../shared/models/product.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  products: ProductModel[] = [];

  constructor(private readonly productService: ProductService) {
    productService.getProducts().subscribe((products: ProductModel[])=>{
      this.products = products;
    });
  }
}
