import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.prod';
import { Usuario } from '../models/usuario.model';
import { Subject } from 'rxjs';
import { LoginData } from '../models/login-data.model';

const URL_POST = `${environment.urlApi}/account`;

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private token!: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin':'*'
    })
  }
  
  seguridadCambio = new Subject<boolean>();
  private usuario!: Usuario;

  cargarUsuario(): void {
    const tokenBrowser = localStorage.getItem('token');
    if (!tokenBrowser) {
      return;
    }

    this.token = tokenBrowser;
    this.seguridadCambio.next(true);

    this.http.get<Usuario>(URL_POST + '').subscribe((response) => {
      console.log('login respuesta', response);

      this.token = response.token;
      this.usuario = {
        email: response.email,
        nombre: response.nombre,
        apellido: response.apellido,
        token: response.token,
        password: '',
        username: response.username,
        usuarioId: response.usuarioId,
      };
      this.seguridadCambio.next(true);
      localStorage.setItem('token', response.token);
    });
  }

  obtenerToken(): string {
    // if(this.token){
      
    // }else{
    //   return localStorage.getItem('token') ?? '';
    // }
    return this.token;
  }

  constructor(private router: Router, private http: HttpClient) {}

  registrarUsuario(usr: Usuario): void {
    this.http
      .post<Usuario>(URL_POST + '/registrar', usr)
      .subscribe((response) => {
        this.token = response.token;
        this.usuario = {
          email: response.email,
          nombre: response.nombre,
          apellido: response.apellido,
          token: response.token,
          password: '',
          username: response.username,
          usuarioId: response.usuarioId,
        };
        this.seguridadCambio.next(true);
        localStorage.setItem('token', response.token);
        this.router.navigate(['/']);
      });
  }

  login(loginData: LoginData): void {

    this.http
      .post<Usuario>(URL_POST + '/SignIn', {
        Email: loginData.email,
        Password: loginData.password,
      }, this.httpOptions)
      .subscribe((response) => {
        console.log('login respuesta', response);

        this.token = response.token;
        this.usuario = {
          email: response.email,
          nombre: response.nombre,
          apellido: response.apellido,
          token: response.token,
          password: '',
          username: response.username,
          usuarioId: response.usuarioId,
        };
        this.seguridadCambio.next(true);
        localStorage.setItem('token', response.token);
        this.router.navigate(['/']);
      });
  }

  salirSesion() {
    // this.usuario = null;
    this.seguridadCambio.next(false);
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  obtenerUsuario() {
    return { ...this.usuario };
  }

  onSesion() {
    return this.usuario != null;
  }
}
