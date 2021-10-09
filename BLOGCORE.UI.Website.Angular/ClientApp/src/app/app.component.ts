import { AuthService } from './core/services/auth.service';
import { Component, PLATFORM_ID, Inject, OnInit } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { LoadingBarService } from '@ngx-loading-bar/core';
import { map, delay, withLatestFrom } from 'rxjs/operators';
// import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
   abrirMenu = false;
   constructor(@Inject(PLATFORM_ID) private platformId: Object, private _authService: AuthService, private _loader: LoadingBarService){
    if (isPlatformBrowser(this.platformId)) {
      // translate.setDefaultLang('en');
      // translate.addLangs(['en', 'de', 'es', 'fr', 'pt', 'cn', 'ae']);
    }
   }

   loaders = this._loader.progress$.pipe(
    delay(1000),
    withLatestFrom(this._loader.progress$),
    map(v => v[1]),
  );
  
  ngOnInit(): void{
    this._authService.cargarUsuario();
  }

}
