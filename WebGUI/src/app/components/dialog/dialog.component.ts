import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddProductService } from 'src/app/services/add-product.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  public addProductForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private service: AddProductService,
    private router: Router,
    private notifyService: NotificationService
  ) {}

  ngOnInit(): void {
    this.addProductForm = this.fb.group({
      Name: ['', [Validators.required]],
      Price: [0, [Validators.required]],
      Components: ['', [Validators.required]],
    });
  }

  addProduct(): void {
    const price = (<HTMLInputElement>document.getElementById('Price')).value;
    console.log(+price);
    if (+price <= 0) {
      this.notifyService.showWarning(
        `Price must be a positive or non-zero value`,
        'Delivery Store'
      );
      // alert('Price must be a positive or non-zero value');
      return;
    }
    this.service.addProduct(this.addProductForm.value);
  }

  get f() {
    return this.addProductForm.controls;
  }
}
