import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { TopbarComponent } from './topbar/topbar.component';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from './loader/loader.component';
import { TapToTopComponent } from './tap-to-top/tap-to-top.component';
import { FeatherIconsComponent } from '../components/feather-icons/feather-icons.component';
import { BreadcrumbComponent } from '../components/breadcrumb/breadcrumb.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { HeaderComponent } from './header/header.component';
import { NavbarComponent } from './navbar/navbar.component';



@NgModule({
  declarations: [
    FooterComponent,
    TopbarComponent,
    LoaderComponent,
    TapToTopComponent,
    MainLayoutComponent,
    AuthLayoutComponent,
    HeaderComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    FormsModule,
    RouterModule
  ],
  exports: [
    FooterComponent,
    TopbarComponent,
    LoaderComponent,
    TapToTopComponent,
  ]
})
export class SharedModule { }
