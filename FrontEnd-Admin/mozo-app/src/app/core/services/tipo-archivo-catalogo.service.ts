// src/app/core/services/tipo-archivo-catalogo.service.ts
import { Injectable, signal, inject } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { TipoGeneralService } from '@app/modules/maestro/services/tipo-general.service';
import { TipoGeneralModel } from '@app/shared/models/maestro/tipo-general.model';
import { TIPO_MAESTRO } from '../global/tipo.constants';
import { STORAGE_KEYS } from '../global/constants';

export interface TipoArchivoConfigJson {
  coTabla: number;
  noTitulo: string;
  txExtensiones: string;
  flGaleria: boolean | number;
}

export interface TipoArchivoCatalogo {
  coTipo: number;
  coTabla: number;
  noTitulo: string;
  txExtensiones: string;
  flGaleria: boolean;
}

export interface ArchivoUploaderConfig {
  coEmpresa: number | null;
  flEmpresaNotKey: number;
  coTipoEntidad: number;
  coEntidad: number;
  coTipo: number;
  accept: string;
  label: string;
  maxSizeMB: number;
  flGaleria: boolean;
}

@Injectable({ providedIn: 'root' })
export class TipoArchivoCatalogoService {
  private tipoGeneralService = inject(TipoGeneralService);



  // Inicializa desde sessionStorage si existe
  private readonly _tipos = signal<TipoArchivoCatalogo[]>(
    this.leerStorage()
  );
  private readonly _cargado = signal(
    this.leerStorage().length > 0
  );

  readonly tipos = this._tipos.asReadonly();
  readonly cargado = this._cargado.asReadonly();

  // ===== STORAGE =====

  private leerStorage(): TipoArchivoCatalogo[] {
    try {
      const raw = sessionStorage.getItem(STORAGE_KEYS.TIPO_ARCHIVO_CATALOGO);
      if (!raw) return [];
      return JSON.parse(raw) as TipoArchivoCatalogo[];
    } catch {
      return [];
    }
  }

  private escribirStorage(tipos: TipoArchivoCatalogo[]): void {
    try {
      sessionStorage.setItem(STORAGE_KEYS.TIPO_ARCHIVO_CATALOGO, JSON.stringify(tipos));
    } catch (e) {
      console.warn('No se pudo guardar catálogo en sessionStorage:', e);
    }
  }

  private limpiarStorage(): void {
    sessionStorage.removeItem(STORAGE_KEYS.TIPO_ARCHIVO_CATALOGO);
  }

  // ===== CARGA =====

  async cargar(): Promise<void> {
    if (this._cargado()) return;

    try {
      const filtro: TipoGeneralModel = {
        coGrupo: TIPO_MAESTRO.general.formatoArchivo.grupo
      };

      const tiposGenerales = await firstValueFrom(
        this.tipoGeneralService.selAllActive(filtro)
      );

      const catalogo = tiposGenerales
        .map(t => this.mapearTipo(t))
        .filter((t): t is TipoArchivoCatalogo => t !== null);

      this._tipos.set(catalogo);
      this._cargado.set(true);
      this.escribirStorage(catalogo); // persiste
    } catch (error) {
      console.error('Error cargando catálogo de tipos de archivo:', error);
    }
  }

  async recargar(): Promise<void> {
    this._cargado.set(false);
    this._tipos.set([]);
    this.limpiarStorage();
    await this.cargar();
  }

  /**
   * Llamar al hacer logout
   */
  limpiar(): void {
    this._tipos.set([]);
    this._cargado.set(false);
    this.limpiarStorage();
  }

  // ===== MAPEO =====

  private mapearTipo(t: TipoGeneralModel): TipoArchivoCatalogo | null {
    const config = this.parsearValor(t.valor);
    if (!config) {
      console.warn(`Tipo ${t.coTipo} sin configuración válida`);
      return null;
    }

    return {
      coTipo: t.coTipo ?? 0,
      coTabla: config.coTabla,
      noTitulo: config.noTitulo,
      txExtensiones: config.txExtensiones,
      flGaleria: this.normalizarBoolean(config.flGaleria)
    };
  }

  private parsearValor(valor: any): TipoArchivoConfigJson | null {
    if (!valor) return null;
    try {
      if (typeof valor === 'object') return valor as TipoArchivoConfigJson;
      if (typeof valor === 'string') return JSON.parse(valor) as TipoArchivoConfigJson;
      return null;
    } catch (e) {
      console.error('Error parseando valor:', valor, e);
      return null;
    }
  }

  private normalizarBoolean(valor: any): boolean {
    if (typeof valor === 'boolean') return valor;
    if (typeof valor === 'number') return valor === 1;
    if (typeof valor === 'string') return valor === '1' || valor.toLowerCase() === 'true';
    return false;
  }

  // ===== BÚSQUEDA =====

  getByCodigo(coTipo: number): TipoArchivoCatalogo | undefined {
    return this._tipos().find(t => t.coTipo === coTipo);
  }

  getByTabla(coTabla: number): TipoArchivoCatalogo[] {
    return this._tipos().filter(t => t.coTabla === coTabla);
  }

  getAccept(coTipo: number): string {
    return this.getByCodigo(coTipo)?.txExtensiones ?? '*/*';
  }

  getTitulo(coTipo: number): string {
    return this.getByCodigo(coTipo)?.noTitulo ?? `Tipo ${coTipo}`;
  }

  esGaleria(coTipo: number): boolean {
    return this.getByCodigo(coTipo)?.flGaleria === true;
  }

  getTabla(coTipo: number): number {
    return this.getByCodigo(coTipo)?.coTabla ?? 0;
  }

  // ===== BUILDER =====

  buildConfig(
    coTipo: number,
    coEntidad: number,
    extras?: {
      coEmpresa?: number | null;
      flEmpresaNotKey?: number;
      label?: string;
      maxSizeMB?: number;
    }
  ): ArchivoUploaderConfig | null {
    const tipo = this.getByCodigo(coTipo);
    if (!tipo) {
      console.warn(`Tipo de archivo ${coTipo} no encontrado en catálogo`);
      return null;
    }

    return {
      coEmpresa: extras?.coEmpresa ?? null,
      flEmpresaNotKey: extras?.flEmpresaNotKey ?? 0,
      coTipoEntidad: tipo.coTabla,
      coEntidad,
      coTipo,
      accept: tipo.txExtensiones,
      label: extras?.label ?? tipo.noTitulo,
      maxSizeMB: extras?.maxSizeMB ?? 10,
      flGaleria: tipo.flGaleria
    };
  }
}
