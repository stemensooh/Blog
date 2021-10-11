import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { MENU } from '../../core/data/menu';
import { RouteInfo } from '../../core/models/route-info.model';
import { AuthService } from '../../core/services/auth.service';
import { PostService } from '../../core/services/post.service';
import { Usuario } from '../../core/models/usuario.model';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
})
export class TopbarComponent implements OnInit {
  focus: boolean = false;
  isNavbarCollapsed = true;
  menuItems: RouteInfo[] = [];
  usuario: Usuario | null = null;
  tieneSesion: boolean = false;
  constructor(private _authService: AuthService, private _route: Router) {}

  ngOnInit() {
    console.log('build menu');

    setTimeout(() => {
      if (this._authService.onSesion()) {
        this.usuario = this._authService.obtenerUsuario();
        this.tieneSesion = this._authService.onSesion();
      }

      this.construirMenu();
    }, 2000);
  }
  getTitle() {}

  logout() {}

  toggleMenu() {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  search(form: NgForm) {
    if (
      form.value.searchText !== undefined &&
      form.value.searchText !== null &&
      form.value.searchText !== ''
    ) {
      this._route.navigate(['/search', form.value.searchText]);
    }
  }

  construirMenu(){
    this.menuItems = [];
    MENU.forEach((element) => {
      if (element.privado) {
        if (this._authService.onSesion()) {
          console.log('tiene sesion');
          this.menuItems.push(element);
        }
      } else {
        this.menuItems.push(element);
      }
    });
  }

  cerrarSesion(){
    this.tieneSesion = false;
    this._authService.salirSesion();
    setTimeout(() => {
      this.construirMenu();
    }, 2000);
  }
}
