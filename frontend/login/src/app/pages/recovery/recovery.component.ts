import { responseDefault } from './../../models/response';
import { recovery } from './../../models/recovery';
import { UserService } from './../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-recovery',
  templateUrl: './recovery.component.html',
  styleUrls: ['./recovery.component.css']
})
export class RecoveryComponent implements OnInit {

  recoveryForm: FormGroup;
  code: string = "";
  verifyForm: boolean = true;
  verifyPasswords: boolean = true;
  verifySizePasswords: boolean = true;
  showNivelPassword: boolean = false;
  barNivelPassword: string = "";
  msgNivelSenha: string = "";
  messageReturn: string = "";
  backgroundMessageReturn: string = "";
  formShow: boolean = true;

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private serviceUser: UserService) {

    this.recoveryForm = this.fb.group({
      password1: ['', Validators.compose([
        Validators.minLength(8),
        Validators.required
      ])],
      password2: ['', Validators.compose([
        Validators.minLength(8),
        Validators.required
      ])]
    });

    this.route.params.subscribe(params => this.code = params['code']);
  }

  ngOnInit(): void {
  }

  send() {
    this.verifyPasswords = this.verificaSenhasIdenticas();
    this.verifySizePasswords = this.verificaTamanhoSenha();

    if (this.recoveryForm.valid) {
      if (this.verifyPasswords) {

        let recoverySend: recovery = {
          Email: "",
          Senha: this.recoveryForm['controls']['password1'].value,
          Code: this.code
        };

        this.serviceUser.linkpassword(recoverySend).subscribe({
          next: (resp: responseDefault) => {
            this.formShow = false;
            this.messageReturn = resp.message;
            this.backgroundMessageReturn = "div-alert-success";
          },
          complete: () => { },
          error: (error) => {
            this.formShow = false;
            this.messageReturn = error.error.message;
            this.backgroundMessageReturn = "div-alert-warning";
          }
        });
      }
    } else {
      this.verifyForm = false;
    }
  }

  verificaSenhasIdenticas() {
    if (this.recoveryForm['controls']['password1'].value == this.recoveryForm['controls']['password2'].value) {
      return true;
    } else {
      return false;
    }
  }

  verificaTamanhoSenha() {
    if (this.recoveryForm['controls']['password1'].value.length >= 8) {
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

  return() {
    window.location.href = "http://localhost:4200";
  }
}
