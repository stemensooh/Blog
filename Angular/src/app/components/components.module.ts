import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from './post-card/post-card.component';
import { NoImagePipe } from '../core/pipes/no-image.pipe';



@NgModule({
  declarations: [PostCardComponent],
  imports: [
    CommonModule,
    // NoImagePipe
  ],
  exports: [
    PostCardComponent
  ]
})
export class ComponentsModule { }
