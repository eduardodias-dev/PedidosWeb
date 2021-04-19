import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoriesComponent } from './Components/categories/categories.component';
import { ClientsComponent } from './Components/clients/clients.component';
import { LoginComponent } from './Components/login/login.component';
import { OrdersComponent } from './Components/orders/orders.component';
import { ProductsComponent } from './Components/products/products.component';
import { RegisterComponent } from './Components/register/register.component';
import { AuthGuard } from './Core/guards/auth.guard';

const routes: Routes = [
  {path:"orders", component: OrdersComponent, canActivate:[AuthGuard]},
  {path:"clients", component: ClientsComponent, canActivate:[AuthGuard]},
  {path:"products", component: ProductsComponent, canActivate:[AuthGuard]},
  {path:"categories", component: CategoriesComponent, canActivate:[AuthGuard]},
  {path:"login", component: LoginComponent},
  {path:"register", component: RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
