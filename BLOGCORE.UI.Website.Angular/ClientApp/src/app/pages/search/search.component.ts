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

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  public texto: string = '';
  posts: Post[] = [];

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _postService: PostService
  ) {}

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.texto = params.text;

      this._postService
        .obtenerPosts('date_asc', '', this.texto)
        .subscribe((data: any) => {
          // console.log(data);
          this.posts = data;
        });
    });
  }
}
