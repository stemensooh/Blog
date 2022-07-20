import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as moment from 'moment';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Settings } from '../utilities/settings';
import { AuthDataModel } from '../models/auth-data.model';

const API_USERS_URL = `${environment.apiUrl}`;


const jwt = new JwtHelperService();

class DecodedToken {
  exp: number = 0;
  username: string = '';
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private uriseg = API_USERS_URL;
  private decodedToken;

  constructor(
    private router: Router,
    private http: HttpClient) {
    this.decodedToken = JSON.parse(localStorage.getItem(Settings.auth_meta) ?? '{}') || new DecodedToken();
   }

  public register(userData: any): Observable<any> {
    const URI = this.uriseg + '/register';
    return this.http.post(URI, userData);
  }

  public login(userData: any): Observable<any> {
    const URI = this.uriseg + '/Auth';
    return this.http.post<AuthDataModel>(URI, userData).pipe(map((response: AuthDataModel) => {
      return this.saveToken(response);
    }));
  }

  private saveToken(response: any): any {
    this.decodedToken = jwt.decodeToken(response.token);
    localStorage.setItem(Settings.auth_data, JSON.stringify(response));
    localStorage.setItem(Settings.auth_meta, JSON.stringify(this.decodedToken));
    return response;
  }

  public logout(): void {
    localStorage.removeItem(Settings.auth_data);
    localStorage.removeItem(Settings.auth_meta);
    this.decodedToken = new DecodedToken();
    this.router.navigate(['/account/login']);
  }

  public isAuthenticated(): boolean {
    console.log(this.decodedToken.exp);
    return moment().isBefore(moment.unix(this.decodedToken.exp));
  }
}