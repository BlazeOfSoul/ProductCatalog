import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductModel } from '../models/product.model';
import { BaseDataService } from './base.data.service';
import { actionRoutes, controllerRoutes } from '../constants/url.constants';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ProductService extends BaseDataService {
  constructor(httpClient: HttpClient) {
    super(httpClient, controllerRoutes.product);
  }

  public addProducts(product: ProductModel): Observable<any> {
    return this.sendPostRequest(product, actionRoutes.addProduct);
  }

  public getProducts(): Observable<ProductModel[]> {
    return this.sendGetRequest({}, actionRoutes.getProducts);
  }

  public getProductsByCategoryName(categoryName: string): Observable<any> {
    return this.sendGetRequest(
      { categoryName: categoryName },
      actionRoutes.getProductsByName
    );
  }

  public updateProduct(product: ProductModel) {
    return this.sendPutRequest(product, actionRoutes.updateProducts);
  }

  public deleteProduct(product: ProductModel): Observable<any> {
    return this.sendDeleteRequest(product.id, actionRoutes.deleteProduct);
  }
}
