// user-list.component.ts
import { Component } from '@angular/core';
import { User } from '../shared/models/user.model';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
  users: User[] = [];
  displayedColumns: string[] = ['Name', 'Email', 'Roles'];

  constructor(private readonly userService: UserService) {
    userService.getAllUsers().subscribe((users: User[]) => {
      this.users = users;
    });
  }
}
