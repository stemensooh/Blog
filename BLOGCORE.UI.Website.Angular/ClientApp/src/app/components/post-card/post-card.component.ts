import { Component, Input, OnInit } from '@angular/core';
import { Orador } from 'src/app/core/models/orador.model';
import { Post } from '../../core/models/post/post.model';
import { PostService } from '../../core/services/post.service';
import { Subscription } from 'rxjs';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {

  @Input() post!: Post;

  constructor(private _router: Router) { }

  ngOnInit(): void {
    // console.log(this.post);
  }

  VerPost(id: number){
    console.log(id);
    this._router.navigate(['/posts', id]);
    
  }

}
