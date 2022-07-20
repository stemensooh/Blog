export class Orador {
    id!: string;
    nombre!: string;
    apellido!: string;
    descripcion!: string;
    imagen!: string;
    especialidades: Especialidad[] = [];

    constructor(id: string, nombre: string, apellido: string, descripcion: string, imagen: string){
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.descripcion = descripcion;
        this.imagen = imagen;
    }
}

export class Especialidad{
    id!: string;
    nombre!: string;
    tipo!: string;

    constructor(){}
}