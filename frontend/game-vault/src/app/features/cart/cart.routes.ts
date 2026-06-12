import {Routes} from "@angular/router";
import { Cart } from "./components/cart/cart";
import { authGuard } from "../../core/guards/auth-guard";

export const CART_ROUTES:Routes = [
    {
        path:"",
        canActivate: [authGuard],
        component: Cart
    }
]