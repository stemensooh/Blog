import { Component, OnDestroy, OnInit, HostListener } from '@angular/core';
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
  private postSubscription!: Subscription;
  public posts: Post[] = [];
  public post: Post[] = [];
  private pageNumber: number = 1;
  constructor(private _postService: PostService, private _router: Router) {}
  ngOnDestroy(): void {
    this.postSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this._postService.obtenerPosts('date_desc', '', '',1, 5)
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
    this.posts = this.posts.filter((item) => item.id !== id);
  }

  VerPost(id: number){
    this._router.navigate(['/posts', id]);
  }

  @HostListener('window:scroll', ['$event'])
  onScroll() {

    const pos = (document.documentElement.scrollTop || document.body.scrollTop ) + 1300;
    const max = ( document.documentElement.scrollHeight || document.body.scrollHeight );
    
    if ( pos > max ) {
      // TODO: llamar el servicio
      if ( this._postService.cargando ) { return; }
      this.pageNumber += 1;
      this._postService.obtenerPosts('date_desc','','', this.pageNumber).subscribe( data => {
        this.posts.push(...data );  
      });
    }
    
    
  }
}
