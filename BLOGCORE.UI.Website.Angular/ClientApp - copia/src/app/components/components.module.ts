import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from './post-card/post-card.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { RouterModule } from '@angular/router';
import { FeatherIconsComponent } from './feather-icons/feather-icons.component';
import { NoImagePipe } from '../core/pipes/no-image.pipe';
import { CommentsComponent } from './comments/comments.component';
import { CommentsFormComponent } from './comments-form/comments-form.component';
import { PostRecentComponent } from './post-recent/post-recent.component';
import { LoadingCssComponent } from './loading-css/loading-css.component';
import { TagComponent } from './tag/tag.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RecaptchaModule } from 'ng-recaptcha';
import { RECAPTCHA_V3_SITE_KEY, RecaptchaV3Module } from "ng-recaptcha";
import { environment } from '../../environments/environment.prod';
import { SocialNetworkComponent } from './social-network/social-network.component';
import { UrlMailToPipe } from '../core/pipes/url-mail-to.pipe';



@NgModule({
  declarations: [
    PostCardComponent,
    NoImagePipe,
    UrlMailToPipe,
    CommentsComponent,
    BreadcrumbComponent,
    FeatherIconsComponent,
    CommentsFormComponent,
    PostRecentComponent,
    TagComponent,
    LoadingCssComponent,
    SocialNetworkComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    RecaptchaModule,
    // RecaptchaV3Module
  ],
  exports: [
    PostCardComponent,
    CommentsComponent,
    BreadcrumbComponent,
    FeatherIconsComponent,
    CommentsFormComponent,
    PostRecentComponent,
    TagComponent,
    LoadingCssComponent,
    SocialNetworkComponent
  ],
  // providers: [{ provide: RECAPTCHA_V3_SITE_KEY, useValue: environment.Recaptcha.ClaveSitioWeb }],
})
export class ComponentsModule { }
