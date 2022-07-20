import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { HomeComponent } from './home/home.component';
import { PostsComponent } from './posts/posts.component';
import { PostsDetailComponent } from './posts-detail/posts-detail.component';
import { PostsFormComponent } from './posts-form/posts-form.component';
import { AboutComponent } from './about/about.component';
import { NoImage2Pipe } from '../core/pipes/no-image.pipe-2';
import { SearchComponent } from './search/search.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
// import { CKEditorModule } from 'ngx-ckeditor';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { ComponentsModule } from '../components/components.module';
import { ProfileService } from '../core/services/profile.service';
import { ProfileFormComponent } from './profile-form/profile-form.component';
import { ProfileComponent } from './profile/profile.component';
import { DropzoneModule, DROPZONE_CONFIG, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { TagsComponent } from './tags/tags.component';

import { RecaptchaModule } from "ng-recaptcha";
import { SettingsComponent } from './settings/settings.component';
import { SupportComponent } from './support/support.component';

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
    SearchComponent,
    ProfileComponent,
    ProfileFormComponent,
    TagsComponent,
    SettingsComponent,
    SupportComponent
  ],
  imports: [
    CKEditorModule,
    CommonModule,
    NgbModule,
    PagesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule,
    NgSelectModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    DropzoneModule,
    PagesRoutingModule,
    
  ],
  providers: [
    // PostService,
    ProfileService,
    { provide: DROPZONE_CONFIG, useValue: DEFAULT_DROPZONE_CONFIG },
  ]
})
export class PagesModule { }
