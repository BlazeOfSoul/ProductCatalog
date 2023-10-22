import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared/access/auth.guard';
import { HomeComponent } from './home/home.component';
import { CreateProductComponent } from './create-product/create-product.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'create-product',
    component: CreateProductComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'category-list',
    component: CategoryListComponent,
    canActivate: [AuthGuard],
  },
  {
  path: 'product/all-category-name/:categoryName',
  component: CategoryDetailComponent,
  canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
