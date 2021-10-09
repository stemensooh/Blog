import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit, OnDestroy {
  private postsSubscription!: Subscription;
  public posts: Post[] = [];

  constructor(private _postService: PostService, private _route: Router) { }
  ngOnDestroy(): void {
    this.postsSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this._postService.obtenerMisPosts('date_desc', '', '',1, 20);
    this.postsSubscription = this._postService
      .allMyPosts()
      .subscribe((posts: Post[]) => {
        this.posts = posts;
      });

  }

  nuevoPost(){
    this._route.navigate(['/posts/add']);
  }

}
