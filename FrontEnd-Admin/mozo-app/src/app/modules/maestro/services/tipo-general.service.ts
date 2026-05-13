import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';
import { TipoGeneralModel } from '@app/shared/models/maestro/tipo-general.model';
import { cleanParams } from '@app/core/global/params.helper';


@Injectable({
  providedIn: 'root'
})
export class TipoGeneralService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.maestro.tipoGeneral);

  selById(c: TipoGeneralModel): Observable<TipoGeneralModel> {
    return this.http.get<TipoGeneralModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

   selAllActive(c: TipoGeneralModel): Observable<TipoGeneralModel[]> {
    return this.http.get<TipoGeneralModel[]>(`${this.baseUrl}/active`, { params: cleanParams(c) });
  }

   selAllActiveByModulo(c: TipoGeneralModel): Observable<TipoGeneralModel[]> {
    return this.http.get<TipoGeneralModel[]>(`${this.baseUrl}/modulos/active`, { params: cleanParams(c) });
  }

}
