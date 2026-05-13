import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, filter, Observable, of, switchMap, take, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CredencialResponseModel } from '@sharedModels/seguridad/auth/credencial-response.model';
import { API_ROUTES, STORAGE_KEYS } from '@app/core/global/constants';
import { ModuloUsuarioModel } from '@app/shared/models/seguridad/modulo-usuario.model';
import { OptionService } from '@app/core/services/option.service';
import { PermisoModel } from '@app/shared/models/seguridad/permiso.model';
import { UrlApiService } from './url-api.service';
import { TipoArchivoCatalogoService } from './tipo-archivo-catalogo.service';

//const BASE_URL = `${environment.baseUrl}/login/permiso`;
//const LOGIN_ENDPOINT = `${BASE_URL}/login`;
//const LOGIN_RENEW_ENDPOINT = `${BASE_URL}/login/login-renew`;
//const LOGIN_OPCIONES = `${environment.baseUrl}/login/menu/modulos`;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = inject(UrlApiService);
  private http = inject(HttpClient);
  private optionService = inject(OptionService);

  private baseUrl = this.api.buildUrl(API_ROUTES.login.permiso);
  private baseMenuUrl = this.api.buildUrl(API_ROUTES.login.menu);

  private userSubject = new BehaviorSubject<any>(this.getUser());
  user$ = this.userSubject.asObservable();

  private isRefreshing = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);
   private readonly tipoArchivoCatalogoService = inject(TipoArchivoCatalogoService);


  /**
   * Realiza el login del usuario
   */
  login(c: PermisoModel): Observable<CredencialResponseModel> {
    return this.http.post<CredencialResponseModel>(`${this.baseUrl}/login`, {
      noUsuario: c.noUsuario,
      noClave: c.noClave
    }).pipe(
      tap(response => this.handleAuthSuccess(response))
    );
  }

  /**
   * Renueva el token usando refresh token
   */
  refreshToken(): Observable<CredencialResponseModel | null> {
    if (this.isRefreshing) {
      return this.refreshTokenSubject.pipe(
        filter(token => token !== null),
        take(1),
        switchMap(() => of(null))
      );
    }

    this.isRefreshing = true;
    this.refreshTokenSubject.next(null);

    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      this.logout();
      return throwError(() => new Error('No refresh token disponible'));
    }

    return this.http.post<CredencialResponseModel>(`${this.baseUrl}/login-renew`, {
      noRefreshToken: refreshToken
    }).pipe(
      tap(response => this.handleAuthSuccess(response)),
      catchError(error => {
        this.logout();
        return throwError(() => error);
      }),
      tap(() => { this.isRefreshing = false; }),
      catchError(error => {
        this.isRefreshing = false;
        this.refreshTokenSubject.next(null);
        return throwError(() => error);
      })
    );
  }

  /**
   * Obtiene los módulos/menús del usuario
   */
  getMenus(): Observable<ModuloUsuarioModel[]> {
    return this.http.get<ModuloUsuarioModel[]>(`${this.baseMenuUrl}/modulos`);
  }

  /**
   * Maneja la respuesta exitosa de autenticación
   */
  private handleAuthSuccess(response: CredencialResponseModel): void {
    const { credencial, noToken, noTokenRefresh } = response;
    this.saveToken(noToken);
    this.saveRefreshToken(noTokenRefresh);
    this.saveUser(credencial);
    this.userSubject.next(credencial);
    this.refreshTokenSubject.next(noToken);

  }

  /**
   * Cierra sesión y limpia datos
   */
  logout(): void {
    this.removeToken();
    this.removeRefreshToken();
    this.removeUser();
    this.optionService.clear();
    this.userSubject.next(null);
    this.refreshTokenSubject.next(null);
     this.tipoArchivoCatalogoService.limpiar(); // limpia catálogo
    this.isRefreshing = false;
  }

  /**
   * Verifica si el usuario está autenticado
   */
  isAuthenticated(): boolean {
    const exp = this.getTokenExpiration();
    const valid = !!exp && Date.now() < exp;
    return valid;
  }

  /**
   * Verifica si el token expira en menos de 1 minuto
   */
  isTokenExpiringSoon(): boolean {
    const exp = this.getTokenExpiration();
    if (!exp) return true;
    return (exp - Date.now()) < 60 * 1000; // 1 minuto
  }

  // ==================== TOKEN HELPERS ====================

  private saveToken(token: string): void {
    localStorage.setItem(STORAGE_KEYS.TOKEN, token);
  }

  getToken(): string | null {
    return localStorage.getItem(STORAGE_KEYS.TOKEN);
  }

  private removeToken(): void {
    localStorage.removeItem(STORAGE_KEYS.TOKEN);
  }

  private saveRefreshToken(token: string): void {
    localStorage.setItem(STORAGE_KEYS.REFRESH_TOKEN, token);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(STORAGE_KEYS.REFRESH_TOKEN);
  }

  private removeRefreshToken(): void {
    localStorage.removeItem(STORAGE_KEYS.REFRESH_TOKEN);
  }

  private saveUser(user: any): void {
    localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(user));
  }

  private removeUser(): void {
    localStorage.removeItem(STORAGE_KEYS.USER);
  }

  getUser(): any {
    const user = localStorage.getItem(STORAGE_KEYS.USER);
    return user ? JSON.parse(user) : null;
  }

  // ==================== JWT HELPERS ====================

  private decodeToken(token: string): any {
    try {
      let payload = token.split('.')[1];
      payload = payload.replace(/-/g, '+').replace(/_/g, '/');
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }

  getTokenExpiration(): number | null {
    const token = this.getToken();
    if (!token) return null;

    const decoded = this.decodeToken(token);
    return decoded?.exp ? decoded.exp * 1000 : null;
  }
}
