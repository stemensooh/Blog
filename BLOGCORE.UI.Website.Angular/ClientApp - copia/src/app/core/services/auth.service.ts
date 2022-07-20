import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.prod';
import { Usuario } from '../models/usuario.model';
import { Subject } from 'rxjs';
import { LoginData } from '../models/login-data.model';
import { LoginResponse } from '../models/login-response';
import { ResultModel } from '../models/result.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as moment from 'moment';

const URL_POST = `${environment.urlApi}/account`;
const jwt = new JwtHelperService();

class DecodedToken {
  exp: number = 0;
  username: string = '';
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private token!: string;
  // private usuario: Usuario | null = null;
  private decodedToken;

  loginResponse!: LoginResponse;
  seguridadCambio = new Subject<boolean>();

  constructor(private router: Router, private http: HttpClient) {
    this.decodedToken = JSON.parse(localStorage.getItem('auth_meta') ?? '{}') || new DecodedToken();
  }

  cargarUsuario(): void {
    // debugger;
    let tokenValue = JSON.parse(localStorage.getItem('auth_data') ?? '{}').token ;
    if (!tokenValue) {
      return;
    }

    this.token = tokenValue;
    this.seguridadCambio.next(true);

    this.http
      .get<ResultModel>(URL_POST + '/Validate')
      .subscribe((response: ResultModel) => {
        this.loginResponse = response.data as LoginResponse;
        const usuario: Usuario = {
          email: this.loginResponse?.email ?? '',
          nombres: this.loginResponse.nombres,
          apellidos: this.loginResponse.apellidos,
          token: this.loginResponse.token,
          password: '',
          username: this.loginResponse.username,
          id: this.loginResponse.id,
        };

        this.saveToken(usuario);
        this.seguridadCambio.next(true);
      });
  }

  obtenerToken(): string {
    return this.token;
  }

  registrarUsuario(usr: Usuario): void {
    this.http
      .post<Usuario>(URL_POST + '/registrar', usr)
      .subscribe((response) => {
        this.token = response.token;
        // this.usuario = {
        //   email: response.email,
        //   nombres: response.nombres,
        //   apellidos: response.apellidos,
        //   token: response.token,
        //   password: '',
        //   username: response.username,
        //   id: response.id,
        // };
        this.seguridadCambio.next(true);
        // localStorage.setItem('token', response.token);
        this.router.navigate(['/']);
      });
  }

  login(loginData: LoginData): void {
    this.http
      .post<ResultModel>(URL_POST + '/SignIn', {
        Email: loginData.Email,
        Password: loginData.Password,
        Captcha: loginData.Captcha,
      })
      .subscribe(
        (response: ResultModel) => {
          this.loginResponse = response.data as LoginResponse;
          this.token = response.data.token;

          const usuario: Usuario = {
            email: this.loginResponse.email,
            nombres: this.loginResponse.nombres,
            apellidos: this.loginResponse.apellidos,
            token: this.loginResponse.token,
            password: '',
            username: this.loginResponse.username,
            id: this.loginResponse.id,
          };

          this.saveToken(usuario);
          this.seguridadCambio.next(true);
          // localStorage.setItem('token', this.token);
          this.router.navigate(['/']);
        }
      );
  }

  salirSesion() {
    // this.usuario = null;
    this.seguridadCambio.next(false);
    localStorage.removeItem('auth_data');
    localStorage.removeItem('auth_meta');
    // debugger;
    this.router.navigate(['/home']);
  }

  obtenerUsuario(): Usuario | null {
    return JSON.parse(localStorage.getItem('auth_data') ?? '{}') as Usuario;
  }

  onSesion() : boolean {
    // console.log(this.decodedToken.exp);
    return moment().isBefore(moment.unix(this.decodedToken.exp));
    // return this.usuario != null;
  }

  private saveToken(response: Usuario): any {
    // this.usuario = response;
    this.decodedToken = jwt.decodeToken(response.token);
    // console.log(this.decodedToken);

    localStorage.setItem('auth_data', JSON.stringify(response));
    localStorage.setItem('auth_meta', JSON.stringify(this.decodedToken));
    return response;
  }
}
