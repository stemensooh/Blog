import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Usuario } from '../../core/models/usuario.model';
import { ComentarioService } from '../../core/services/comentario.service';
import { ResultModel } from '../../core/models/result.model';
import { environment } from '../../../environments/environment';
// import { ReCaptchaV3Service } from 'ng-recaptcha';
import { SweetAlert2Service } from '../../core/services/sweet-alert-2.service';

@Component({
  selector: 'app-comments-form',
  templateUrl: './comments-form.component.html',
  styleUrls: ['./comments-form.component.css']
})
export class CommentsFormComponent implements OnInit {
  @Input() postId: number = 0;
  @Input() ComentarioPadreId: number = 0;
  @Output() propagar = new EventEmitter<string>();

  commentForm!: FormGroup;
  usuario: Usuario | null = null;
  tieneSesion: boolean = true;
  captchaToken: string = '';
  captchaSiteKey: string = environment.Recaptcha.ClaveSitioWeb;

  constructor(
    private _swal: SweetAlert2Service,
    // private recaptchaV3Service: ReCaptchaV3Service, 
    private _fb: FormBuilder, 
    private _authService: AuthService, 
    private _comentarioService: ComentarioService
    ) { }

  ngOnInit(): void {
    
    this.commentForm = this._fb.group({
      username: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      comment: ['', [Validators.required]],
    });

    // if (this._authService.onSesion()) {
    //   this.usuario = this._authService.obtenerUsuario();
    //   this.tieneSesion = this._authService.onSesion();
    //   // console.log(this.usuario);
    //   this.commentForm = this._fb.group({
    //     username: [ { value: `${this.usuario?.nombre} ${this.usuario?.apellido}`, disabled: true }, [Validators.required]],
    //     email: [ { value: this.usuario?.email, disabled: true }, [Validators.required, Validators.email]],
    //     comment: ['', [Validators.required]],
    //   });
    // }
  }

  get f() {
    return this.commentForm.controls;
  }

  registrarComentario() {
    // console.log(this.f);

    if (this.commentForm.valid) {
      this._comentarioService
      .agregar(
        this.postId,
        this.f.username.value,
        this.f.email.value,
        this.f.comment.value,
        this.ComentarioPadreId,
        this.captchaToken
      )
      .subscribe((response: ResultModel) => {
        console.log(response);
        if (response.estado) {
          // this.recargarComentario = true;
          this.propagar.emit('recargar');
          this.commentForm = this._fb.group({
            username: ['', [Validators.required]],
            email: ['', [Validators.required, Validators.email]],
            comment: ['', [Validators.required]],
          });
        }
      });
    } else {
      this._swal.mensajeGenerico('', 'Formulario inválido', 'error');
    }
  }

  resolved(captchaResponse: string) {
    this.captchaToken = captchaResponse;
  }
}
