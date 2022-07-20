import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'noImage'
})
export class NoImagePipe implements PipeTransform {

  transform(imagen: string): string {

    if(imagen === undefined){
      return 'assets/images/sin-imagen.jpg';
    } else if(imagen === null){
      return 'assets/images/sin-imagen.jpg';
    } else if(imagen === ''){
      return 'assets/images/sin-imagen.jpg';
    } else{
      return 'data:image/jpg;base64,' + imagen;
    }
    
  }

}
