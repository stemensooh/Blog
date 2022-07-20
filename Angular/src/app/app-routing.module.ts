import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { AccountLayoutComponent } from './shared/account-layout/account-layout.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  { 
    path: 'account', 
    component: AccountLayoutComponent,
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule) 
  },
  { 
    path: '', 
    component: MainLayoutComponent,
    loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule), 
    // canActivate: [AuthGuard]
   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top', useHash: true })],
  exports: [RouterModule],
  providers: [
    
  ]
})
export class AppRoutingModule {}
