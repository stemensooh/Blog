import { Routes } from "@angular/router";

export const AuthLayoutRoutes: Routes = [
  {
    path: "",
    redirectTo: "dashboard",
    pathMatch: "full",
  },
  {
    path: "dashboard",
    //component: DashboardComponent,
  },
  {
    path: "user-profile",
    //component: UserProfileComponent,
  },
  {
    path: "tables",
    //component: TablesComponent,
  },
  {
    path: "icons",
    //component: IconsComponent,
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
