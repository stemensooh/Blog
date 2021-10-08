import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminLayoutRoutes } from './admin-layout.routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from '../../pages/home/home.component';
import { PostService } from '../../core/services/post.service';
import { ComponentsModule } from '../../components/components.module';
import { PostsComponent } from '../../pages/posts/posts.component';
import { PostsDetailComponent } from '../../pages/posts-detail/posts-detail.component';
import { AboutComponent } from '../../pages/about/about.component';
import { AuthInterceptor } from '../../core/services/auth-interceptor';

@NgModule({
  declarations: [
    HomeComponent,
    PostsComponent,
    PostsDetailComponent,
    AboutComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    ComponentsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },

    PostService
  ]
})
export class AdminLayoutModule { }
