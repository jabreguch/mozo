// src/app/core/services/image-compression.service.ts
import { Injectable } from '@angular/core';
import imageCompression, { Options } from 'browser-image-compression';

// ===== INTERFACES =====

export interface EstandarImagen {
  maxWidth: number;
  maxSizeMB: number;
  quality: number;
  formato: 'webp' | 'jpeg' | 'png';
}

export const ESTANDAR_IMAGEN: {
  upload: EstandarImagen;
  thumbnail: EstandarImagen;
  preview: EstandarImagen;
} = {
  // Para upload final al servidor
  upload: {
    maxWidth: 2560,
    maxSizeMB: 1,
    quality: 0.85,
    formato: 'webp'
  },
  // Para thumbnails de listados
  thumbnail: {
    maxWidth: 200,
    maxSizeMB: 0.05,
    quality: 0.75,
    formato: 'webp'
  },
  // Para preview en formularios
  preview: {
    maxWidth: 800,
    maxSizeMB: 0.3,
    quality: 0.80,
    formato: 'webp'
  }
};

export interface ValidacionImagen {
  valido: boolean;
  errores: string[];
  advertencias: string[];
  ancho?: number;
  alto?: number;
}

export interface DimensionesImagen {
  ancho: number;
  alto: number;
}

@Injectable({ providedIn: 'root' })
export class ImageCompressionService {

  // ===== CONSTANTES =====

  readonly LIMITES = {
    maxBytes: 20 * 1024 * 1024,
    maxAncho: 8000,
    maxAlto: 8000,
    minAncho: 50,
    minAlto: 50
  };

  private readonly imageMimes = [
    'image/jpeg', 'image/jpg', 'image/png',
    'image/webp', 'image/bmp', 'image/gif',
    'image/heic', 'image/heif'
  ];

  private readonly imageExtensions = [
    '.jpg', '.jpeg', '.png', '.webp',
    '.bmp', '.gif', '.heic', '.heif'
  ];

  // ===== DETECCIÓN DE TIPO =====

  /**
   * Verifica si un archivo es imagen
   */
  esImagen(file: File): boolean {
    return this.imageMimes.includes(file.type.toLowerCase()) ||
           /\.(jpe?g|png|webp|bmp|gif|heic|heif)$/i.test(file.name);
  }

  /**
   * Verifica si una extensión corresponde a imagen
   */
  esExtensionImagen(extension: string): boolean {
    const ext = extension.startsWith('.') ? extension : `.${extension}`;
    return this.imageExtensions.includes(ext.toLowerCase());
  }

  /**
   * Obtiene la extensión de un archivo
   */
  getExtension(filename: string): string {
    const idx = filename.lastIndexOf('.');
    return idx >= 0 ? filename.substring(idx).toLowerCase() : '';
  }

  /**
   * Obtiene el nombre sin extensión
   */
  getNombreSinExtension(filename: string): string {
    return filename.replace(/\.[^/.]+$/, '');
  }

  // ===== VALIDACIÓN =====

  /**
   * Valida con política permisiva: errores solo si es imposible procesar
   */
  async validar(file: File): Promise<ValidacionImagen> {
    const r: ValidacionImagen = {
      valido: true,
      errores: [],
      advertencias: []
    };

    if (!this.esImagen(file)) {
      r.errores.push('No es una imagen válida');
      r.valido = false;
      return r;
    }

    if (file.size > this.LIMITES.maxBytes) {
      r.errores.push(`Imagen excede ${this.fmt(this.LIMITES.maxBytes)} (tope absoluto)`);
      r.valido = false;
    }

    if (/\.(heic|heif)$/i.test(file.name)) {
      r.advertencias.push('HEIC/HEIF se convertirá a WebP');
    }

    try {
      const dim = await this.getDimensiones(file);
      r.ancho = dim.ancho;
      r.alto = dim.alto;

      if (dim.ancho < this.LIMITES.minAncho || dim.alto < this.LIMITES.minAlto) {
        r.errores.push(`Imagen muy pequeña (${dim.ancho}x${dim.alto}px)`);
        r.valido = false;
      }
      if (dim.ancho > this.LIMITES.maxAncho || dim.alto > this.LIMITES.maxAlto) {
        r.errores.push(`Resolución máxima ${this.LIMITES.maxAncho}x${this.LIMITES.maxAlto}px`);
        r.valido = false;
      }

      if (file.size > 5 * 1024 * 1024) {
        r.advertencias.push(`Pesa ${this.fmt(file.size)}, se comprimirá`);
      }
      if (dim.ancho > ESTANDAR_IMAGEN.upload.maxWidth) {
        r.advertencias.push(`Se redimensionará a ${ESTANDAR_IMAGEN.upload.maxWidth}px de ancho`);
      }
    } catch {
      r.errores.push('No se pudo leer la imagen (puede estar corrupta)');
      r.valido = false;
    }
    return r;
  }

