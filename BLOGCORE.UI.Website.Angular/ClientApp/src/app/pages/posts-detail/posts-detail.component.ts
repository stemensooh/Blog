import { Component, OnInit } from '@angular/core';
import { PostService } from '../../core/services/post.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Post } from '../../core/models/post/post.model';
import { ComentarioService } from '../../core/services/comentario.service';
import { ComentarioModel } from 'src/app/core/models/post/comentario.model';

@Component({
  selector: 'app-posts-detail',
  templateUrl: './posts-detail.component.html',
  styleUrls: ['./posts-detail.component.scss'],
})
export class PostsDetailComponent implements OnInit {
  public post!: Post;
  public comentarios: ComentarioModel[] = [];

  constructor(private _postService: PostService, private rutaActiva: ActivatedRoute, private _comentarioService: ComentarioService) {
    

  }

  ngOnInit(): void {
    let id = this.rutaActiva.snapshot.params.id;

    this._postService.verPost(id).subscribe((data: any) => {
      this.post = data;
      console.log(data);
    });

    this._comentarioService.cargarComentariosPost(id).subscribe((data: any) => {
      this.comentarios = data;
    });
  }
}
