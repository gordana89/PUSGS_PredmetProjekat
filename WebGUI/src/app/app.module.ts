import { UsersService } from './services/users.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MY_DATE_FORMATS } from './helpers/datetime.validator';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ToastrModule } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';

import { HttpClientModule } from '@angular/common/http';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from './components/dialog/dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './components/profile/profile.component';
import { StoreComponent } from './components/store/store.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { VerifyDelivererAdminComponent } from './components/verify-deliverer-admin/verify-deliverer-admin.component';
import { OrdersAdminComponent } from './components/orders-admin/orders-admin.component';
import { BasketComponent } from './components/basket/basket.component';
import { OrdersHistoryClientComponent } from './components/orders-history-client/orders-history-client.component';
import { OrdersHistoryDelivererComponent } from './components/orders-history-deliverer/orders-history-deliverer.component';
import { OrdersToTakeDelivererComponent } from './components/orders-to-take-deliverer/orders-to-take-deliverer.component';
import { DelivererDeliverComponent } from './components/deliverer-deliver/deliverer-deliver.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    DialogComponent,
    ProfileComponent,
    StoreComponent,
    LoginComponent,
    RegisterComponent,
    VerifyDelivererAdminComponent,
    OrdersAdminComponent,
    BasketComponent,
    OrdersHistoryClientComponent,
    OrdersHistoryDelivererComponent,
    OrdersToTakeDelivererComponent,
    DelivererDeliverComponent,
    EditProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatGridListModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
    }),
  ],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS },
    DatePipe,
    UsersService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
