import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from './post-card/post-card.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { RouterModule } from '@angular/router';
import { FeatherIconsComponent } from './feather-icons/feather-icons.component';



@NgModule({
  declarations: [
    PostCardComponent,
    
    
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    PostCardComponent,
  ]
})
export class ComponentsModule { }
