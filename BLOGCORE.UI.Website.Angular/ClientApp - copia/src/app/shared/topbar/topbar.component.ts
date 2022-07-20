import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MENU } from '../../core/data/menu';
import { RouteInfo } from '../../core/models/route-info.model';
import { AuthService } from '../../core/services/auth.service';
import { Usuario } from '../../core/models/usuario.model';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
})
export class TopbarComponent implements OnInit, OnChanges {
  focus: boolean = false;
  isNavbarCollapsed = true;
  menuItems: RouteInfo[] = [];
  usuario: Usuario | null = null;
  @Input() tieneSesion: boolean = false;
  constructor(private _authService: AuthService, private _route: Router) {}

  ngOnChanges(changes: SimpleChanges): void {
    console.log('build menu');
    this.construirMenu();
  }

  ngOnInit() {
    
    
  }
  getTitle() {
    
  }

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

  construirMenu() {
    console.log('construirMenu', this.menuItems);
    this.menuItems = [];
    setTimeout(() => {
      MENU.forEach((element) => {
        if (element.privado) {
          if (this.tieneSesion) {
            this.usuario = this._authService.obtenerUsuario();
            // this.tieneSesion = this._authService.onSesion();
            this.menuItems.push(element);
          }
        } else {
          this.menuItems.push(element);
        }
      });
    }, 2000);
  }

  cerrarSesion() {
    this.tieneSesion = false;
    this._authService.salirSesion();
    // this.construirMenu();
  }
}
