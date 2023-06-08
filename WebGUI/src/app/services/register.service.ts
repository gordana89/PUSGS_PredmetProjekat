import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { RegisterModel } from '../models/registerModel';
import { NotificationService } from './notification.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  public userToRegister = {} as RegisterModel;
  constructor(
    private http: HttpClient,
    private notifyService: NotificationService,
    private router: Router,
    private datepipe: DatePipe
  ) {}

  registerUser(form: FormGroup, selectedFile): void {
    if (form.valid) {
      this.userToRegister.Username = form.value.username;
      this.userToRegister.address = form.value.address;
      this.userToRegister.dateOfBirth = this.datepipe.transform(
        form.value.dateOfBirth,
        'yyyy-MM-dd'
      );
      this.userToRegister.email = form.value.email;
      this.userToRegister.password = form.value.password;
      this.userToRegister.firstName = form.value.firstName;
      this.userToRegister.lastName = form.value.lastName;
      this.userToRegister.userType = form.value.userType;
      this.userToRegister.file = selectedFile;

      const formData = new FormData();

      formData.append('Username', this.userToRegister.Username);
      formData.append('FirstName', this.userToRegister.firstName);
      formData.append('LastName', this.userToRegister.lastName);
      formData.append('Address', this.userToRegister.address);
      formData.append('Email', this.userToRegister.email);
      formData.append('Password', this.userToRegister.password);
      formData.append('UserType', this.userToRegister.userType);
      formData.append('DateOfBirth', this.userToRegister.dateOfBirth);
      formData.append('File', this.userToRegister.file);

      this.http.post<any>(environment.users, formData).subscribe(
        (res) => {
          this.notifyService.showSuccess(
            'Registered successfully !!',
            'Delivery Store'
          );

          form.reset();
          this.router.navigate(['/login']);
          // window.location.href = 'login';
        },
        (err) => {
          this.notifyService.showError(
            `${err.error.Message}`,
            'Delivery Store'
          );
        }
      );
    } else if (form.invalid) alert('Form not valid');
  }
}
