import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  usersToVerify: any[] = [];
  deliveersForApproval: any[] = [];

  public req = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });

  constructor(
    private notifyService: NotificationService,
    private http: HttpClient
  ) {}

  getUsers(): any {
    this.http.get<any>(environment.users, { headers: this.req }).subscribe(
      (res) => {
        this.usersToVerify = res;
        res.forEach((user: any) => {
          if (user.userType == 'Deliverer')
            this.deliveersForApproval.push(user);
        });
        // this.deliveersForApproval.forEach((user: any) => console.log(user));
        return this.deliveersForApproval;
      },
      (err) => {
        this.notifyService.showError(
          `Error: ${err.error.Message}`,
          'Delivery Store'
        );
      }
    );
  }
}
