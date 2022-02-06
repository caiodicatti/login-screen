import { register, registerResponse } from './../models/register';
import { authentication, authenticationResponse } from './../models/authentication';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  header = new HttpHeaders({ 'Content-Type': 'application/json' });
  urlBase = "https://localhost:5001/api/usuario";

  autenticacao(auth: authentication) {
    return this.http.post<authenticationResponse>(`${this.urlBase}/logar`, auth);
  }

  cadastrar(register: register) {
    return this.http.post<registerResponse>(`${this.urlBase}/cadastro`, register);
  }
}
