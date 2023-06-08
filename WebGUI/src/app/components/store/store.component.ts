import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProductModel } from 'src/app/models/productModel';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.scss'],
})
export class StoreComponent implements OnInit {
  public productsList: any;

  constructor(
    private notifyService: NotificationService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.http.get<any>(environment.products).subscribe(
      (res) => {
        this.productsList = res;
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
