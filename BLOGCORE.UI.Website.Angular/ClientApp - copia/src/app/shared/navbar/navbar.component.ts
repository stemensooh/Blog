import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { Usuario } from '../../core/models/usuario.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnChanges {
  isNavbarCollapsed = true;
  focus: boolean = false;
  usuario: Usuario | null = null;

  @Input() tieneSesion: boolean = false;

  constructor(private _authService: AuthService, private _route: Router) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.usuario = this._authService.obtenerUsuario();
  }

  ngOnInit(): void {}

  search(form: NgForm) {
    if (
      form.value.searchText !== undefined &&
      form.value.searchText !== null &&
      form.value.searchText !== ''
    ) {
      this._route.navigate(['/search', form.value.searchText]);
    }
  }

  cerrarSesion() {
    this.tieneSesion = false;
    this._authService.salirSesion();
    // this.construirMenu();
  }
}
