import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PerfilModel } from '@app/shared/models/seguridad/perfil.model';
import { cleanParams } from '@app/core/global/params.helper';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';



@Injectable({
  providedIn: 'root'
})
export class PerfilService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.perfil);

  selAll(c: PerfilModel): Observable<PerfilModel[]> {
    return this.http.get<PerfilModel[]>(`${this.baseUrl}`, { params: cleanParams(c) });
  }

  selAllActive(): Observable<PerfilModel[]> {
    return this.http.get<PerfilModel[]>(`${this.baseUrl}/active`);
  }

  selById(c: PerfilModel): Observable<PerfilModel> {
    return this.http.get<PerfilModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  insert(c: PerfilModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

  update(c: PerfilModel): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}`, c);
  }

  updateState(c: PerfilModel): Observable<number> {
    return this.http.patch<number>(`${this.baseUrl}/state`, c);
  }

  deleteById(c: PerfilModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  selDefaultByModulo(c: PerfilModel): Observable<PerfilModel> {
    return this.http.get<PerfilModel>(`${this.baseUrl}/default-bymodulo`, { params: cleanParams(c) });
  }

}
