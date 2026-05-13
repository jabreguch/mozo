import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UrlApiService {
  baseUrl = environment.baseUrl;

  buildUrl(path: string): string {
    return `${this.baseUrl}${path}`;
  }
}
