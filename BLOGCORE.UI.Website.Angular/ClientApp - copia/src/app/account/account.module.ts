import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SigninComponent } from './signin/signin.component';
import { AccountRoutingModule } from './account-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';
import { RecaptchaModule } from 'ng-recaptcha';



@NgModule({
  declarations: [
    SigninComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RecaptchaModule
  ],
  providers:[
    AuthService
  ]
})
export class AccountModule { }
