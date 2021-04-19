import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/Core/models/User';
import { AuthorizeService } from 'src/app/Core/services/authorize.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loading = false;
  formLogin: FormGroup;
  userLogin = new User();
  errorMessage: string;
  showError = false;
  constructor(private authorizeService: AuthorizeService, private router: Router) { }

  ngOnInit(): void {
    this.formLogin = new FormGroup({
      name: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    })
    
  }

  login(){
    if(this.formLogin.valid){
      
      this.loading = true;
      this.authorizeService.login(this.userLogin)
        .then(res => {
          localStorage.setItem("tokenApplication", res.token);
          localStorage.setItem("expirationToken", res.expiration.toString());
          
          this.router.navigate(['/'])
          
        }).catch(err=>{
          console.log(err);
          this.showError = true;
          this.errorMessage = err.error;
        })
        .finally(()=>{
          this.loading = false;
        })
    }
  }

}
