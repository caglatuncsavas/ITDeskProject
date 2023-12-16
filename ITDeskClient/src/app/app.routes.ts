import { Routes } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';

export const routes: Routes = [
    {
        path:"login",
        loadComponent: ()=> import("./components/login/login.component") // lazy loading
    },
    {
        path:"",
        component:LayoutComponent,
        canActivateChild:[()=> inject(AuthService).checkAuthentication()],
        children:[
            {
                path:"",
               loadComponent: ()=> import("./components/home/home.component") // lazy loading
            },
            {
                path:"ticket-details/:value",
                loadComponent:()=> import("./components/detail/detail.component")
            }
        ]
    }
];
