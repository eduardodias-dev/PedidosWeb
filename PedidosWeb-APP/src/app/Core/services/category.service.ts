import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl= 'https://localhost:44323/api/categories';
  options:{};
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  getCategories(){
    return this.http.get(this.apiUrl, this.options)
            .toPromise()
            .then(res=> <Category[]> res)
            .then(data => data)
  }
  
  getCategoryById(id:number){
    return this.http.get(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> <Category> res)
            .then(data => data)
  }

  createCategory(category: Category){
    return this.http.post(this.apiUrl,category, this.options)
            .toPromise()
            .then(res=> <Category> res)
            .then(res=> res)
  }

  updateCategory(id:number, category: Category){
    return this.http.put(this.apiUrl+"/"+id, category, this.options)
            .toPromise()
            .then(res=> res)
  }
  
  deleteCategory(id:number){
    return this.http.delete(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> res)
  }

}
