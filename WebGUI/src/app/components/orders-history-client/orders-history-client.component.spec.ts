import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersHistoryClientComponent } from './orders-history-client.component';

describe('OrdersHistoryClientComponent', () => {
  let component: OrdersHistoryClientComponent;
  let fixture: ComponentFixture<OrdersHistoryClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersHistoryClientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersHistoryClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
