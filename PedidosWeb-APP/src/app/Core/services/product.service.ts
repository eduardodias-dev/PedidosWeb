import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl= 'https://localhost:44323/api/products';
  options:{};
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  getProducts(){
    return this.http.get(this.apiUrl, this.options)
            .toPromise()
            .then(res=> <Product[]> res)
            .then(data => data)
  }
  
  getProductById(id:number){
    return this.http.get(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> <Product> res)
            .then(data => data)
  }

  createProduct(product: Product){
    return this.http.post(this.apiUrl, product, this.options)
            .toPromise()
            .then(res=> <Product> res)
            .then(res=> res)
  }

  updateProduct(id:number, product: Product){
    return this.http.put(this.apiUrl+"/"+id, product, this.options)
            .toPromise()
            .then(res=> res)
  }
  
  deleteProduct(id:number){
    return this.http.delete(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> res)
  }
}
