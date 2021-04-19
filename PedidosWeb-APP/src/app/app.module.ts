import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrdersComponent } from './Components/orders/orders.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { ProductsComponent } from './Components/products/products.component';
import { OrderitemsComponent } from './Components/orderitems/orderitems.component';
import { ClientsComponent } from './Components/clients/clients.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutBaseComponent } from './Components/layout-base/layout-base.component';


@NgModule({
  declarations: [
    AppComponent,
    OrdersComponent,
    CategoriesComponent,
    ProductsComponent,
    OrderitemsComponent,
    ClientsComponent,
    LoginComponent,
    RegisterComponent,
    LayoutBaseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
