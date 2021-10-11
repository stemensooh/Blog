import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';
import { PostService } from './post.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class SweetAlert2Service {
  constructor(private _router: Router) {} // private _postService: PostService

  mensajeGenerico(
    titulo: string,
    mensaje: string,
    icon: SweetAlertIcon = 'success'
  ) {
    Swal.fire(titulo, mensaje, icon);
  }

  mensajeGenericoConfirmacion(
    titulo: string,
    mensaje: string,
    icon: SweetAlertIcon = 'success'
  ) {
    Swal.fire({
      title: titulo, 
      text: mensaje, 
      icon,
      confirmButtonColor: '#28a745',
      confirmButtonText: 'Ok',
    });
  }

  mensajeGenericoConfirmacionRedireccion(
    titulo: string,
    mensaje: string,
    icon: SweetAlertIcon = 'success',
    url: string = ''
  ) {
    Swal.fire({
      title: titulo,
      text: mensaje,
      icon: icon,
      confirmButtonColor: '#28a745',
      confirmButtonText: 'Ok',
    }).then((result) => {
      if (result.isConfirmed) {
        this._router.navigate([url]);
      }
    });
  }
}
