import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post/post.model';
import { Observable, Subject } from 'rxjs';
import { PostFormModel } from '../models/post/post-form.model';

const URL_POST = `${environment.urlApi}/posts`;

@Injectable({
  providedIn: 'root',
})
export class PostService {
  private postsSubject = new Subject<Post[]>();
  private postsLista: Post[] = [];

  private postsMasVistoSubject = new Subject<Post[]>();
  private postsMasVisto!: Post[];

  public cargando: boolean = false;
  private pagina = 1;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    }),
  };

  constructor(private _http: HttpClient) {
    console.log('I am pointing to web api: ' + URL_POST);
  }

  obtenerMisPosts(
    sortOrder: string,
    currentFilter: string,
    searchString: string,
    pageNumber: number = 1,
    pageSize: number = 10
  ) {
    let urlFinal = `${URL_POST}/MisPosts?sortOrder=${sortOrder}&currentFilter=${currentFilter}&searchString=${searchString}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
    // this._http.get<Post[]>(urlFinal, options: { Headers: this.httpOptionsPost })
    this._http.get<Post[]>(urlFinal).subscribe((data: any) => {
      this.postsLista = data;
      this.postsSubject.next([...this.postsLista]);
    });
  }

  allMyPosts() {
    return this.postsSubject.asObservable();
  }

  // search(
  //   sortOrder: string,
  //   currentFilter: string,
  //   searchString: string,
  //   pageNumber: number = 1,
  //   pageSize: number = 5
  // ) : Observable<Post[]> {
  //   let urlFinal = `${URL_POST}?sortOrder=${sortOrder}&currentFilter=${currentFilter}&searchString=${searchString}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
  //   return this._http.get<Post[]>(urlFinal);
  // }

  obtenerPosts(
    sortOrder: string,
    currentFilter: string,
    searchString: string,
    pageNumber: number = 1,
    pageSize: number = 5
  ) {
    let urlFinal = `${URL_POST}?sortOrder=${sortOrder}&currentFilter=${currentFilter}&searchString=${searchString}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
    // console.log(pageSize);
    // this._http.get<Post[]>(urlFinal, options: { Headers: this.httpOptionsPost })
    return this._http.get<Post[]>(urlFinal);
    // .subscribe((data: any) => {
    //   this.postsLista = data;
    //   this.postsSubject.next([...this.postsLista]);
    // });
  }

  // all() {
  //   return this.postsSubject.asObservable();
  // }

  consultarPostMasVisto() {
    let urlFinal = `${URL_POST}/VerPostsRecientes`;
    this._http.get<Post[]>(urlFinal).subscribe((data: any) => {
      this.postsMasVisto = data;
      this.postsMasVistoSubject.next([...this.postsMasVisto]);
    });
  }

  mostViews() {
    return this.postsMasVistoSubject.asObservable();
  }

  verPost(id: number) {
    let urlFinal = `${URL_POST}/VerPost/${id}`;
    return this._http.get<Post>(urlFinal);
  }

  addPost(form: PostFormModel) {
    let urlFinal = `${URL_POST}/Registrar`;
    return this._http.post<PostFormModel>(urlFinal, form, this.httpOptions);
  }
}
