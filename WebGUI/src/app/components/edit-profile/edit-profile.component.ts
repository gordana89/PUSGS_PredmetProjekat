import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss'],
})
export class EditProfileComponent implements OnInit {
  public selectedFile!: File;
  public editProfileForm!: FormGroup;
  user = JSON.parse(localStorage.getItem('user') || '{}');
  public req = new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token')),
  });

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private notifyService: NotificationService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    this.editProfileForm = this.formBuilder.group({
      username: [this.data.userEditInfo.username, Validators.required],
      email: [this.data.userEditInfo.email, Validators.required],
      firstName: [this.data.userEditInfo.firstName, Validators.required],
      lastName: [this.data.userEditInfo.lastName, Validators.required],
      dateOfBirth: [this.data.userEditInfo.dateOfBirth, Validators.required],
      address: [this.data.userEditInfo.address, Validators.required],
      userType: [this.data.userEditInfo.userType, Validators.required],
      file: [this.data.userEditInfo.file],
    });
  }
  onFileChanged(event: Event) {
    const target = event.target as HTMLInputElement;
    this.selectedFile = target.files[0];
  }

  submitEditProfile(): void {
    if (this.editProfileForm.value.address == (null || '')) {
      this.notifyService.showWarning(
        `You need to enter address!`,
        'Delivery Store'
      );
      return;
    }
    if (this.editProfileForm.value.firstName == (null || '')) {
      this.notifyService.showWarning(
        `You need to enter firstname!`,
        'Delivery Store'
      );
      return;
    }
    if (this.editProfileForm.value.lastName == (null || '')) {
      this.notifyService.showWarning(
        `You need to enter last name!`,
        'Delivery Store'
      );
      return;
    }
    if (this.editProfileForm.value.dateOfBirth == (null || '')) {
      this.notifyService.showWarning(
        `You need to enter birthdate!`,
        'Delivery Store'
      );
      return;
    }

    this.data.userEditInfo.file = this.selectedFile;

    this.editProfileForm.value.file = this.selectedFile;
    const formData = new FormData();

    formData.append('Id', this.user.userID);
    formData.append('Username', this.editProfileForm.value.username);
    formData.append('Address', this.editProfileForm.value.address);
    formData.append('DateOfBirth', this.editProfileForm.value.dateOfBirth);
    formData.append('Email', this.editProfileForm.value.email);
    formData.append('FirstName', this.editProfileForm.value.firstName);
    formData.append('LastName', this.editProfileForm.value.lastName);
    formData.append('UserType', this.editProfileForm.value.userType);
    formData.append('File', this.editProfileForm.value.file);

    this.http
      .put<any>(
        `https://localhost:5001/api/Users/${this.user.userID}`,
        formData,
        {
          headers: this.req,
        }
      )
      .subscribe(
        (res) => {
          console.log('Success: ', res);
          window.location.reload();
        },
        (err) => {
          console.log('The error is: ', err);
          window.location.reload();
        }
      );
  }
}
