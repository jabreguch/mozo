import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { cleanParams } from '@app/core/global/params.helper';
import { PaginaModel } from '@app/shared/models/seguridad/pagina.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';



@Injectable({
  providedIn: 'root'
})
export class PaginaService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.pagina);

  insert(c: PaginaModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

  update(c: PaginaModel): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}`, c);
  }

  deleteById(c: PaginaModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  updateState(c: PaginaModel): Observable<number> {
    return this.http.patch<number>(`${this.baseUrl}/state`, c);
  }

  selById(c: PaginaModel): Observable<PaginaModel> {
    return this.http.get<PaginaModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

}
