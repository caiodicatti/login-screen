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
    return this.http.post<any>(`${this.urlBase}/logar`, auth);
  }
}
