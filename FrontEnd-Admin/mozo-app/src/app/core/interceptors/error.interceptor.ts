import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

/**
 * Interfaz para ProblemDetails de .NET
 */
interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number; // ✅ Hacer opcional
  detail?: string;
  code?: string;
  errors?: ErrorDetail[];
  timestamp?: string;
  traceId?: string;
  message?: string;
  [key: string]: any;
}

interface ErrorDetail {
  field: string;
  message: string;
  code: string;
}

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const problemDetails: ProblemDetails = error.error || {};

      // Registrar para debugging
      console.error('Error Response:', {
        httpStatus: error.status,
        apiStatus: problemDetails.status,
        code: problemDetails.code,
        detail: problemDetails.detail,
        traceId: problemDetails.traceId
      });

      // ✅ Obtener mensaje
      const message = problemDetails.detail || problemDetails.message || 'Ocurrió un error inesperado';
      const title = problemDetails.title || `Error ${error.status}`;
      const code = problemDetails.code || error.status.toString();

      // Manejar según status HTTP
      switch (error.status) {
        case 400:
          handleBadRequest(toastr, problemDetails, message, title, code);
          break;

        case 401:
          handleUnauthorized(toastr, router, message, code);
          break;

        case 403:
          handleForbidden(toastr, router, message, code);
          break;

        case 404:
          handleNotFound(toastr, message, code);
          break;

        case 500:
          handleInternalServerError(toastr, problemDetails, message, code);
          break;

        default:
          toastr.error(message, 'Error');
          break;
      }

      return throwError(() => error);
    })
  );
};

/**
 * Maneja errores 400 - Bad Request / Validación
 */
function handleBadRequest(
  toastr: ToastrService,
  problemDetails: ProblemDetails,
  message: string,
  title: string,
  code: string
): void {
  if (problemDetails.errors && Array.isArray(problemDetails.errors)) {
    const errors = problemDetails.errors as ErrorDetail[];

    if (errors.length > 1) {
      // Mostrar primero la validación general
      toastr.error(message || 'Errores de validación', `${title} (${code})`);
      // Y luego un resumen de otros errores
      console.warn('Errores de validación:', errors);
    } else if (errors.length === 1) {
      toastr.error(
        `${errors[0].field}: ${errors[0].message}`,
        `Validación (${code})`
      );
    }
  } else {
    toastr.error(message, `${title} (${code})`);
  }
}

/**
 * Maneja errores 401 - No autorizado
 */
function handleUnauthorized(
  toastr: ToastrService,
  router: Router,
  message: string,
  code: string
): void {
  toastr.warning(
    message || 'Sesión expirada, inicia sesión nuevamente',
    `No autorizado (${code})`
  );

  // Limpiar almacenamiento
  localStorage.removeItem('token');
  localStorage.removeItem('refreshToken');
  localStorage.removeItem('credencial');

  // Redirigir después de un pequeño delay
  setTimeout(() => {
    router.navigate(['/login']);
  }, 1500);
}

/**
 * Maneja errores 403 - Acceso denegado
 */
function handleForbidden(
  toastr: ToastrService,
  router: Router,
  message: string,
  code: string
): void {
  toastr.error(
    message || 'No tienes permisos para realizar esta acción',
    `Prohibido (${code})`
  );
  router.navigate(['/forbidden']);
}

/**
 * Maneja errores 404 - No encontrado
 */
function handleNotFound(
  toastr: ToastrService,
  message: string,
  code: string
): void {
  toastr.error(message || 'Recurso no encontrado', `Error ${code}`);
}

/**
 * Maneja errores 500 - Error interno del servidor
 */
function handleInternalServerError(
  toastr: ToastrService,
  problemDetails: ProblemDetails,
  message: string,
  code: string
): void {
  toastr.error(
    message || 'Error interno del servidor',
    `Error ${code}`
  );

  if (problemDetails.traceId) {
    console.error('Trace ID:', problemDetails.traceId);
    // ✅ Eliminar la verificación de environment
    console.log(`Para más detalles, busca el Trace ID en los logs del servidor: ${problemDetails.traceId}`);
  }
}
