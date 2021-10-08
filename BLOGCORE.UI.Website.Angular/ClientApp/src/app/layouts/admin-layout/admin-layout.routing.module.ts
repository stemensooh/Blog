import { Routes, CanActivate } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { HomeComponent } from '../../pages/home/home.component';
import { PostsComponent } from '../../pages/posts/posts.component';
import { PostsDetailComponent } from '../../pages/posts-detail/posts-detail.component';
import { AboutComponent } from '../../pages/about/about.component';


export const AdminLayoutRoutes: Routes = [
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
    path: "posts/:id",
    component: PostsDetailComponent,
  },
  {
    path: "maps",
    //component: MapsComponent,
  },
  {
    path: "ventas",
    //loadChildren: () => import("src/app/pages/ventas/ventas.module").then((m) => m.VentasModule),
  },
];
