import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { cleanParams } from '@app/core/global/params.helper';
import { PagedResult } from '@app/shared/models/base.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';
import { EmpresaModuloModel } from '@app/shared/models/seguridad/empresa-modulo.model';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';


@Injectable({
  providedIn: 'root'
})
export class EmpresaModuloService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.empresaModulo);

  insert(c: EmpresaModuloModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

  update(c: EmpresaModuloModel): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}`, c);
  }


  deleteById(c: EmpresaModuloModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  selById(c: EmpresaModuloModel): Observable<EmpresaModuloModel> {
      return this.http.get<EmpresaModuloModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
    }

  selAll(c: EmpresaModuloModel): Observable<EmpresaModuloModel[]> {
    return this.http.get<EmpresaModuloModel[]>(`${this.baseUrl}`, { params: cleanParams(c) });
  }

  selSelectAndUnSelectAll(c: EmpresaModuloModel): Observable<EmpresaModuloModel[]> {
    return this.http.get<EmpresaModuloModel[]>(`${this.baseUrl}/select-unselect`, { params: cleanParams(c) });
  }
//SelSelectAndUnSelectAllAsync

}
