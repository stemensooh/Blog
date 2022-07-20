import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewChecked,
  AfterViewInit,
  Component,
  DoCheck,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { PostService } from '../../core/services/post.service';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../../core/models/post/post.model';
import { HostListener } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  public texto: string = '';
  posts: Post[] = [];
  private pageNumber: number = 1;

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _postService: PostService
  ) {}

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.texto = params.text;

      this.consultarPosts(true);
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
    .obtenerPosts(
      {
        sortOrder: 'date_desc', 
        currentFilter: '',
        searchString: this.texto,
        pageNumber: this.pageNumber,
        pageSize: 6,
        profile: '',
        categoria: ''
      }
    )
    .subscribe((data: any) => {
      if (cargar){
        this.posts.push(...data);
      }else{
        this.posts = data;
      }
    });
  }
}
