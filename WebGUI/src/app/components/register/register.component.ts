import { RegisterService } from './../../services/register.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MustMatch } from '../../helpers/must-match.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  public selectedFile!: File;

  constructor(private fb: FormBuilder, private service: RegisterService) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        username: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        address: ['', [Validators.required]],
        userType: ['', [Validators.required]],
        dateOfBirth: ['', [Validators.required]],
        password: ['', [Validators.required, Validators.minLength(3)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(3)]],
        file: [''],
      },
      {
        validator: MustMatch('password', 'confirmPassword'),
      }
    );
  }
  // convenience getter for easy access to form fields
  get f() {
    return this.registerForm.controls;
  }

  register(): void {
    this.service.registerUser(this.registerForm, this.selectedFile);
  }

  onFileChanged(event: Event) {
    const target = event.target as HTMLInputElement;
    this.selectedFile = target.files[0];
  }
}
