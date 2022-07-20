import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'urlMailTo'
})
export class UrlMailToPipe implements PipeTransform {

  transform(url: string): string {
    if (url.includes('http')){
      return url;
    }else{
      return `mailto:#${url}`;
    }
  }

}
