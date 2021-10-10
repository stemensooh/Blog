import { Component, OnDestroy, OnInit, HostListener } from '@angular/core';
import { Subscription } from 'rxjs';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent implements OnInit, OnDestroy {
  public posts: Post[] = [];
  private pageNumber: number = 1;

  constructor(private _postService: PostService, private _route: Router) {}
  ngOnDestroy(): void {}

  ngOnInit(): void {
    this._postService
      .obtenerMisPosts('date_desc', '', '', 1, 8)
      .subscribe((posts: Post[]) => {
        this.posts = posts;
      });
  }

  nuevoPost() {
    this._route.navigate(['/posts/add']);
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
      this._postService
        .obtenerMisPosts('date_desc', '', '', this.pageNumber, 4)
        .subscribe((data) => {
          this.posts.push(...data);
        });
    }
  }
}
