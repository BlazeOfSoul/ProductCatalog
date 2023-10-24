import { Component } from '@angular/core';
import { CategoryModel } from '../shared/models/category.model';
import { CategoryService } from '../shared/services/category.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent {
  categories: CategoryModel[] = [];
  displayedModelColumns: string[] = ['name'];
  displayedColumns: string[] = ['name', 'actions'];
  dataSource: MatTableDataSource<CategoryModel>;

  constructor(private readonly categoryService: CategoryService, private router: Router) {
    this.dataSource = new MatTableDataSource(this.categories);

    categoryService.getCategories().subscribe((category: CategoryModel[]) => {
      this.categories = category;
      this.dataSource.data = this.categories;
    });
  }

  deleteProduct(category: CategoryModel) {
    this.categoryService.deleteCategory(category).subscribe(() => {
      this.categories = this.categories.filter(c => c !== category);
      this.dataSource.data = this.categories;
    });
  }

  startEdit(category: CategoryModel) {
    category.editing = true;
  }

  saveProduct(category: CategoryModel) {
    category.editing = undefined;
    this.categoryService.updateCategory(category).subscribe();
    this.dataSource.data = this.categories;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  goToCategory(category: CategoryModel) {
    this.router.navigate(['product/all-category-name', category.name]);
  }
}
