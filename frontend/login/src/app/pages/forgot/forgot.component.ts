import { recoverResponse } from './../../models/recover';
import { UserService } from './../../services/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css']
})
export class ForgotComponent implements OnInit {

  forgotForm: FormGroup;
  verifyForm: boolean = true;
  mensagem: string = "";
  showDiv: string = "";

  constructor(private fb: FormBuilder,
    private serviceUser: UserService) {

    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]]
    });
  }

  ngOnInit(): void {
  }

  send() {

    if (this.forgotForm.valid) {

      let email: string = this.forgotForm['controls']['email'].value;

      this.serviceUser.recuperarsenha(email).subscribe({
        next: (resp: recoverResponse) => {
          alert(resp.message);

          if (resp.success) {
            window.location.href = "http://localhost:4200";
          }
        },
        complete: () => { },
        error: (error) => {
          this.mensagem = error.error.message;
        }
      });
    }
  }

  touchInputEmail(word: string) {
    console.log('aq => ' + word)
    if (this.mensagem != "") {
      if (word.length >= 3) {
        this.showDiv = "none";
        this.mensagem = "";
      } else {
        this.showDiv = "block";
      }
    }
  }
}
