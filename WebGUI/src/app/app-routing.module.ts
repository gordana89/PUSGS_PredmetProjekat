import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './components/basket/basket.component';
import { DelivererDeliverComponent } from './components/deliverer-deliver/deliverer-deliver.component';
import { LoginComponent } from './components/login/login.component';
import { OrdersAdminComponent } from './components/orders-admin/orders-admin.component';
import { OrdersHistoryClientComponent } from './components/orders-history-client/orders-history-client.component';
import { OrdersHistoryDelivererComponent } from './components/orders-history-deliverer/orders-history-deliverer.component';
import { OrdersToTakeDelivererComponent } from './components/orders-to-take-deliverer/orders-to-take-deliverer.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { StoreComponent } from './components/store/store.component';
import { VerifyDelivererAdminComponent } from './components/verify-deliverer-admin/verify-deliverer-admin.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'profile', component: ProfileComponent },
  { path: 'store', component: StoreComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'verifyDelivererAdmin', component: VerifyDelivererAdminComponent },
  { path: 'ordersAdmin', component: OrdersAdminComponent },
  { path: 'basket', component: BasketComponent },
  { path: 'orderHistoryClient', component: OrdersHistoryClientComponent },
  { path: 'ordersToTakeDeliverer', component: OrdersToTakeDelivererComponent },
  { path: 'delivererDeliver', component: DelivererDeliverComponent },
  {
    path: 'ordersHistoryDeliverer',
    component: OrdersHistoryDelivererComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
