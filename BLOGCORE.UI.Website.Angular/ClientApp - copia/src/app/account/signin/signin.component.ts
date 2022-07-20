import { Component, OnInit } from '@angular/core';
import { Validators, NgForm, FormBuilder, FormGroup } from '@angular/forms';
import { MyConfiguration } from '../../core/utilities/my-configuration';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
})
export class SigninComponent implements OnInit {
  loginForm!: FormGroup;

  submitted = false;
  error = '';
  returnUrl!: string;
  tieneErrores: boolean = false;
  captchaToken: string = '';
  captchaSiteKey: string = environment.Recaptcha.ClaveSitioWeb;

  constructor(private _fb: FormBuilder, private _authService: AuthService, private _router: Router) {}

  ngOnInit(): void {

    // setTimeout(() => {
    //   if (this._authService.onSesion() === true) {
    //     this._router.navigate(['/home']);
    //   }
    // }, 2000);

    let email = '';
    let recordarme = /true/i.test(
      localStorage.getItem(MyConfiguration.FormLoginRecordarme) ?? ''
    );

    if (recordarme) {
      email = localStorage.getItem(MyConfiguration.FormLoginEmail) ?? '';
    }

    this.loginForm = this._fb.group({
      email: [email, [Validators.required, Validators.email]],
      password: ['12345678', [Validators.required]],
      recordarme: [recordarme],
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    } else {
      if (this.f.recordarme.value) {
        
        localStorage.setItem(
          MyConfiguration.FormLoginEmail,
          this.f.email.value
        );
        localStorage.setItem(
          MyConfiguration.FormLoginRecordarme,
          this.f.recordarme.value
        );
      } else {
        localStorage.removeItem(MyConfiguration.FormLoginEmail);
        localStorage.removeItem(MyConfiguration.FormLoginRecordarme);
      }

      this._authService.login({
        Email: this.f.email.value,
        Password: this.f.password.value,
        Captcha: this.captchaToken
      });
    }
  }

  resolved(captchaResponse: string) {
    this.captchaToken = captchaResponse;
  }
}
