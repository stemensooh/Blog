import { Component, OnInit } from '@angular/core';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Post } from '../../core/models/post/post.model';
import { PostService } from '../../core/services/post.service';
import { PostFormModel } from '../../core/models/post/post-form.model';
import { CategoriaModel } from '../../core/models/post/categoria.model';
import { CategoriaService } from '../../core/services/categoria.service';
import { ActivatedRoute } from '@angular/router';
import { SweetAlert2Service } from '../../core/services/sweet-alert-2.service';
import { ToastrService } from 'ngx-toastr';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  MinLengthValidator,
  Validators,
} from '@angular/forms';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorService } from '../../core/services/error.service';

@Component({
  selector: 'app-posts-add',
  templateUrl: './posts-form.component.html',
  styleUrls: ['./posts-form.component.scss'],
})
export class PostsFormComponent implements OnInit {
  postFormModel: PostFormModel = new PostFormModel();

  postFormGroup!: FormGroup;

  titulo: string = 'Agregar Post';

  id: number = 0;
  imageBase64Data!: string;
  imageBase64Name!: string;
  esNuevo: boolean = false;
  editorData: string =
    '<p><b>This</b> is a CKEditor 4 WYSIWYG editor instance created with Angular.</p>';

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
    private fb: FormBuilder,
    //private _toastr: ToastrService,
    private _swal: SweetAlert2Service,
    private _activatedRoute: ActivatedRoute,
    private _postService: PostService,
    private _categoriaService: CategoriaService,
    private _errorServie: ErrorService
  ) {}

  ngOnInit(): void {
    this.postFormGroup = this.fb.group({
      id: [0, [Validators.required]],
      titulo: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(200),
        ],
      ],
      categoria: [[], [Validators.required]],
      cuerpo: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(20000),
        ],
      ],
    });

    this._categoriaService.getAll().subscribe((data) => {
      this.categorias = data;
    });

    this._activatedRoute.params.subscribe((params) => {
      this.id = params.id;

      if (this.id > 0) {
        this._postService.registrarPostGet(this.id).subscribe((data: any) => {
          this.postFormModel = data;
          this.titulo = 'Editar Post';

          this.postFormGroup = this.fb.group({
            id: [this.postFormModel.id, [Validators.required]],
            titulo: [
              this.postFormModel.titulo,
              [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(200),
              ],
            ],
            categoria: [this.postFormModel.categoria, [Validators.required]],
            cuerpo: [
              this.postFormModel.cuerpo,
              [
                Validators.required,
                Validators.minLength(10),
                Validators.maxLength(20000),
              ],
            ],
          });
        });
      }
    });
  }

  get f() {
    return this.postFormGroup.controls;
  }

  public onUploadInit(args: any): void {}

  public onUploadError(args: any): void {}

  public onUploadSuccess(args: any): void {
    this.imageBase64Data = args[0].dataURL
      .replace('data:image/jpeg;base64,', '')
      .replace('data:image/png;base64,', '');
    this.imageBase64Name = args[0].name;
    this.postFormModel.mantenerImage = false;
    this.postFormModel.imagen = this.imageBase64Name;
    this.postFormModel.imagenBase64 = this.imageBase64Data;
  }

  onSubmit() {
    console.warn(this.postFormGroup.value);
    if (!this.postFormGroup.invalid) {
      this.postFormModel.cuerpo = this.f.cuerpo.value;
      this.postFormModel.titulo = this.f.titulo.value;
      this.postFormModel.id = this.f.id.value;
      this.postFormModel.categoria = this.f.categoria.value;
      this._postService.registrarPostPost(this.postFormModel).subscribe(
        (data: any) => {
          if (data) {
            if (this.id > 0) {
              this._swal.mensajeGenericoConfirmacionRedireccion(
                '',
                'Registro editado',
                'success',
                '/posts'
              );
            } else {
              this._swal.mensajeGenericoConfirmacionRedireccion(
                '',
                'Registro agregado',
                'success',
                'posts'
              );
            }
          }
        },
        (error: HttpErrorResponse) => {
          this._errorServie.handleError(error, 'post-form');
        }
      );
    } else {
      this._swal.mensajeGenerico('', 'Formulario inválido', 'error');
    }
  }

  

  
}
