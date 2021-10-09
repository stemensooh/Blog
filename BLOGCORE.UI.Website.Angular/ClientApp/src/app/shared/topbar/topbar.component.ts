import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { MENU } from '../../core/data/menu';
import { RouteInfo } from '../../core/models/route-info.model';
import { AuthService } from '../../core/services/auth.service';
import { PostService } from '../../core/services/post.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
})
export class TopbarComponent implements OnInit {
  focus: boolean = false;
  isNavbarCollapsed = true;
  menuItems: RouteInfo[] = [];

  constructor(
    private _guardService: AuthGuard,
    private _authService: AuthService,
    private _postService: PostService
  ) {}

  ngOnInit() {
    MENU.forEach((element) => {
      if (element.privado) {
        if (this._authService.onSesion()) {
          this.menuItems.push(element);
        }
      } else {
        this.menuItems.push(element);
      }
    });

    //this.menuItems = MENU.filter(listTitle => listTitle);
  }
  getTitle() {}

  logout() {}

  toggleMenu() {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  search(form: NgForm){
    console.log(form.value);
    this._postService.search('date_asc', '', form.value).subscribe((data: any) => {
      console.log(data);
    });
  }
}
