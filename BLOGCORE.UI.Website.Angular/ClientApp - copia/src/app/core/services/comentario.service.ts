import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ComentarioModel } from '../models/post/comentario.model';
import { ResultModel } from '../models/result.model';
import { Observable } from 'rxjs';

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

  agregar(postId: number, nombreCompleto: string, email: string, comentario: string, comentarioPadreId: number, captcha: string): Observable<ResultModel>{
    const commet = {
      Email: email,
      Comentario: comentario,
      NombreCompleto: nombreCompleto,
      PostId: postId,
      ComentarioPadreId: comentarioPadreId,
      Captcha: captcha
    };
    // console.log(commet);
    return this._http.post<ResultModel>(URL_POST, commet);
  }
}
