import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthLayoutComponent } from './auth-layout.component';
import { RouterModule } from '@angular/router';
import { AuthLayoutRoutes } from './auth-layout.routing.module';



@NgModule({
  declarations: [
 
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(AuthLayoutRoutes),
  ]
})
export class AuthLayoutModule { }
