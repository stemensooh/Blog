export class ComentarioModel {
    id!: number;
    usuario!: string;
    nombreCompleto!: string;
    imagen!: string;
    mensaje!: string;
    fecha!: Date;
    reaccion!: number;
    comentarios!: ComentarioModel[];
}