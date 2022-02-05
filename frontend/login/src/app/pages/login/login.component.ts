import { authenticationResponse, authentication } from './../../models/authentication';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  verifyForm: boolean = true;
  responseApi: authenticationResponse | undefined;

  constructor(private fb: FormBuilder,
    private serviceUser: UserService) {

    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', Validators.compose([
        Validators.minLength(8),
        Validators.required
      ])]
    });

  }

  ngOnInit(): void {


  }

  logar() {
    if (this.loginForm.valid) {
      console.log('logou man')

      console.log(this.loginForm['controls']['email'].value)

      let authSend: authentication = {
        Email: this.loginForm['controls']['email'].value,
        Senha: this.loginForm['controls']['password'].value
      };

      this.serviceUser.autenticacao(authSend).subscribe(
        response => {
          console.log(response)
        }, error => {
          console.log(error)
        })

    } else {
      this.verifyForm = false;
    }

    console.log('a')
  }

}
