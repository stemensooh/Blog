import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'noImage2'
})
export class NoImage2Pipe implements PipeTransform {

  transform(imagen: string): string {
    // console.log(imagen);
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
