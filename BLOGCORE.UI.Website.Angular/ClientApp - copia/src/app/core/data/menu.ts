import { RouteInfo } from "../models/route-info.model";

export const MENU: RouteInfo[] = [
    { path: '/home', title: 'Home',  icon: 'ni-tv-2 text-primary', class: '', childs: [], privado: false },
    { path: '/posts', title: 'Mis Posts',  icon: 'ni-tv-2 text-primary', class: '', childs: [], privado: true },
    { path: '/about', title: 'About',  icon:'ni-planet text-blue', class: '', childs: [], privado: false },
];