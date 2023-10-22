import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseDataService } from './base.data.service';
import { actionRoutes, controllerRoutes } from '../constants/url.constants';
import { HttpClient } from '@angular/common/http';
import { CategoryModel } from '../models/category.model';

@Injectable({ providedIn: 'root' })
export class CategoryService extends BaseDataService {
  constructor(httpClient: HttpClient) {
    super(httpClient, controllerRoutes.category);
  }

  public addCategory(category: CategoryModel): Observable<any> {
    return this.sendPostRequest(category, actionRoutes.addCategory);
  }

  public getCategories(): Observable<CategoryModel[]> {
    return this.sendGetRequest({}, actionRoutes.getCategories);
  }

  public updateCategory(category: CategoryModel) {
    return this.sendPutRequest(category, actionRoutes.updateCategories);
  }

  public deleteCategory(category: CategoryModel): Observable<any> {
    return this.sendDeleteRequest(category.id, actionRoutes.deleteCategory);
  }
}
