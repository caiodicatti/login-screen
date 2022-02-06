import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  nomeUsuario: string = "";
  textButton: string = "Voltar a tela inicial";
  autenticado: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.getStorage();
  }

  getStorage() {
    if (sessionStorage.getItem('autenticado') && sessionStorage.getItem('autenticado') == "true") {
      this.autenticado = true;
      this.textButton = "Deslogar";
      this.nomeUsuario = sessionStorage.getItem('nome')?.toString() || "";
    }
  }

  destroySession() {
    sessionStorage.setItem('autenticado', 'false');
    sessionStorage.removeItem('idUsuario');
    sessionStorage.removeItem('nome');
    sessionStorage.removeItem('email');

    window.location.href = "http://localhost:4200";
  }
}
