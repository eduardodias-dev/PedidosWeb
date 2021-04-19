import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from '../models/Client';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  apiUrl= 'https://localhost:44323/api/clients';
  options:{};
  
  constructor(private http: HttpClient) { 
    this.options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('tokenApplication')
      })
    }
  }

  getClients(){
    return this.http.get(this.apiUrl, this.options)
            .toPromise()
            .then(res=> <Client[]> res)
            .then(data => data)
  }
  
  getClientById(id:number){
    return this.http.get(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> <Client> res)
            .then(data => data)
  }

  createClient(client: Client){
    return this.http.post(this.apiUrl,client, this.options)
            .toPromise()
            .then(res=> <Client> res)
            .then(res=> res)
  }

  updateClient(id:number, client: Client){
    return this.http.put(this.apiUrl+"/"+id, client, this.options)
            .toPromise()
            .then(res=> res)
  }
  
  deleteClient(id:number){
    return this.http.delete(this.apiUrl+"/"+id, this.options)
            .toPromise()
            .then(res=> res)
  }
}
