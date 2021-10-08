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



@NgModule({
  declarations: [
    FooterComponent,
    TopbarComponent,
    LoaderComponent,
    TapToTopComponent,
    FeatherIconsComponent,
    BreadcrumbComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    RouterModule
  ],
  exports: [
    FooterComponent,
    TopbarComponent,
    LoaderComponent,
    TapToTopComponent,
    FeatherIconsComponent,
    BreadcrumbComponent
  ]
})
export class SharedModule { }
