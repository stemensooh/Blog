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
  public posts: Post[] = [];
  public post: Post[] = [];
  private pageNumber: number = 1;
  cargando: boolean = false;
  constructor(private _postService: PostService, private _router: Router) {}
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.consultarPosts(false);

    setTimeout(() => {
      this._postService.consultarPostMasVisto().subscribe((post: Post[]) => {
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

  VerPost(id: number) {
    this._router.navigate(['/posts', id]);
  }

  @HostListener('window:scroll', ['$event'])
  onScroll() {
    const pos =
      (document.documentElement.scrollTop || document.body.scrollTop) + 1300;
    const max =
      document.documentElement.scrollHeight || document.body.scrollHeight;

    if (pos > max) {

      if (this._postService.cargando) {
        this.cargando = true;
        return;
      }else{
        this.cargando = false;
      }
      this.pageNumber += 1;
      this.consultarPosts(true);
    }
  }

  private consultarPosts(cargar: boolean){
    this._postService
    .obtenerPosts({
        sortOrder: 'date_desc', 
        currentFilter: '',
        searchString: '',
        pageNumber: this.pageNumber,
        pageSize: 5,
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
