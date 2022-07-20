// import {
//   HttpHandler,
//   HttpInterceptor,
//   HttpRequest,
// } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { AuthService } from '../services/auth.service';


// @Injectable({
//   providedIn: 'root'
// })
// export class AuthInterceptorService implements HttpInterceptor {
//   constructor(private seguridadService: AuthService) {}

//   intercept(req: HttpRequest<any>, next: HttpHandler) {
//     const tokenSeguridad = this.seguridadService.obtenerToken();
//     const request = req.clone({
//       headers: req.headers.set('Authorization', 'Bearer ' + tokenSeguridad),
//     });

//     return next.handle(request);
//   }
// }
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { timeout, catchError } from 'rxjs/operators';
import { MyConfiguration } from '../utilities/my-configuration';
import Swal from 'sweetalert2';
import { tap } from 'rxjs/operators';
import { map } from 'rxjs/operators';
import { finalize } from 'rxjs/operators';
import { environment } from '../../../environments/environment.prod';
import { IniciarLoading, DetenerLoading } from '../utilities/loading';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
    // begin
    IniciarLoading();
    let tokenValue = localStorage.getItem('token') ?? "";// JSON.parse(localStorage.getItem(MyConfiguration.auth_data) ?? '{}').token;

    if (tokenValue) {
      request = request.clone({
        setHeaders: {
          // 'origin': 'http://localhost:4201',
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Authorization': 'Bearer ' + tokenValue,
          // 'Access-Control-Allow-Origin' : '*',
          // 'Access-Control-Allow-Credentials': 'true',
          // 'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
        }
      });
    }

    // console.log('request', request);
    return next.handle(request)
    // .catch((err: HttpErrorResponse) => {

    //   if (err.error instanceof Error) {
    //     // A client-side or network error occurred. Handle it accordingly.
    //     console.error('An error occurred:', err.error.message);
    //   } else {
    //     // The backend returned an unsuccessful response code.
    //     // The response body may contain clues as to what went wrong,
    //     console.error(`Backend returned code ${err.status}, body was: ${err.error}`);
    //   }

    //   // ...optionally return a default fallback value so app can continue (pick one)
    //   // which could be a default value (which has to be a HttpResponse here)
    //   // return Observable.of(new HttpResponse({body: [{name: "Default value..."}]}));
    //   // or simply an empty observable
    //   return Observable.empty<HttpEvent<any>>();
    // });
    .pipe(
      // tap((response: any) => {
      //   console.log(response);
        
      //   return ;
      // }),
      catchError(this.handleError),
      finalize(() => {
        DetenerLoading();
      }));
  }

  private handleError(error: HttpErrorResponse) {
    let mensaje = '';
    // DetenerLoading();
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      mensaje = 'Ocurrió un error: '+ error.error;
    } 
    else if (error.status === 401){
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      mensaje = 'No tiene acceso a este contenido: ' + error.status + ' [Unauthorized]';
    }
    else if (error.status === 403){
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      mensaje = 'No posee los permisos necesarios para este contenido: ' + error.status + ' [Forbidden]';
    }
    else{
      if (environment.production){
        mensaje = 'Algo malo sucedio; Por favor, inténtelo de nuevo más tarde.';
      }else{
        mensaje = `Código devuelto de back-end ${error.status}, el contenido es: ${error.error}`;
      }
    }
    // console.log('handleError', error);
    
    Swal.fire({
      icon: 'error',
      title: '',
      html: mensaje,
    });
    // Return an observable with a user-facing error message.
    return throwError(() => new Error());
  }

}