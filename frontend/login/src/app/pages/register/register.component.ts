import { register, registerResponse } from './../../models/register';
import { UserService } from './../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  verifyForm: boolean = true;
  verifyPasswords: boolean = true;
  msgNivelSenha: string = "";
  barNivelPassword: string = "";
  senhaEscrita: string = "";
  showNivelPassword: boolean = false;
  verifySizePasswords: boolean = true;

  constructor(private fb: FormBuilder,
    private serviceUser: UserService) {

    this.registerForm = this.fb.group({
      nome: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.pattern(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]],
      password1: ['', Validators.compose([
        Validators.minLength(8),
        Validators.required
      ])],
      password2: ['', Validators.compose([
        Validators.minLength(8),
        Validators.required
      ])]
    });
  }

  ngOnInit(): void {
  }

  cadastrar() {
    this.verifyPasswords = this.verificaSenhasIdenticas();
    this.verifySizePasswords = this.verificaTamanhoSenha();

    if (this.registerForm.valid) {
      if (this.verifyPasswords) {

        let registerSend: register = {
          nome: this.registerForm['controls']['nome'].value,
          email: this.registerForm['controls']['email'].value,
          senha: this.registerForm['controls']['password1'].value
        };

        this.serviceUser.cadastrar(registerSend).subscribe({
          next: (resp: registerResponse) => {
            alert(resp.message);
            window.location.href = "http://localhost:4200";
          },
          complete: () => { },
          error: (error) => {
            alert(error.error.message);
          }
        });
      }
    } else {
      this.verifyForm = false;
    }
  }

  verificaSenhasIdenticas() {
    if (this.registerForm['controls']['password1'].value == this.registerForm['controls']['password2'].value) {
      return true;
    } else {
      return false;
    }
  }

  verificaTamanhoSenha() {
    if (this.registerForm['controls']['password1'].value.length >= 8) {
      return true;
    } else {
      return false;
    }
  }

  senhaDigitada(password: string) {

    let numeros = /([0-9])/;
    let alfabeto = /([a-zA-Z])/;
    let chEspeciais = /([~,!,@,#,$,%,^,&,*,-,_,+,=,?,>,<])/;

    password.length >= 1 ? this.showNivelPassword = true : this.showNivelPassword = false;

    if (password.length < 6) {
      this.barNivelPassword = "red";
      this.msgNivelSenha = "Fraca"
    } else {
      if (password.match(numeros) && password.match(alfabeto) && password.match(chEspeciais)) {
        this.barNivelPassword = "green";
        this.msgNivelSenha = "Forte"
      } else {
        this.barNivelPassword = "yellow";
        this.msgNivelSenha = "Médio, insira um caractere especial"
      }
    }
  }
}
