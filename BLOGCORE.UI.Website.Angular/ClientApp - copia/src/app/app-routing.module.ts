import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthLayoutComponent } from './shared/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { AccountModule } from './account/account.module';
import { PagesModule } from './pages/pages.module';

const routes: Routes =[
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        //canActivate: [AuthGuard],
        loadChildren: () => import('src/app/pages/pages.module').then(m => m.PagesModule)
      }
    ]
  }, 
  {
    path: 'account',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('src/app/account/account.module').then(m => m.AccountModule)
      }
    ]
  }, 
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top', useHash: true })],

  exports: [RouterModule]
})
export class AppRoutingModule { }
