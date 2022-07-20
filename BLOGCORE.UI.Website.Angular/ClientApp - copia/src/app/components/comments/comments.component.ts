import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewChecked,
  AfterViewInit,
  Component,
  DoCheck,
  Input,
  OnChanges,
  OnInit,
} from '@angular/core';
import { ComentarioModel } from '../../core/models/post/comentario.model';
import { ComentarioService } from '../../core/services/comentario.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Usuario } from '../../core/models/usuario.model';
import { tap } from 'rxjs/operators';
import { ResultModel } from '../../core/models/result.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss'],
})
export class CommentsComponent implements OnInit, OnChanges {
  @Input() postId: number = 0;
  ComentarioPadreId: number = 0;

  comentarios: ComentarioModel[] = [];
  totalComentarios: number = 0;

  tieneSesion: boolean = true;

  constructor(
    private _comentarioService: ComentarioService,
    private _fb: FormBuilder,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {

  }

  ngOnChanges() {
    this.cargarComentarios();
  }

  private cargarComentarios() {
    this.ComentarioPadreId = 0;
    this._comentarioService
      .cargarComentariosPost(this.postId)
      .subscribe((data: any) => {
        this.comentarios = data.comentarios;
        this.totalComentarios = data.totalComentario;
      });
  }

  procesaPropagar(e: any, content: any){
    if (e === "recargar"){
      this.modalService.dismissAll();
      this.cargarComentarios();
    }
  }
  
  open(content: any, id: number){
    console.log(id);
    this.ComentarioPadreId = id;
    this.modalService.open(content, { size: 'lg' });
  }

}
