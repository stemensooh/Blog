import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { TipoRedSocialModel } from '../models/profile/tipo-red-social.model';
import { RedesSocialesModel } from '../models/profile/redes-sociales.model';

const URL_POST = `${environment.urlApi}/perfil`;

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private _http: HttpClient) {
    console.log('I am pointing to web api: ' + URL_POST);
  }

  verPerfil(username: string) {
    let urlFinal = `${URL_POST}/Ver/${username}`;
    return this._http.get(urlFinal);
  }

  verMiPerfil() {
    return this._http.get(URL_POST);
  }

  misRedesSociales(username: string) {
    return this._http.get<RedesSocialesModel[]>(URL_POST + `/MisRedes/${username}`);
  }

  tipoRedSocial() {
    return this._http.get<TipoRedSocialModel[]>(URL_POST + '/TipoRedSocial');
  }
}
