import { Component, OnInit } from '@angular/core';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Post } from '../../core/models/post/post.model';
import { PostService } from '../../core/services/post.service';
import { PostFormModel } from '../../core/models/post/post-form.model';
import { CategoriaModel } from '../../core/models/post/categoria.model';
import { CategoriaService } from '../../core/services/categoria.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-posts-add',
  templateUrl: './posts-form.component.html',
  styleUrls: ['./posts-form.component.scss'],
})
export class PostsFormComponent implements OnInit {
  postFormModel: PostFormModel = new PostFormModel();

  titulo: string = 'Agregar Post'

  id: number = 0;
  imageBase64Data!: string;
  imageBase64Name!: string;
  esNuevo: boolean = false;
  editorData: string = '<p><b>This</b> is a CKEditor 4 WYSIWYG editor instance created with Angular.</p>';

  post!: Post;
  categorias: CategoriaModel[] = [];
  public ClassicEditor = ClassicEditor;
  public config: DropzoneConfigInterface = {
    clickable: true,
    maxFiles: 1,
    addRemoveLinks: true,
    autoReset: null,
    errorReset: null,
    cancelReset: null,
  };

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _postService: PostService,
    private _categoriaService: CategoriaService
  ) {}

  ngOnInit(): void {
    this._categoriaService.getAll().subscribe((data) => {
      this.categorias = data;
    });

    this._activatedRoute.params.subscribe((params) => {
      this.id = params.id;

      this._postService
        .verPost(this.id)
        .subscribe((data: any) => {
          console.log(data);
          
          this.postFormModel = new PostFormModel();
          this.postFormModel.Id = data.id;
          this.postFormModel.Titulo = data.titulo;
          this.postFormModel.Cuerpo = data.cuerpo;
          this.postFormModel.Categoria = data.categoriaId;
          this.postFormModel.ImagenBase64 = data.imagen;

          this.titulo = 'Editar Post';
        });
    });
  }

  public onUploadInit(args: any): void {}

  public onUploadError(args: any): void {}

  public onUploadSuccess(args: any): void {
    this.imageBase64Data = args[0].dataURL.replace('data:image/jpeg;base64,', '');
    this.imageBase64Name = args[0].name;
    this.postFormModel.MantenerImage = false;
    this.postFormModel.Imagen = this.imageBase64Name;
    this.postFormModel.ImagenBase64 = this.imageBase64Data.replace('data:image/webp;base64,', '');
  }

  onSubmit() {
    this.postFormModel.Imagen = this.imageBase64Name;
    this.postFormModel.ImagenBase64 = this.imageBase64Data.replace('data:image/webp;base64,', '');
    this._postService.addPost(this.postFormModel).subscribe((data: any) => {
      console.log(data);
    });
  }
}
