import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent implements OnInit {

  constructor(private _cookieService: CookieService, private _authService: AuthService) {}

  ngOnInit(): void {
    this._cookieService.set('Test', 'Hello World');
    console.log(this._cookieService.get('Test'));
    // this._cookieService = this._cookieService.get('Test');
  }


  CrearSesion(){
    this._authService.login({ email: 'stemensooh@gmail.com', password: '12345678' });
  }
}
