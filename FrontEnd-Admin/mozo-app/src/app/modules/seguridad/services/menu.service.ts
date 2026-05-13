import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { cleanParams } from '@app/core/global/params.helper';
import { MenuModel } from '@app/shared/models/seguridad/menu.model';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';


@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.seguridad.menu);

  selArbol(c: MenuModel): Observable<MenuModel[]> {
    return this.http.get<MenuModel[]>(`${this.baseUrl}/arbol`, { params: cleanParams(c) });
  }
  /*
    selAllActive(): Observable<MenuModel[]> {
      return this.http.get<MenuModel[]>(`${BASE_URL}/active`);
    }
      */
  selById(c: MenuModel): Observable<MenuModel> {
    return this.http.get<MenuModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  insert(c: MenuModel): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, c);
  }

  update(c: MenuModel): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}`, c);
  }

  updateState(c: MenuModel): Observable<number> {
    return this.http.patch<number>(`${this.baseUrl}/state`, c);
  }

  deleteById(c: MenuModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

}
