import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostsComponent } from './posts/posts.component';
import { AuthGuard } from '../core/guards/auth.guard';
import { PostsFormComponent } from './posts-form/posts-form.component';
import { PostsDetailComponent } from './posts-detail/posts-detail.component';
import { SearchComponent } from './search/search.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileFormComponent } from './profile-form/profile-form.component';
import { AboutComponent } from './about/about.component';
import { TagsComponent } from './tags/tags.component';
import { SettingsComponent } from './settings/settings.component';
import { SupportComponent } from './support/support.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "home",
    pathMatch: "full",
  },
  {
    path: "home",
    component: HomeComponent,
  },
  {
    path: "about",
    component: AboutComponent,
  },
  {
    path: "posts",
    component: PostsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "posts/add",
    component: PostsFormComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "posts/edit/:id",
    component: PostsFormComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "posts/:id",
    component: PostsDetailComponent,
  },
  {
    path: "search/:text",
    component: SearchComponent,
  },
  {
    path: "profile/:username",
    component: ProfileComponent,
  },
  {
    path: "profile/edit/",
    component: ProfileFormComponent,
  },
  {
    path: 'tags/:text',
    component: TagsComponent
  },
  {
    path: 'tags',
    component: TagsComponent
  },
  {
    path: 'settings',
    component: SettingsComponent
  },
  {
    path: 'support',
    component: SupportComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
