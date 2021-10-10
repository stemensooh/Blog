import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ComentarioModel } from '../models/post/comentario.model';

const URL_POST = `${environment.urlApi}/comentario`;

@Injectable({
  providedIn: 'root',
})
export class ComentarioService {
  constructor(private _http: HttpClient) {}

  cargarComentariosPost(id: number) {
    let url = URL_POST + "/" + id;
    return this._http.get<ComentarioModel[]>(url);
  }
}
