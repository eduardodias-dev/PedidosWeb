import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/Core/models/User';
import { AuthorizeService } from 'src/app/Core/services/authorize.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  loading = false;
  formRegister: FormGroup;
  userLogin = new User();
  errorMessage: string;
  showError = false;
  constructor(private authorizeService: AuthorizeService, private router: Router) { }

  ngOnInit(): void {
    this.formRegister = new FormGroup({
      name: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    })
    
  }

  register(){
    if(this.formRegister.valid){
      
      this.loading = true;
      this.authorizeService.register(this.userLogin)
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
    }else{
      console.log(this.formRegister.errors);
      
    }
  }

}
