import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from './post-card/post-card.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { RouterModule } from '@angular/router';
import { FeatherIconsComponent } from './feather-icons/feather-icons.component';
import { NoImagePipe } from '../core/pipes/no-image.pipe';
import { CommentsComponent } from './comments/comments.component';



@NgModule({
  declarations: [
    PostCardComponent,
    NoImagePipe,
    CommentsComponent
    
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    PostCardComponent,
    CommentsComponent
  ]
})
export class ComponentsModule { }
