export class ComentarioModel {
    id!: number;
    usuario!: string;
    imagen!: string;
    mensaje!: string;
    fecha!: Date;
    reaccion!: number;
    comentarios!: ComentarioModel[]
}