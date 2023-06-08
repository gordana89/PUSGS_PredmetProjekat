import { ProductModel } from 'src/app/models/productModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class AddProductService {
  public productToAdd = {} as ProductModel;
  public listOfAddedProducts: ProductModel[] = [];

  public req = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });
  constructor(
    private http: HttpClient,
    private notifyService: NotificationService
  ) {}

  addProduct(product: ProductModel): void {
    this.productToAdd = product;
    this.http
      .post<any>(environment.products, this.productToAdd, {
        headers: this.req,
      })
      .subscribe(
        (res) => {
          window.location.href = 'store';

          this.notifyService.showSuccess(
            `Product: ${product.Name}\nadded successfully !!`,
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

  addProductToBasket(prod: ProductModel): void {
    this.listOfAddedProducts.push(prod);
  }
  getAllProductsFromBasket(): ProductModel[] {
    return this.listOfAddedProducts;
  }
  removeAllProductsFromBasket(): void {
    this.listOfAddedProducts = [];
  }

  calcuclateNewTotalPrice(): number {
    var result: number = 0;
    this.listOfAddedProducts.forEach((product) => {
      result += +product.price;
      console.log(result);
    });
    return result;
  }

  removeProductFromBasket(prod) {
    // this.listOfAddedProducts.filter((obj) => {
    //   return obj !== product;
    // });
    this.listOfAddedProducts.forEach((item, index) => {
      if (item == prod) this.listOfAddedProducts.splice(index, 1);
    });

    this.notifyService.showInfo(
      `Removed product successfully !!`,
      'Delivery Store'
    );
  }
}
