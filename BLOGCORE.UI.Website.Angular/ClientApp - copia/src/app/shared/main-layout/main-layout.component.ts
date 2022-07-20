import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {
  tieneSesion = false;

  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
    this._authService.cargarUsuario();
    this.tieneSesion = this._authService.onSesion();
  }

}