  /**
   * Validación con dimensiones personalizadas
   */
  async validarConOpciones(file: File, opciones?: {
    minWidth?: number;
    minHeight?: number;
    maxWidth?: number;
    maxHeight?: number;
    maxBytes?: number;
  }): Promise<ValidacionImagen> {
    const r: ValidacionImagen = {
      valido: true,
      errores: [],
      advertencias: []
    };

    if (!this.esImagen(file)) {
      r.errores.push('No es una imagen válida');
      r.valido = false;
      return r;
    }

    const maxBytes = opciones?.maxBytes ?? this.LIMITES.maxBytes;
    const minW = opciones?.minWidth ?? this.LIMITES.minAncho;
    const minH = opciones?.minHeight ?? this.LIMITES.minAlto;
    const maxW = opciones?.maxWidth ?? this.LIMITES.maxAncho;
    const maxH = opciones?.maxHeight ?? this.LIMITES.maxAlto;

    if (file.size > maxBytes) {
      r.errores.push(`Imagen excede ${this.fmt(maxBytes)}`);
      r.valido = false;
    }

    try {
      const dim = await this.getDimensiones(file);
      r.ancho = dim.ancho;
      r.alto = dim.alto;

      if (dim.ancho < minW || dim.alto < minH) {
        r.errores.push(`Mínimo ${minW}x${minH}px (actual ${dim.ancho}x${dim.alto}px)`);
        r.valido = false;
      }
      if (dim.ancho > maxW || dim.alto > maxH) {
        r.advertencias.push(`Se redimensionará a máximo ${maxW}x${maxH}px`);
      }
    } catch {
      r.errores.push('No se pudo leer la imagen');
      r.valido = false;
    }

    return r;
  }

  // ===== COMPRESIÓN =====

  /**
   * Comprime imagen para upload (maxWidth: 2560, calidad 85%, WebP)
   */
  async comprimirParaUpload(file: File): Promise<File> {
    return this.comprimirConEstandar(file, ESTANDAR_IMAGEN.upload);
  }

  /**
   * Comprime imagen para thumbnail (maxWidth: 200)
   */
  async comprimirParaThumbnail(file: File): Promise<File> {
    return this.comprimirConEstandar(file, ESTANDAR_IMAGEN.thumbnail);
  }

  /**
   * Comprime imagen para preview (maxWidth: 800)
   */
  async comprimirParaPreview(file: File): Promise<File> {
    return this.comprimirConEstandar(file, ESTANDAR_IMAGEN.preview);
  }

  /**
   * Comprime con configuración específica
   */
  async comprimirConEstandar(file: File, est: EstandarImagen): Promise<File> {
    const fileType = `image/${est.formato === 'jpeg' ? 'jpeg' : est.formato}`;

    const options: Options = {
      maxSizeMB: est.maxSizeMB,
      maxWidthOrHeight: est.maxWidth,
      useWebWorker: true,
      initialQuality: est.quality,
      fileType
    };

    try {
      const blob = await imageCompression(file, options);
      const ext = est.formato === 'jpeg' ? 'jpg' : est.formato;
      const nuevoNombre = this.cambiarExtension(file.name, ext);
      return new File([blob], nuevoNombre, { type: fileType });
    } catch (error) {
      console.error('Error comprimiendo imagen:', error);
      throw new Error('No se pudo comprimir la imagen');
    }
  }

  // ===== DIMENSIONES =====

  /**
   * Obtiene las dimensiones de una imagen
   */
  async getDimensiones(blob: Blob): Promise<DimensionesImagen> {
    return new Promise((resolve, reject) => {
      const url = URL.createObjectURL(blob);
      const img = new Image();
      img.onload = () => {
        URL.revokeObjectURL(url);
        resolve({ ancho: img.naturalWidth, alto: img.naturalHeight });
      };
      img.onerror = e => {
        URL.revokeObjectURL(url);
        reject(e);
      };
      img.src = url;
    });
  }

  // ===== PREVIEW =====

  /**
   * Genera preview en base64 (data URL)
   * Útil para mostrar mientras se sube
   */
  async generarPreview(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = e => reject(e);
      reader.readAsDataURL(file);
    });
  }

  /**
   * Genera preview optimizado (comprime primero para que pese menos)
   */
  async generarPreviewOptimizado(file: File): Promise<string> {
    if (!this.esImagen(file)) {
      return this.generarPreview(file);
    }

    try {
      // Comprimir antes de generar preview
      const comprimido = await this.comprimirParaPreview(file);
      return this.generarPreview(comprimido);
    } catch {
      // Si falla la compresión, usar el original
      return this.generarPreview(file);
    }
  }

  // ===== UTILIDADES =====

  /**
   * Cambia la extensión de un archivo
   */
  cambiarExtension(nombre: string, ext: string): string {
    const extLimpia = ext.startsWith('.') ? ext.substring(1) : ext;
    return nombre.replace(/\.[^/.]+$/, '') + '.' + extLimpia;
  }

  /**
   * Convierte File a Blob
   */
  fileToBlob(file: File): Blob {
    return new Blob([file], { type: file.type });
  }

  /**
   * Convierte data URL (base64) a File
   */
  dataUrlToFile(dataUrl: string, filename: string): File {
    const arr = dataUrl.split(',');
    const mime = arr[0].match(/:(.*?);/)?.[1] ?? 'image/png';
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);
    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
  }

  /**
   * Formatea bytes a unidad legible
   */
  formatearBytes(bytes: number): string {
    return this.fmt(bytes);
  }

  // ===== HELPERS PRIVADOS =====

  private fmt(bytes: number): string {
    if (bytes < 1024) return `${bytes} B`;
    if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(0)} KB`;
    return `${(bytes / 1024 / 1024).toFixed(1)} MB`;
  }
}
