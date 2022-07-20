import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

const URL_POST = `${environment.urlApi}/perfil`;

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private _http: HttpClient) {
    console.log('I am pointing to web api: ' + URL_POST);
   }

   verPerfil(username: string){
     let urlFinal = `${URL_POST}/Ver/${username}`;
    return this._http.get(urlFinal);
   }

   verMiPerfil(){
     return this._http.get(URL_POST);
   }
}
