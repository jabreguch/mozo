// src/app/modules/maestro/services/archivo.service.ts
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpEvent, HttpEventType, HttpRequest } from '@angular/common/http';
import { Observable, from, map, switchMap, filter } from 'rxjs';
import { cleanParams } from '@app/core/global/params.helper';
import { UrlApiService } from '@app/core/services/url-api.service';
import { API_ROUTES } from '@app/core/global/constants';
import { ArchivoFilterDto, ArchivoModel, ArchivoUploadParams, DownloadUrlOptions, OrdenMasivoRequest, UploadProgress } from '@app/shared/models/maestro/archivo.model';
import { ImageCompressionService } from '@app/core/services/image-compression.service ';


@Injectable({ providedIn: 'root' })
export class ArchivoService {
  private http = inject(HttpClient);
  private api = inject(UrlApiService);
  private comp = inject(ImageCompressionService);
  private baseUrl = this.api.buildUrl(API_ROUTES.maestro.archivo);

  // ============================================================
  // LECTURA
  // ============================================================

  /**
   * Obtiene un archivo por ID
   */
  selById(filter: ArchivoFilterDto): Observable<ArchivoModel> {
    return this.http.get<ArchivoModel>(`${this.baseUrl}/byid`, {
      params: cleanParams(filter)
    });
  }

  /**
   * Obtiene un archivo por UK (empresa + tipoEntidad + entidad + tipo + orden)
   * Útil para casos Single donde el archivo es único por tipo
   */
  selMetaDataByUk(filter: ArchivoFilterDto): Observable<ArchivoModel> {
    return this.http.get<ArchivoModel>(`${this.baseUrl}/meta-data-byuk`, {
      params: cleanParams(filter)
    });
  }

  /**
   * Obtiene metadata de archivo por ID
   */
  selMetaDataById(filter: ArchivoFilterDto): Observable<ArchivoModel> {
    return this.http.get<ArchivoModel>(`${this.baseUrl}/metadata-byid`, {
      params: cleanParams(filter)
    });
  }

  /**
   * Obtiene todos los archivos de una entidad (sin filtro de estado)
   */
  selAll(filter: ArchivoFilterDto): Observable<ArchivoModel[]> {
    return this.http.get<ArchivoModel[]>(this.baseUrl, {
      params: cleanParams(filter)
    });
  }

  /**
   * Obtiene archivos activos de una entidad
   */
  selAllActive(filter: ArchivoFilterDto): Observable<ArchivoModel[]> {
    return this.http.get<ArchivoModel[]>(`${this.baseUrl}/active`, {
      params: cleanParams(filter)
    });
  }

  /**
   * Obtiene la galería de archivos de un tipo específico, ordenada
   */
  selGaleria(filter: ArchivoFilterDto): Observable<ArchivoModel[]> {
    return this.selAllActive(filter).pipe(
      map(archivos => archivos
        .filter(a => a.coTipo === filter.coTipo)
        .sort((a, b) => (a.nuOrden ?? 0) - (b.nuOrden ?? 0))
      )
    );
  }

  // ============================================================
  // UPLOAD / REPLACE
  // ============================================================

  /**
   * Sube un archivo nuevo (legacy - mantenido por compatibilidad)
   */
  insert(file: File, params: ArchivoUploadParams): Observable<UploadProgress> {
    return this.upload(file, params);
  }

  /**
   * Sube un archivo nuevo con compresión automática si es imagen
   */
  upload(file: File, params: ArchivoUploadParams): Observable<UploadProgress> {
    if (this.comp.esImagen(file)) {
      return from(this.comp.comprimirParaUpload(file)).pipe(
        switchMap(comp => this.subirArchivo(comp, params, true, 'POST'))
      );
    }
    return this.subirArchivo(file, params, false, 'POST');
  }

  /**
   * Reemplaza un archivo existente
   */
  reemplazar(
    coArchivo: number,
    file: File,
    params: ArchivoUploadParams
  ): Observable<UploadProgress> {
    const paramsConId = { ...params, coArchivo };

    if (this.comp.esImagen(file)) {
      return from(this.comp.comprimirParaUpload(file)).pipe(
        switchMap(comp => this.subirArchivo(comp, paramsConId, true, 'PUT'))
      );
    }
    return this.subirArchivo(file, paramsConId, false, 'PUT');
  }

