import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { cleanParams } from '@app/core/global/params.helper';
import { EmpresaModel } from '@app/shared/models/seguridad/empresa.model';
import { PagedResult } from '@app/shared/models/base.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';


@Injectable({
  providedIn: 'root'
})
export class EmpresaService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.empresa);

  insert(c: EmpresaModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

  update(c: EmpresaModel): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}`, c);
  }

  updateState(c: EmpresaModel): Observable<number> {
    return this.http.patch<number>(`${this.baseUrl}/state`, c);
  }

  deleteById(c: EmpresaModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  selAll(c: EmpresaModel): Observable<PagedResult<EmpresaModel>> {
    return this.http.get<EmpresaModel[]>(`${this.baseUrl}`, { params: cleanParams(c) })
      .pipe(
        map(r => {
          return {
            rowsCount: r[0]?.rowsCount ?? 0,
            data: r
          } as PagedResult<EmpresaModel>;
        })
      );
  }

  selAllActive(): Observable<EmpresaModel[]> {
    return this.http.get<EmpresaModel[]>(`${this.baseUrl}/active`);
  }

  selById(c: EmpresaModel): Observable<EmpresaModel> {
    return this.http.get<EmpresaModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

}
