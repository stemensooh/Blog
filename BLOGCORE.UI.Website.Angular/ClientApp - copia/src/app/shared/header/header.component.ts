import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isNavbarCollapsed = true;
  focus: boolean = false;
  
  constructor(private _authService: AuthService, private _route: Router) { }

  ngOnInit(): void {
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
}
