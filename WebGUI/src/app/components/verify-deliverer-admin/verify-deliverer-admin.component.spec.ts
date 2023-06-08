import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyDelivererAdminComponent } from './verify-deliverer-admin.component';

describe('VerifyDelivererAdminComponent', () => {
  let component: VerifyDelivererAdminComponent;
  let fixture: ComponentFixture<VerifyDelivererAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VerifyDelivererAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyDelivererAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
