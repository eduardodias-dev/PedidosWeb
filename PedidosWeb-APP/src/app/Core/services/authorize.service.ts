import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/User';
import { UserToken } from '../models/UserToken';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  apiUrl= 'https://localhost:44323/api/authorize';
  options:{};
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  login(user: User){
    return this.http.post(this.apiUrl+"/login",user, this.options)
            .toPromise()
            .then(res=> <UserToken> res)
            .then(res=> res)
  }

  register(user: User){
    return this.http.post(this.apiUrl+"/register",user, this.options)
            .toPromise()
            .then(res=> <UserToken> res)
            .then(res=> res)
  }
}
