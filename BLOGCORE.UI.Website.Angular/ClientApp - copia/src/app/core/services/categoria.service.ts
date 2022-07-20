import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoriaModel } from '../models/post/categoria.model';

const URL_POST = `${environment.urlApi}/categoria`;

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  constructor(private _http: HttpClient) {}

  getAll() {
    return this._http.get<CategoriaModel[]>(URL_POST);
  }
}
