import { Component } from '@angular/core';
import { ProductModel } from '../shared/models/product.model';
import { ProductService } from '../shared/services/product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent {
  products: ProductModel[] = [];
  displayedModelColumns: string[] = ['name', 'description', 'priceInRubles', 'generalNote', 'specialNote', 'categoryName'];
  displayedColumns: string[] = [...this.displayedModelColumns, 'actions'];

  constructor(
    private readonly productService: ProductService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(params => {
      const categoryName = params['categoryName'];
      productService.getProductsByCategoryName(categoryName).subscribe((products: ProductModel[]) => {
        this.products = products;
      });
    });
  }
}
