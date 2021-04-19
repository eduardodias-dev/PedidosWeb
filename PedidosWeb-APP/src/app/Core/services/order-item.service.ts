import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderItem } from '../models/OrderItem';

@Injectable({
  providedIn: 'root'
})
export class OrderItemService {
  apiUrl= 'https://localhost:44323/api/orderItems';
  options:{};
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  getOrderItems(){
    return this.http.get(this.apiUrl, this.options)
            .toPromise()
            .then(res=> <OrderItem[]> res)
            .then(data => data)
  }
  
  getOrderItemById(id:number){
    return this.http.get(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> <OrderItem> res)
            .then(data => data)
  }

  createOrderItem(orderItem: OrderItem){
    return this.http.post(this.apiUrl,orderItem, this.options)
            .toPromise()
            .then(res=> <OrderItem> res)
            .then(res=> res)
  }

  updateOrderItem(id:number, orderItem: OrderItem){
    return this.http.put(this.apiUrl+"/"+id, orderItem, this.options)
            .toPromise()
            .then(res=> res)
  }
  
  deleteOrderItem(id:number){
    return this.http.delete(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> res)
  }
}
