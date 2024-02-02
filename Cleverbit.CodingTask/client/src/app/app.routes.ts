import { Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {notLoggedGuard} from "./guards/not-logged.guard";
import {authGuard} from "./guards/auth.guard";
import {ProductlistComponent} from "./components/productlist/productlist.component";
import {ProductComponent} from "./components/product/product.component";

export const routes: Routes = [{
  path: '', redirectTo: 'productlist', pathMatch: 'full'
}, { path: 'products/:productId', component: ProductComponent
}, {
  path: 'productlist', component: ProductlistComponent, canActivate: [authGuard]
}, {
  path: 'login', component: LoginComponent, canActivate: [notLoggedGuard]
}, {
  path: '**', redirectTo: 'productlist'
}];
