import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PerfilModel } from '@app/shared/models/seguridad/perfil.model';
import { cleanParams } from '@app/core/global/params.helper';
import { PerfilPaginaModel } from '@app/shared/models/seguridad/perfil-pagina.model';
import { MenuModel } from '@app/shared/models/seguridad/menu.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';

@Injectable({
  providedIn: 'root'
})
export class PerfilPaginaService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.perfilPagina);

  selAll(c: PerfilPaginaModel): Observable<MenuModel[]> {
    return this.http.get<PerfilModel[]>(`${this.baseUrl}`, { params: cleanParams(c) });
  }

  insert(c: PerfilModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

}
