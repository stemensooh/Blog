import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SweetAlert2Service } from './sweet-alert-2.service';
import { sourcePage } from '../utilities/my-configuration';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private _swal: SweetAlert2Service) {}

  // handleError(error: HttpErrorResponse, sourcePage: sourcePage) {
  //   switch (sourcePage) {
  //     case 'post-form':
  //       this.erroresPostForm(error);
  //       break;
  //     default:
  //       break;
  //   }

  //   if (error.status === 401) {
  //     this._swal.mensajeGenericoConfirmacionRedireccion(
  //       '',
  //       'Su sesión ha finalizado',
  //       'info',
  //       '/home'
  //     );
  //   }
  //   if (error.status === 403) {
  //     this._swal.mensajeGenericoConfirmacionRedireccion(
  //       '',
  //       'Usted no tiene permisos',
  //       'info',
  //       '/home'
  //     );
  //   }
  // }

  // erroresPostForm(error: HttpErrorResponse) {
  //   if (error.status === 400) {
  //     var errores = error.error.errors;
  //     if (errores.Categoria) {
  //       this.mostrarError(errores.Categoria);
  //     }
  //     if (errores.Titulo) {
  //       this.mostrarError(errores.Titulo);
  //     }
  //     if (errores.Cuerpo) {
  //       this.mostrarError(errores.Cuerpo);
  //     }
  //   }
  // }

  // mostrarError(errores: string[]) {
  //   errores.forEach((element: string) => {
  //     this._swal.mensajeGenerico('', element, 'error');
  //   });
  // }
}
