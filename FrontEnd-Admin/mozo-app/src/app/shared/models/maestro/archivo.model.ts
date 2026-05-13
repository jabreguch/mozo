import { BaseModel } from "@sharedModels/base.model";

export interface ArchivoModel extends BaseModel {
  coEmpresa?: number | null;
  coArchivo: number;
  coTipoEntidad: number;
  coEntidad: number;
  coTipo: number;

  noArchivo?: string;
  noExtension?: string;
  noRuta?: string;
  nuBytes: number;
  nuAlto?: number | null;
  nuAncho?: number | null;

  nuOrden: number;
  txDescripcion?: string;
  flDefault: number;
  noTitulo?: string;

  // Calculados desde el backend
  url?: string;
  contentType?: string;
}

// ===== INTERFACES =====

export interface ArchivoUploadParams {
  flEmpresaNotKey?: number;
  coEmpresa?: number | null;
  coTipoEntidad: number;
  coEntidad: number;
  coTipo: number;
  nuOrden?: number;
  txDescripcion?: string;
  noTitulo?: string;
  flDefault?: number;
  flGaleria:number;
}

export interface UploadProgress {
  progreso: number;
  completado: boolean;
  archivo?: ArchivoModel;
  ids?: number[];
  fase?: 'comprimiendo' | 'subiendo' | 'finalizado';
}

export interface DownloadUrlOptions {
  width?: number;
  format?: 'webp' | 'jpg' | 'png';
  coEmpresa?: number;
}

export interface ArchivoFilterDto {
  flEmpresaNotKey?: number;
  coEmpresa?: number | null;
  coArchivo?: number;
  coTipoEntidad?: number;
  coEntidad?: number;
  coTipo?: number;
  nuOrden?: number;
  flGaleria?: number;
}

export interface OrdenArchivoDto {
  coArchivo: number;
  nuOrden: number;
}

export interface OrdenMasivoRequest {
  flEmpresaNotKey: number;
  coEmpresa?: number | null;
  ordenes: OrdenArchivoDto[];
}

