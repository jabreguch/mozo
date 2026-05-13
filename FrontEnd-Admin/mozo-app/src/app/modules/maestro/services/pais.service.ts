import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaisModel } from '@app/shared/models/maestro/pais.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';

const BASE_URL = `${environment.baseUrl}/maestro/pais`;

@Injectable({
  providedIn: 'root'
})
export class PaisService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.maestro.pais);

  selAllActive(): Observable<PaisModel[]> {
    return this.http.get<PaisModel[]>(`${this.baseUrl}/active`);
  }

}
