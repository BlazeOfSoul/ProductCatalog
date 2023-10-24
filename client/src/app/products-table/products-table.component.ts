import { Component, Input, SimpleChanges } from '@angular/core';
import { ProductService } from '../shared/services/product.service';
import { ProductModel } from '../shared/models/product.model';
import { UserService } from '../shared/services/user.service';
import { ProductModelUpdateByUser } from '../shared/models/product.model.user.update';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.scss'],
})
export class ProductsTableComponent {
  @Input()
  products: ProductModel[] = [];

  displayedColumns: string[] = [
    'actions'
  ];

  displayedModelFields: string[] = [
    'name',
    'description',
    'priceInRubles',
    'generalNote',
    'categoryName',
  ];
  showDollarPrice = false;
  specialNote: string = 'specialNote';

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    this.products = changes['products'].currentValue;
  }

  constructor(private readonly productService: ProductService, private readonly userService: UserService) {
    this.setDisplayedColumns();
  }

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

  saveProductUser(product: ProductModelUpdateByUser) {
    product.editing = undefined;
    this.productService.updateProductUser(product).subscribe();
  }

  showPriceInDollars() {
    this.showDollarPrice = true;
  }

  hidePriceInDollars() {
    this.showDollarPrice = false;
  }

  setDisplayedColumns() {
    if (this.userService.hasRoleAdminOrModerator()) {
        this.displayedModelFields.push(this.specialNote);
    }
    this.displayedColumns = this.displayedModelFields.concat(this.displayedColumns);
  }

  public isAdminOrModerator() : boolean {
    return this.userService.hasRoleAdminOrModerator();
  }

  shouldDisplayTooltip(column: string): boolean {
    return column === 'priceInRubles';
  }

  getTooltipText(product: any): string {
    return `${product['priceInDollars']} $`;
  }
}
