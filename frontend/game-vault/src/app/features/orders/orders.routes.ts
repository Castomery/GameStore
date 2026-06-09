import { Routes } from '@angular/router';

export const ORDERS_ROUTES: Routes = [
    {
        path: '',
        component: OrderListComponent,
    },
    {
        path: ':id',
        component: OrderDetailComponent,
    }
];