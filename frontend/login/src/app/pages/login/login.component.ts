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
  mensagem: string = "";

  constructor(private fb: FormBuilder,
    private serviceUser: UserService) {

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]],
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

      let authSend: authentication = {
        Email: this.loginForm['controls']['email'].value,
        Senha: this.loginForm['controls']['password'].value
      };

      /* this.serviceUser.autenticacao(authSend).subscribe(
        response => {
          console.log(response)
        }, error => {
          console.log(error)
        }) */

      this.serviceUser.autenticacao(authSend).subscribe({
        next: (resp: authenticationResponse) => {
          this.mensagem = resp.message;
          sessionStorage.setItem('autenticado', 'true');
          sessionStorage.setItem('idUsuario', resp.result.idUsuario.toString());
          sessionStorage.setItem('nome', resp.result.nome);
          sessionStorage.setItem('email', resp.result.email);

          window.location.href = "http://localhost:4200/home";
        },
        complete: () => { },
        error: (error) => {
          this.mensagem = error.error.message;
        }
      });

    } else {
      this.verifyForm = false;
    }
  }

}
