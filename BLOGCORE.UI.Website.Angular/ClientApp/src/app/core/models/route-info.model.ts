export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    childs: RouteInfo[];
    privado: boolean;
}