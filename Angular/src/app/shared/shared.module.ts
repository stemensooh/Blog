import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopbarComponent } from './topbar/topbar.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { AccountLayoutComponent } from './account-layout/account-layout.component';
import { HeaderComponent } from './header/header.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    TopbarComponent, 
    MainLayoutComponent, 
    AccountLayoutComponent, 
    HeaderComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([]),
  ],
  exports: [
    TopbarComponent, 
    MainLayoutComponent, 
    AccountLayoutComponent, 
    HeaderComponent
  ]
})
export class SharedModule { }
