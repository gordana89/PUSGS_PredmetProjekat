import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  public productsList: any;

  constructor(
    private http: HttpClient,
    private notifyService: NotificationService
  ) {}

  getProducts(): void {
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
