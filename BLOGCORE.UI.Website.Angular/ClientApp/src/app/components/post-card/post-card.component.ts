import { Component, Input, OnInit } from '@angular/core';
import { Post } from '../../core/models/post/post.model';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import Swal from 'sweetalert2';
import { SweetAlert2Service } from '../../core/services/sweet-alert-2.service';
import { PostService } from '../../core/services/post.service';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss'],
})
export class PostCardComponent implements OnInit {
  @Input() post!: Post;
  @Input() misPosts: boolean = false;
  // estaAutenticado: boolean = false;
  activarOpciones: boolean = false;

  constructor(
    private _postService: PostService,
    private _router: Router,
    private _authService: AuthService,
    private _swal: SweetAlert2Service
  ) {}

  ngOnInit(): void {
    // this.estaAutenticado = ;
    if (this.misPosts && this._authService.onSesion()) {
      this.activarOpciones = true;
    }
  }

  VerPost(id: number) {
    this._router.navigate(['/posts', id]);
  }

}
