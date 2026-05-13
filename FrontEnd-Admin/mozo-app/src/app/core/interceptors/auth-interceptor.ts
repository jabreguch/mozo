import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { catchError, switchMap, throwError } from 'rxjs';

const PUBLIC_ENDPOINTS = [
  '/login/permiso/login',
  '/login/permiso/login-renew'
];

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  // Skip públicos endpoints
  if (PUBLIC_ENDPOINTS.some(endpoint => req.url.includes(endpoint))) {
    return next(req);
  }

  const token = authService.getToken();
  const authReq = token
    ? req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        return authService.refreshToken().pipe(
          switchMap(() => {
            const newToken = authService.getToken();
            const retryReq = req.clone({
              setHeaders: { Authorization: `Bearer ${newToken}` }
            });
            return next(retryReq);
          }),
          catchError(() => throwError(() => error))
        );
      }
      return throwError(() => error);
    })
  );

};
