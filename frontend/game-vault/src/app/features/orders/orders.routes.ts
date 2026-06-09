import { Routes } from '@angular/router';
import { OrderList } from './components/order-list/order-list';
import { OrderDetail } from './components/order-detail/order-detail';

export const ORDERS_ROUTES: Routes = [
    {
        path: '',
        component: OrderList,
    },
    {
        path: ':id',
        component: OrderDetail,
    }
];