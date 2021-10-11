import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { DropzoneModule, DROPZONE_CONFIG, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { NgSelectModule } from '@ng-select/ng-select';


import { HomeComponent } from '../../pages/home/home.component';
import { ComponentsModule } from '../../components/components.module';
import { PostsFormComponent } from '../../pages/posts-form/posts-form.component';
import { PostsComponent } from '../../pages/posts/posts.component';
import { PostsDetailComponent } from '../../pages/posts-detail/posts-detail.component';
import { AboutComponent } from '../../pages/about/about.component';

import { PostService } from '../../core/services/post.service';
import { AuthInterceptor } from '../../core/services/auth-interceptor';

import { AdminLayoutRoutes } from './admin-layout.routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NoImage2Pipe } from '../../core/pipes/no-image.pipe-2';
import { SearchComponent } from '../../pages/search/search.component';

import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
  // Change this to your upload POST address:
  url: 'https://httpbin.org/post',
  acceptedFiles: 'image/*',
  createImageThumbnails: true
};

@NgModule({
  declarations: [
    HomeComponent,
    PostsComponent,
    PostsDetailComponent,
    PostsFormComponent,
    AboutComponent,
    NoImage2Pipe,
    SearchComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    NgSelectModule,
    ReactiveFormsModule,
    CKEditorModule,
    NgxDropzoneModule,
    DropzoneModule,

    
  ],
  providers: [
    { provide: DROPZONE_CONFIG, useValue: DEFAULT_DROPZONE_CONFIG },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    

    PostService
  ]
})
export class AdminLayoutModule { }
