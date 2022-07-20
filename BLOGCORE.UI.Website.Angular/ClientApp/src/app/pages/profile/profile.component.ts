import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from '../../core/services/profile.service';
import { AuthService } from '../../core/services/auth.service';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  username!: string;
  tieneSesion: boolean = true;
  posts: Post[] = [];
  private pageNumber: number = 1;

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
      // console.log(this.username);
      this.consultarPosts(false);
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
    .obtenerPosts({
        sortOrder: 'date_desc', 
        currentFilter: '',
        searchString: '',
        pageNumber: this.pageNumber,
        pageSize: 5,
        profile: this.username
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
