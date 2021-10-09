import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ComentarioModel } from '../models/post/comentario.model';

const URL_POST = `${environment.urlApi}/categoria`;

@Injectable({
  providedIn: 'root',
})
export class ComentarioService {
  constructor(private _http: HttpClient) {}

  cargarComentariosPost(id: number) {
    return this._http.get<ComentarioModel[]>(URL_POST);
  }
}
