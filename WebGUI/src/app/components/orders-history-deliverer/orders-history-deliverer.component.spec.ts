import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersHistoryDelivererComponent } from './orders-history-deliverer.component';

describe('OrdersHistoryDelivererComponent', () => {
  let component: OrdersHistoryDelivererComponent;
  let fixture: ComponentFixture<OrdersHistoryDelivererComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersHistoryDelivererComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersHistoryDelivererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
