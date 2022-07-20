import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from '../../core/services/profile.service';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';

@Component({
  selector: 'app-profile-form',
  templateUrl: './profile-form.component.html',
  styleUrls: ['./profile-form.component.css']
})
export class ProfileFormComponent implements OnInit {
  public posts: Post[] = [];
  username!: string;
  tieneSesion: boolean = true;

  constructor(
    private _authService: AuthService,
    private _activatedRoute: ActivatedRoute,
    private _profileService: ProfileService,
    private _postService: PostService
  ) {}

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.username = params.username;
      this.tieneSesion = this._authService.onSesion();
      console.log(this.username);
      // alert(this.username);
      // this._postService
      //   .obtenerPosts('date_asc', '', this.username)
      //   .subscribe((data: any) => {
      //     // console.log(data);
      //     this.posts = data;
      //   });
    });
  }

}
