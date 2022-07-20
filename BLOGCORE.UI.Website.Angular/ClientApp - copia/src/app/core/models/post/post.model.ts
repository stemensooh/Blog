export class Post {
  id!: number;
  titulo!: string;
  categoriaId!: number[];
  categoriaName!: string[];
  cuerpo!: string;
  imagen!: string;
  fecha!: Date;
  fechaCreacion!: string;
  username!: string;
  autor!: string;
  vistas!: number;
  vistasPaginaUsuario!: number;
  vistasPaginaAnonimo!: number;
  comentarios!: number;
}
