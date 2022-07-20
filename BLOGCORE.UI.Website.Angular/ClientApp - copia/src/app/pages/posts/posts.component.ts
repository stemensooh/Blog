import { Component, OnDestroy, OnInit, HostListener } from '@angular/core';
import { Subscription } from 'rxjs';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { SweetAlert2Service } from '../../core/services/sweet-alert-2.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent implements OnInit, OnDestroy {
  public posts: Post[] = [];
  private pageNumber: number = 1;

  constructor(
    private _swal: SweetAlert2Service,
    private _postService: PostService,
    private _route: Router
  ) {}
  ngOnDestroy(): void {}

  ngOnInit(): void {
    this.consultarPosts(true);
  }

  nuevoPost() {
    this._route.navigate(['/posts/add']);
  }

  eliminarPost(Id: number) {
    Swal.fire({
      title: 'Eliminar registro',
      text: '¡No podrás revertir esto!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#28a745',
      confirmButtonText: 'Eliminar',
      cancelButtonColor: '#dc3545',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this._postService.eliminar(Id).subscribe((data: any) => {
          if (data.estado) {
            this.posts = this.posts.filter((x) => x.id != Id);
            this._swal.mensajeGenericoConfirmacion('', data.mensaje, 'success');
          } else {
            this._swal.mensajeGenericoConfirmacion('', data.mensaje, 'error');
          }
        });
      }
    });
  }

  @HostListener('window:scroll', ['$event'])
  onScroll() {
    const pos =
      (document.documentElement.scrollTop || document.body.scrollTop) + 1300;
    const max =
      document.documentElement.scrollHeight || document.body.scrollHeight;

    if (pos > max) {
      if (this._postService.cargando) {
        return;
      }
      this.pageNumber += 1;
      this.consultarPosts(true);
    }
  }

  private consultarPosts(cargar: boolean){
    this._postService
    .obtenerMisPosts({
        sortOrder: 'date_desc', 
        currentFilter: '',
        searchString: '',
        pageNumber: this.pageNumber,
        pageSize: 8,
        profile: '',
        categoria: ''
      })
    .subscribe((data) => {
      if (cargar){
        this.posts.push(...data);
      }else{
        this.posts = data;
      }
    });
  }
}
