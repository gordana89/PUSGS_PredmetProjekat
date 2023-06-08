import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OrderModel } from 'src/app/models/orderModel';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-orders-history-client',
  templateUrl: './orders-history-client.component.html',
  styleUrls: ['./orders-history-client.component.scss'],
})
export class OrdersHistoryClientComponent implements OnInit {
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
      .get<any>(`${environment.customerId}${this.user.userID}`, {
        headers: this.reqHeader,
      })
      .subscribe(
        (res) => {
          res.forEach((item: any) => {
            this.listOfOrders.push(item);
          });
        },
        (err) => {
          alert('Something went wrong');
        }
      );
  }
}
