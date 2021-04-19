import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  apiUrl= 'https://localhost:44323/api/orders';
  options:{};
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  getOrders(){
    return this.http.get(this.apiUrl, this.options)
            .toPromise()
            .then(res=> <Order[]> res)
            .then(data => data)
  }
  
  getOrderById(id:number){
    return this.http.get(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> <Order> res)
            .then(data => data)
  }

  createOrder(order: Order){
    return this.http.post(this.apiUrl, order, this.options)
            .toPromise()
            .then(res=> <Order> res)
            .then(res=> res)
  }

  updateOrder(id:number, order: Order){
    return this.http.put(this.apiUrl+"/"+id, order, this.options)
            .toPromise()
            .then(res=> res)
  }
  
  deleteOrder(id:number){
    return this.http.delete(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> res)
  }
}
