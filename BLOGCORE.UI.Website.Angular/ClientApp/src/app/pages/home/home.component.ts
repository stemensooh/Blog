import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';
import { Router } from '@angular/router';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  private postsSubscription!: Subscription;
  private postSubscription!: Subscription;
  public posts: Post[] = [];
  public post: Post[] = [];

  constructor(private _postService: PostService, private _router: Router) {}
  ngOnDestroy(): void {
    this.postsSubscription.unsubscribe();
    this.postSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this._postService.obtenerPosts('date_desc', '', '',1, 20);
    this.postsSubscription = this._postService
      .all()
      .subscribe((posts: Post[]) => {
        this.posts = posts;
      });

    setTimeout(() => {
      this._postService.consultarPostMasVisto();
      this.postSubscription = this._postService
        .mostViews()
        .subscribe((post: Post[]) => {
          //console.log(post);
          this.post = post;
          this.post.forEach((p) => {
            this.removePosts(p.id);
          });
        });
    }, 1000);
  }

  removePosts(id: number): void {
    //console.log('removePosts primary', this.posts);

    this.posts = this.posts.filter((item) => item.id !== id);
    //console.log('removePosts', this.posts);
  }

  VerPost(id: number){
    console.log(id);
    this._router.navigate(['/posts', id]);
    
  }
}
