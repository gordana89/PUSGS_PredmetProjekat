import { NotificationService } from './notification.service';
import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/loginModel';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import jwt_decode from 'jwt-decode';
import { UserModel } from '../models/userModel';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public loginCredentials = {} as LoginModel;
  public user = {} as UserModel;

  constructor(
    private notifyService: NotificationService,
    private router: Router,
    private http: HttpClient
  ) {}

  loginUser(form: FormGroup) {
    if (form.valid) {
      this.loginCredentials.email = form.value.email;
      this.loginCredentials.password = form.value.password;

      this.http
        .post<any>(environment.login_user, this.loginCredentials)
        .subscribe(
          (res) => {
            this.user = jwt_decode(res.token);
            localStorage.setItem('user', JSON.stringify(this.user));
            localStorage.setItem('token', JSON.stringify(res.token));
            this.notifyService.showSuccess(
              'Loged in successfully !!',
              'Delivery Store'
            );

            form.reset();
            // this.router.navigate(['/store']);
            window.location.href = 'store';
          },
          (err) => {
            this.notifyService.showError(
              `Error: ${err.error.Message}`,
              'Delivery Store'
            );
          }
        );
    } else if (form.invalid) alert('Form not valid');
  }

  logoutUser(): void {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    localStorage.clear();
    // this.router.navigate(['/login']);
    window.location.href = 'login';
  }
}
