import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import { UsersService } from 'src/app/services/users.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-verify-deliverer-admin',
  templateUrl: './verify-deliverer-admin.component.html',
  styleUrls: ['./verify-deliverer-admin.component.scss'],
})
export class VerifyDelivererAdminComponent implements OnInit {
  public usersToVerify: any;
  public deliveersForApproval: any = [];
  public req = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });
  constructor(
    private notifyService: NotificationService,
    private http: HttpClient,
    private service: UsersService
  ) {}

  ngOnInit(): void {
    this.http.get<any>(environment.users, { headers: this.req }).subscribe(
      (res) => {
        this.usersToVerify = res;
        res.forEach((user: any) => {
          if (user.userType == 'Deliverer')
            this.deliveersForApproval.push(user);
        });
        this.deliveersForApproval.forEach((user: any) => console.log(user));
      },
      (err) => {
        this.notifyService.showError(
          `Error: ${err.error.Message}`,
          'Delivery Store'
        );
      }
    );
  }

  rejectUser(email): void {
    this.http
      .put<any>(`${environment.users}/${email}/cancel`, null, {
        headers: this.req,
      })
      .subscribe(
        (res) => {
          this.deliveersForApproval.find(
            (element) => element.email == email
          ).status = 'Canceled';
          this.notifyService.showWarning(
            `User with email: ${email}\nSuccessfully rejected !!`,
            'Delivery Store'
          );
        },
        (err) => {
          this.notifyService.showError(
            `Error: ${err.error.Message}`,
            'Delivery Store'
          );
        }
      );
  }

  approveUser(email): void {
    this.http
      .put<any>(`${environment.users}/${email}/approve`, null, {
        headers: this.req,
      })
      .subscribe(
        (res) => {
          this.deliveersForApproval.find(
            (element) => element.email == email
          ).status = 'Approved';
          this.notifyService.showSuccess(
            `User with email: ${email}\nSuccessfully approved !!`,
            'Delivery Store'
          );
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
