import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { environment } from 'src/environments/environment';
import { cleanParams } from '@app/core/global/params.helper';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';


@Injectable({
  providedIn: 'root'
})
export class ModuloService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.modulo);

  /**
   * Obtiene todos los módulos
   */
  selAll(): Observable<ModuloModel[]> {
    return this.http.get<ModuloModel[]>(this.baseUrl);
  }

  /**
   * Obtiene todos los módulos activos
   */
  selAllActive(): Observable<ModuloModel[]> {
    return this.http.get<ModuloModel[]>(`${this.baseUrl}/active`);
  }

  /**
   * Obtiene áreas de módulos activos
   */
  selAllActiveAreas(): Observable<ModuloModel[]> {
    return this.http.get<ModuloModel[]>(`${this.baseUrl}/active/areas`);
  }

  /**
   * Obtiene un módulo por ID
   */
  selById(c: ModuloModel): Observable<ModuloModel> {
    return this.http.get<ModuloModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  /**
   * Crea un nuevo módulo
   */
  insert(c: ModuloModel): Observable<number> {
    return this.http.post<number>(this.baseUrl, c);
  }

  /**
   * Actualiza un módulo existente
   */
  update(c: ModuloModel): Observable<number> {
    return this.http.put<number>(this.baseUrl, c);
  }

  /**
   * Activa o desactiva un módulo
   */
  updateState(c: ModuloModel): Observable<number> {
    return this.http.patch<number>(`${this.baseUrl}/state`, c);
  }

  /**
   * Elimina un módulo por ID
   */
  deleteById(c: ModuloModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }
}