  /**
   * Lógica común de subida con soporte POST (nuevo) y PUT (reemplazo)
   */
  private subirArchivo(
    file: File,
    params: ArchivoUploadParams & { coArchivo?: number },
    procesadoEnCliente: boolean,
    method: 'POST' | 'PUT'
  ): Observable<UploadProgress> {
    const fd = new FormData();
    fd.append('file', file, file.name);
    fd.append('procesadoEnCliente', String(procesadoEnCliente));
    fd.append('nombreOriginal', file.name);

    Object.entries(params).forEach(([k, v]) => {
      if (v !== undefined && v !== null) fd.append(k, String(v));
    });

    const url = `${this.baseUrl}/meta-data`;

    const req = new HttpRequest(method, url, fd, {
      reportProgress: true
    });

    return this.http.request<ArchivoModel>(req).pipe(
      map(ev => this.mapEvent(ev)),
      filter((p): p is UploadProgress => p !== null)
    );
  }

  // ============================================================
  // UPDATE
  // ============================================================

  /**
   * Actualiza metadata descriptiva (descripción, título, default, orden)
   */
  update(archivo: ArchivoModel): Observable<number> {
    return this.http.put<number>(this.baseUrl, archivo);
  }

  /**
   * Actualiza solo el orden de un archivo
   */
  updateOrden(archivo: ArchivoModel): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/orden`, archivo);
  }

  /**
   * Reordena varios archivos a la vez
   */
  updateOrdenMasivo(request: OrdenMasivoRequest): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/orden-masivo`, request);
  }

  // ============================================================
  // DELETE
  // ============================================================

  /**
   * Elimina un archivo por filter
   */
  deleteById(filter: ArchivoFilterDto): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/byid`, {
      params: cleanParams(filter)
    });
  }

  /**
   * Elimina un archivo y su archivo físico (alias de deleteById)
   */
  delete(filter: ArchivoFilterDto): Observable<void> {
    return this.deleteById(filter);
  }

  /**
   * Elimina archivo completo incluyendo caché
   */
  deleteFull(coArchivo: number, coEmpresa?: number): Observable<void> {
    const params: any = { coArchivo };
    if (coEmpresa) params.coEmpresa = coEmpresa;
    return this.http.delete<void>(`${this.baseUrl}/full`, { params });
  }

  // ============================================================
  // DOWNLOAD / URLs
  // ============================================================

  /**
   * URL para usar en <img [src]="..."> con caché y resize automático
   */
  getDownloadUrl(coArchivo: number, opts?: DownloadUrlOptions): string {
    const p = new URLSearchParams();
    p.set('coArchivo', String(coArchivo));
    if (opts?.coEmpresa) p.set('coEmpresa', String(opts.coEmpresa));
    if (opts?.width) p.set('width', String(opts.width));
    if (opts?.format) p.set('format', opts.format);
    return `${this.baseUrl}/download?${p.toString()}`;
  }

  /**
   * srcset para imágenes responsive
   */
  getSrcSet(
    coArchivo: number,
    widths: number[] = [200, 400, 800, 1600],
    format: 'webp' | 'jpg' = 'webp'
  ): string {
    return widths
      .map(w => `${this.getDownloadUrl(coArchivo, { width: w, format })} ${w}w`)
      .join(', ');
  }

  /**
   * Descarga un archivo como Blob (para descarga manual)
   */
  download(coArchivo: number, coEmpresa?: number): Observable<Blob> {
    const params: any = { coArchivo };
    if (coEmpresa) params.coEmpresa = coEmpresa;
    return this.http.get(`${this.baseUrl}/download`, {
      params,
      responseType: 'blob'
    });
  }

  // ============================================================
  // HELPERS PRIVADOS
  // ============================================================

  /**
   * Mapea eventos HTTP a UploadProgress
   */
  private mapEvent(ev: HttpEvent<any>): UploadProgress | null {
    switch (ev.type) {
      case HttpEventType.UploadProgress:
        return {
          progreso: ev.total ? Math.round((100 * ev.loaded) / ev.total) : 0,
          completado: false,
          fase: 'subiendo'
        };

      case HttpEventType.Response:
        const archivo = ev.body as ArchivoModel;
        return {
          progreso: 100,
          completado: true,
          archivo: archivo,
          ids: archivo?.coArchivo ? [archivo.coArchivo] : [],
          fase: 'finalizado'
        };

      case HttpEventType.Sent:
      case HttpEventType.ResponseHeader:
        return null;

      default:
        return null;
    }
  }
}
