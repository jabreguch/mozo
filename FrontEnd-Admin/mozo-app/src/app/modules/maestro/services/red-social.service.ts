import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { cleanParams } from '@app/core/global/params.helper';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';
import { RedSocialModel } from '@app/shared/models/maestro/red-social.model';


@Injectable({
  providedIn: 'root'
})
export class RedSocialService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private baseUrl = this.api.buildUrl(API_ROUTES.maestro.redSocial);

  selAll(c: RedSocialModel): Observable<RedSocialModel[]> {
    return this.http.get<RedSocialModel[]>(this.baseUrl, { params: cleanParams(c) });
  }

  selById(c: RedSocialModel): Observable<RedSocialModel> {
    return this.http.get<RedSocialModel>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

  insert(c: RedSocialModel): Observable<number> {
    return this.http.post<number>(this.baseUrl, c);
  }

  update(c: RedSocialModel): Observable<number> {
    return this.http.put<number>(this.baseUrl, c);
  }

  deleteById(c: RedSocialModel): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, { params: cleanParams(c) });
  }

}
