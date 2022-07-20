import { Component, OnInit } from '@angular/core';
import { CategoriaService } from '../../core/services/categoria.service';
import { CategoriaModel } from '../../core/models/post/categoria.model';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.css']
})
export class TagComponent implements OnInit {

  categorias: CategoriaModel[] = [];

  constructor(private _tags: CategoriaService) { }

  ngOnInit(): void {
    this._tags.getAll().subscribe((data: CategoriaModel[]) => {
      this.categorias = data;
    });
  }

}
