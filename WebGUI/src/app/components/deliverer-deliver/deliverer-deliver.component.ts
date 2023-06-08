import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OrderModel } from 'src/app/models/orderModel';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-deliverer-deliver',
  templateUrl: './deliverer-deliver.component.html',
  styleUrls: ['./deliverer-deliver.component.scss'],
})
export class DelivererDeliverComponent implements OnInit {
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
            if (
              item.delivererId == this.user.userID &&
              item.status == 'In progress'
            ) {
              this.listOfOrders.push(item);
            }
          });
        },
        (err) => {
          this.notifyService.showError(
            `Something went wrong`,
            'Delivery Store'
          );
        }
      );
  }
  deliver(order): void {
    this.http
      .put<any>(`${environment.orders}/${order.id}/archive`, null, {
        headers: this.reqHeader,
      })
      .subscribe(
        (res) => {
          console.log(res);

          this.listOfOrders.find((element) => element.id == order.id).status =
            '2';

          setTimeout(() => {
            this.notifyService.showSuccess(
              `Order delivered successfully !!!`,
              'Delivery Store'
            );
            window.location.href = 'ordersHistoryDeliverer';
          }, 1500);
        },
        (err) => {
          if (err.error.Message.includes('Order is already delivered'))
            this.notifyService.showError(
              `Order is already delivered`,
              'Delivery Store'
            );
        }
      );
  }
}
