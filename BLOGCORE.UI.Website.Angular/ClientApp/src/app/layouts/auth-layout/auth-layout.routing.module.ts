import { Routes } from "@angular/router";
import { SigninComponent } from '../../pages/signin/signin.component';

export const AuthLayoutRoutes: Routes = [
  {
    path: "",
    redirectTo: "signin",
    pathMatch: "full",
  },
  {
    path: "signin",
    component: SigninComponent,
  },
  
];
