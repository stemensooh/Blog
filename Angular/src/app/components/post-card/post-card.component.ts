import { Component, OnInit, Input } from '@angular/core';
import { PostService } from '../../core/services/post.service';
import { Router } from '@angular/router';
import { Post } from '../../core/models/post/post.model';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
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
    // private _swal: SweetAlert2Service
  ) { }

  ngOnInit(): void {
    if (this.misPosts && this._authService.isAuthenticated()) {
      this.activarOpciones = true;
    }
  }

  VerPost(id: number) {
    this._router.navigate(['/posts', id]);
  }

}
