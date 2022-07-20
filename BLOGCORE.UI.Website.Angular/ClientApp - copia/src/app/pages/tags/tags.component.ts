import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post/post.model';
import { CategoriaModel } from '../../core/models/post/categoria.model';
import { CategoriaService } from '../../core/services/categoria.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})
export class TagsComponent implements OnInit {

  private pageNumber: number = 1;

  texto?: string ;
  posts: Post[] = [];
  categorias: CategoriaModel[] = [];

  constructor(
    private _activatedRoute: ActivatedRoute, 
    private _postService: PostService,
    private _tags: CategoriaService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.texto = params.text;
      this.consultarPosts(false);
    });

    this._tags.getAll().subscribe((data: CategoriaModel[]) => {
      this.categorias = data;
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
    .obtenerPosts(
      {
        sortOrder: 'date_desc', 
        currentFilter: '',
        searchString: '',
        pageNumber: this.pageNumber,
        pageSize: 6,
        profile: '',
        categoria: this.texto ?? ''
      }
    )
    .subscribe((data: any) => {
      // console.log(data);
      if (cargar){
        this.posts.push(...data);
      }else{
        this.posts = data;
      }
    });
  }

  limpiarTexto(){
    this.texto = undefined;
    this.router.navigate(['/tags']);
  }
}
