import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post/post.model';
import { Observable, of, Subject } from 'rxjs';
import { PostFormModel } from '../models/post/post-form.model';
import { tap } from 'rxjs/operators';
import { PostParametroModel } from '../models/post-parametro.model';

const URL_POST = `${environment.urlApi}/posts`;

@Injectable({
  providedIn: 'root',
})
export class PostService {
  public cargando: boolean = false;

  // httpOptions = {
  //   headers: new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Access-Control-Allow-Origin': '*',
  //   }),
  // };

  constructor(private _http: HttpClient) {
    console.log('I am pointing to web api: ' + URL_POST);
  }

  obtenerMisPosts(
    postParametroModel: PostParametroModel
    // sortOrder: string,
    // currentFilter: string,
    // searchString: string,
    // pageNumber: number = 1,
    // pageSize: number = 10
  ) {
    if (this.cargando) {
      return of([]);
    }
    this.cargando = true;

    let urlFinal = `${URL_POST}/MisPosts?sortOrder=${postParametroModel.sortOrder}&currentFilter=${postParametroModel.currentFilter}&searchString=${postParametroModel.searchString}&pageNumber=${postParametroModel.pageNumber}&pageSize=${postParametroModel.pageSize}`;
    return this._http.get<Post[]>(urlFinal).pipe(
      tap(() => {
        //this.carteleraPage += 1;
        this.cargando = false;
      })
    );
  }

  obtenerPosts(
    postParametroModel: PostParametroModel
  ) {
    if (this.cargando) {
      return of([]);
    }
    this.cargando = true;
    let urlFinal = `${URL_POST}?sortOrder=${postParametroModel.sortOrder}&currentFilter=${postParametroModel.currentFilter}&searchString=${postParametroModel.searchString}&pageNumber=${postParametroModel.pageNumber}&pageSize=${postParametroModel.pageSize}&profile=${postParametroModel.profile}`;
    return this._http.get<Post[]>(urlFinal).pipe(
      tap(() => {
        this.cargando = false;
      })
    );
  }

  consultarPostMasVisto() {
    let urlFinal = `${URL_POST}/VerPostsRecientes`;
    return this._http.get<Post[]>(urlFinal);
  }

  verPost(id: number) {
    let urlFinal = `${URL_POST}/VerPost/${id}`;
    return this._http.get<Post>(urlFinal);
  }

  registrarPostPost(form: PostFormModel) {
    let urlFinal = `${URL_POST}/Registrar`;
    console.log(form);
    return this._http.post<PostFormModel>(
      urlFinal, form
    );
  }

  registrarPostGet(Id: number) {
    let urlFinal = `${URL_POST}/Registrar/${Id}`;
    return this._http.get<PostFormModel>(urlFinal);
  }

  eliminar(Id: number) {
    let urlFinal = `${URL_POST}/EliminarPost/${Id}`;
    return this._http.delete(urlFinal);
  }
}
