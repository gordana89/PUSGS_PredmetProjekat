import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OrderModel } from 'src/app/models/orderModel';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-orders-history-deliverer',
  templateUrl: './orders-history-deliverer.component.html',
  styleUrls: ['./orders-history-deliverer.component.scss'],
})
export class OrdersHistoryDelivererComponent implements OnInit {
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
              item.status != 'Waiting'
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
}
