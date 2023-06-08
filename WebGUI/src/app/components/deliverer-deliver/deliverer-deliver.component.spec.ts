import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DelivererDeliverComponent } from './deliverer-deliver.component';

describe('DelivererDeliverComponent', () => {
  let component: DelivererDeliverComponent;
  let fixture: ComponentFixture<DelivererDeliverComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DelivererDeliverComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DelivererDeliverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
