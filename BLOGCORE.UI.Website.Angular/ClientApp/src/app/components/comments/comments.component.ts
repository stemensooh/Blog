import { Component, Input, OnInit } from '@angular/core';
import { ComentarioModel } from '../../core/models/post/comentario.model';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  @Input() comentarios!: ComentarioModel[];
  constructor() { }

  ngOnInit(): void {
    console.log(this.comentarios);
  }

}
