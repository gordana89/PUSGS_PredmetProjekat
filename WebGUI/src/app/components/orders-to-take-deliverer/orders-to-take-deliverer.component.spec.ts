import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersToTakeDelivererComponent } from './orders-to-take-deliverer.component';

describe('OrdersToTakeDelivererComponent', () => {
  let component: OrdersToTakeDelivererComponent;
  let fixture: ComponentFixture<OrdersToTakeDelivererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersToTakeDelivererComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersToTakeDelivererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
