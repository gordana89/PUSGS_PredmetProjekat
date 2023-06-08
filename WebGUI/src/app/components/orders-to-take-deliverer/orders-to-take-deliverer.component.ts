import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OrderModel } from 'src/app/models/orderModel';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-orders-to-take-deliverer',
  templateUrl: './orders-to-take-deliverer.component.html',
  styleUrls: ['./orders-to-take-deliverer.component.scss'],
})
export class OrdersToTakeDelivererComponent implements OnInit {
  public userStatus: string;
  user = JSON.parse(localStorage.getItem('user') || '{}');
  public listOfOrders: OrderModel[] = [];
  public reqHeader = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });
  constructor(
    private http: HttpClient,
    private notifyService: NotificationService
  ) {}

  ngOnInit(): void {
    this.http
      .get<any>(`${environment.orders}`, {
        headers: this.reqHeader,
      })
      .subscribe(
        (res) => {
          res.forEach((item: any) => {
            this.listOfOrders.push(item);
          });
          console.log(this.listOfOrders);
        },
        (err) => {
          alert('Something went wrong');
        }
      );

    this.http
      .get<any>(`${environment.users}/${this.user.email}`, {
        headers: this.reqHeader,
      })
      .subscribe(
        (res) => {
          this.userStatus = res.status;
        },
        (err) => {
          alert('Something went wrong');
        }
      );
  }

  takeOrder(id, order): void {
    if (this.userStatus != 'Approved') {
      this.notifyService.showWarning(
        `Your account needs to be activated first!!!`,
        'Delivery Store'
      );
      return;
    }

    var timeForDelivery = Math.floor(Math.random() * 10) + 1;
    console.log({
      DelivererId: this.user.userID,
      TimeForDelivery: timeForDelivery.toString(),
    });
    this.http
      .put<any>(
        `${environment.orders}/${id}/take`,
        {
          DelivererId: this.user.userID,
          TimeForDelivery: timeForDelivery.toString(),
        },
        {
          headers: this.reqHeader,
        }
      )
      .subscribe(
        (res) => {
          this.notifyService.showSuccess(
            `Order taken successfully !!!`,
            'Delivery Store'
          );

          setTimeout(() => {}, 1000);

          this.notifyService.showSuccess(
            `Time window for delivery is: ${timeForDelivery}`,
            'Delivery Store'
          );
          this.listOfOrders = this.listOfOrders.filter((obj) => {
            return obj.id != id;
          });
        },
        (err) => {
          if (
            err.error.Message.toString().includes(
              'Only one order is allow to take'
            )
          )
            this.notifyService.showError(
              `You've already taken one order !!!`,
              'Delivery Store'
            );
        }
      );
  }
}
