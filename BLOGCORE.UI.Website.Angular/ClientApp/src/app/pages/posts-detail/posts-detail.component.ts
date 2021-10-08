import { Component, OnInit } from '@angular/core';
import { PostService } from '../../core/services/post.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Post } from '../../core/models/post/post.model';

@Component({
  selector: 'app-posts-detail',
  templateUrl: './posts-detail.component.html',
  styleUrls: ['./posts-detail.component.scss'],
})
export class PostsDetailComponent implements OnInit {
  public post!: Post;

  constructor(private _postService: PostService, private rutaActiva: ActivatedRoute, ) {
    

  }

  ngOnInit(): void {
    this._postService.verPost(this.rutaActiva.snapshot.params.id).subscribe((data: any) => {
      this.post = data;
      console.log(data);
    });
  }
}
