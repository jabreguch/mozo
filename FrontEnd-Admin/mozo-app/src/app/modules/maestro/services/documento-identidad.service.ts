import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DocumentoIdentidadModel } from '@app/shared/models/maestro/documento-identidad.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';


@Injectable({
  providedIn: 'root'
})
export class DocumentoIdentidadService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.maestro.documentoIdentidad);

  selAllActive(): Observable<DocumentoIdentidadModel[]> {
    return this.http.get<DocumentoIdentidadModel[]>(`${this.baseUrl}/active`);
  }

}
