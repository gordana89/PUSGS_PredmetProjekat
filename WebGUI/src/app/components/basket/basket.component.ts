import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProductModel } from 'src/app/models/productModel';
import { AddProductService } from 'src/app/services/add-product.service';
import { NotificationService } from 'src/app/services/notification.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  public productsList: any = [];
  public productsInBasket: any = [];
  color: 'lightblue';
  total: number = 0;
  user = JSON.parse(localStorage.getItem('user') || '{}');

  public req = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });

  constructor(
    private http: HttpClient,
    private notifyService: NotificationService,
    private service: AddProductService
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

    this.productsInBasket = this.service.getAllProductsFromBasket();
  }

  addProductToBasket(prod): void {
    this.service.addProductToBasket(prod);
    this.total = this.service.calcuclateNewTotalPrice();
  }

  removeProductFromBasket(prod) {
    this.service.removeProductFromBasket(prod);
  }

  calculatePrice(): number {
    let total = 0;
    this.productsInBasket.forEach((product) => {
      total += product.price;
    });

    total += 300;

    return total;
  }

  checkout(): void {
    const addressForDelivery = (<HTMLInputElement>(
      document.getElementById('addressForDelivery')
    )).value;

    const comment = (<HTMLInputElement>document.getElementById('comment'))
      .value;

    if (addressForDelivery === '') {
      alert('Please enter an address that will be used for the delivery!');
      return;
    }

    let obj = {
      address: addressForDelivery,
      comment: comment,
      products: this.productsInBasket,
      creatorId: this.user.userID,
      price: this.calculatePrice(),
    };

    console.log(obj);

    this.http
      .post<any>(`${environment.orders}`, obj, {
        headers: this.req,
      })
      .subscribe(
        (res) => {
          this.notifyService.showSuccess(
            `Order successfully placed`,
            'Delivery Store'
          );
          this.service.removeAllProductsFromBasket();
          setTimeout(() => {
            window.location.reload();
          }, 1500);
        },
        (err) => {
          console.log(err);
        }
      );
  }
}
