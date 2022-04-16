import { Component } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  userNameField: AbstractControl;
  passwordField: AbstractControl;
  errorMessages: {
    required: string,
    minlength: string,
    maxlength: string
  }

  constructor(
    private authService: AuthService,
    private builder: FormBuilder,
    private router: Router
  ) {
    this.loginForm = this.builder.group({
      username: ['',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20)
        ]],
      password: ['',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20)
        ]]
    });
    this.errorMessages = {
      required: 'You should fill this field.',
      minlength: 'Not enough symbols entered.',
      maxlength: 'You entered too many symbols.'
    };
    this.userNameField = this.loginForm.controls['username'];
    this.passwordField = this.loginForm.controls['password'];
  }

  login(value: any) {
    this.authService.login(value.username, value.password)
      .subscribe(data => {
        localStorage.setItem('jwt', data.jwt);
        this.router.navigateByUrl('/');
      });
  }

  logErrors(control: AbstractControl): string {
    let result: string = '';
    if (control && control.errors) {
      Object.keys(control.errors).forEach((key: string) => {
        if (!control.touched && key !== 'required') {
          result += (this.errorMessages as any)[key] + '\n';
        }
      });
    }
    return result;
  }
}
