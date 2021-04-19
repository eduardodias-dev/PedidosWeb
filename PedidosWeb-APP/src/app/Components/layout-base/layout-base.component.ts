import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout-base',
  templateUrl: './layout-base.component.html',
  styleUrls: ['./layout-base.component.css']
})
export class LayoutBaseComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  signOut(){
    localStorage.removeItem("tokenApplication");
    localStorage.removeItem("expirationToken");
    
    this.router.navigate(['/'])
  }

}
