import { Component, Input, SimpleChanges } from '@angular/core';
import { ProductService } from '../shared/services/product.service';
import { ProductModel } from '../shared/models/product.model';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.scss'],
})
export class ProductsTableComponent {
  @Input()
  products: ProductModel[] = [];

  displayedModelColumns: string[] = [
    'name',
    'description',
    'priceInRubles',
    'generalNote',
    'specialNote',
    'categoryName',
  ];
  displayedColumns: string[] = [
    'name',
    'description',
    'priceInRubles',
    'generalNote',
    'specialNote',
    'categoryName',
    'actions',
  ];
  showDollarPrice = false;

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    this.products = changes['products'].currentValue;
  }

  constructor(private readonly productService: ProductService) {}

  deleteProduct(product: ProductModel) {
    this.productService.deleteProduct(product).subscribe(() => {
      this.products = this.products.filter((p) => p !== product);
    });
  }

  startEdit(product: ProductModel) {
    product.editing = true;
  }

  saveProduct(product: ProductModel) {
    product.editing = undefined;
    this.productService.updateProduct(product).subscribe();
  }

  showPriceInDollars(product: ProductModel) {
    this.showDollarPrice = true;
  }

  hidePriceInDollars() {
    this.showDollarPrice = false;
  }
}
