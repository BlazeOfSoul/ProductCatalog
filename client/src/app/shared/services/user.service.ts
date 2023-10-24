import { Injectable } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { fieldLocalStorage } from '../constants/local-storage.constants';
import { User } from '../models/user.model';
import { UserDataService } from './user.data.service';
import { actionRoutes, controllerRoutes } from '../constants/url.constants';
import { BaseDataService } from './base.data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class UserService extends BaseDataService {
  public constructor(private readonly userDataService: UserDataService, httpClient: HttpClient) {
    super(httpClient, controllerRoutes.user);
  }

  public saveUser(userInfo: User): void {
    localStorage.setItem(fieldLocalStorage.user, JSON.stringify(userInfo));
  }

  public getUser(): User | null {
    const userInfo = localStorage.getItem(fieldLocalStorage.user);
    return userInfo ? JSON.parse(userInfo) : null;
  }

  public getAllUsers(): Observable<User[]> {
    return this.sendGetRequest({}, actionRoutes.getAllUsers);
  }

  public deleteUser(user: User): Observable<any> {
    return this.sendDeleteRequest(user.userName, actionRoutes.deleteUser);
  }

  public hasRoleAdmin(): boolean {
    const user: User | null = this.getUser();
    return user?.roles?.includes('Admin') ?? false;
  }

  public hasRoleModerator(): boolean {
    const user: User | null = this.getUser();
    return user?.roles?.includes('Moderator') ?? false;
  }

  public hasRoleAdminOrModerator(): boolean {
    return this.hasRoleAdmin() || this.hasRoleModerator();
  }

  public hasNoRoles(): boolean {
    return !this.hasRoleAdmin() && !this.hasRoleModerator();
  }

  public getUserFromServer(): Observable<User> {
    return this.userDataService.getUserInfo();
  }

  public getUserFromServerInfoAndSave(): Subscription {
    return this.getUserFromServer().subscribe((answer) => {
      this.saveUser(answer);
    });
  }
}
