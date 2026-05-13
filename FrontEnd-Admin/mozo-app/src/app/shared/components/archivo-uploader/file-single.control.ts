// src/app/shared/components/archivo-single/archivo-single.component.ts
import {
  Component,
  inject,
  input,
  output,
  signal,
  computed,
  effect,
  ChangeDetectionStrategy
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArchivoService } from '@app/modules/maestro/services/archivo.service';
import { ArchivoSingleConfig } from './file-single-config.model';
import { ArchivoFilterDto, ArchivoModel } from '@app/shared/models/maestro/archivo.model';
import { ImageCompressionService } from '@app/core/services/image-compression.service ';
import { environment } from 'src/environments/environment';
import { ConfirmService } from '@app/shared/services/confirm.service';


type EstadoControl = 'vacio' | 'cargando' | 'subiendo' | 'reemplazando' | 'completo' | 'error';

@Component({
  selector: 'mz-file-single',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './file-single.control.html',
  styleUrl: './file-single.control.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FileSingleComponent {
  private readonly confirmService = inject(ConfirmService);
  dragOver = signal(false);
  // ===== Inputs =====
  config = input.required<ArchivoSingleConfig>();

  // ===== Outputs =====
  cargado = output<ArchivoModel>();
  reemplazado = output<ArchivoModel>();
  eliminado = output<number>();
  errorUpload = output<string>();

  // ===== Servicios =====
  private archivoService = inject(ArchivoService);
  private imgService = inject(ImageCompressionService);

  // ===== State =====
  archivo = signal<ArchivoModel | null>(null);
  estado = signal<EstadoControl>('vacio');
  progreso = signal(0);
  mensaje = signal('');
  previewLocal = signal('');  // preview mientras se sube

  // ===== Computed =====
  tieneArchivo = computed(() => this.archivo() !== null);

  esImagen = computed(() => {
    if (this.previewLocal()) return true;
    const arch = this.archivo();
    if (!arch?.contentType) return false;
    return arch.contentType.startsWith('image/');
  });

  urlPreview = computed(() => {
    if (this.previewLocal()) return this.previewLocal();
    return this.storageUrl(this.archivo()?.url);
  });


  procesando = computed(() =>
    this.estado() === 'subiendo' ||
    this.estado() === 'reemplazando' ||
    this.estado() === 'cargando'
  );

  iconoPorExtension = computed(() => {
    const arch = this.archivo();
    if (!arch?.noExtension) return '📎';

    if (this.esImagen()) return '🖼️';

    const ext = arch.noExtension.toLowerCase();
    switch (ext) {
      case '.pdf': return '📄';
      case '.doc': case '.docx': return '📝';
      case '.xls': case '.xlsx': return '📊';
      case '.txt': return '📃';
      case '.zip': case '.rar': return '🗜️';
      case '.msg': return '✉️';
      default: return '📎';
    }
  });

  constructor() {
    // Cargar archivo existente cuando cambia la config
    effect(() => {
      const cfg = this.config();
      if (cfg.coEntidad > 0 && (cfg.autoLoad ?? true)) {
        this.cargarArchivoExistente();
      }
    });
  }

  // ===== Carga inicial =====

  private cargarArchivoExistente() {
    const cfg = this.config();
    this.estado.set('cargando');

    const filter: ArchivoFilterDto = {
      flEmpresaNotKey: cfg.flEmpresaNotKey,
      coEmpresa: cfg.coEmpresa,
      coTipoEntidad: cfg.coTipoEntidad,
      coEntidad: cfg.coEntidad,
      coTipo: cfg.coTipo,
      flGaleria: 0,
      nuOrden: 1  // archivo único siempre tiene orden 1
    };

    this.archivoService.selMetaDataByUk(filter).subscribe({
      next: arch => {
        this.archivo.set(arch);
        this.estado.set('completo');
      },
      error: () => {
        this.archivo.set(null);
        this.estado.set('vacio');
      }
    });
  }

  // ===== Selección de archivo =====

  async onFileSelect(e: Event) {
    const input = e.target as HTMLInputElement;
    const file = input.files?.[0];
    input.value = '';  // limpia para permitir re-seleccionar mismo archivo

    if (!file) return;
    if (this.config().readonly) return;

    // Validar tamaño
    const maxSize = this.config().maxSizeMB * 1024 * 1024;
    if (file.size > maxSize) {
      this.setError(`El archivo excede ${this.config().maxSizeMB} MB`);
      return;
    }

    // Validar imagen (si aplica)
    if (this.imgService.esImagen(file)) {
      const validacion = await this.imgService.validar(file);
      if (!validacion.valido) {
        this.setError(validacion.errores.join('. '));
        return;
      }

      // Generar preview local mientras se sube
      this.previewLocal.set(await this.imgService.generarPreview(file));
    }

    this.mensaje.set('');

    // Decidir si subir o reemplazar
    if (this.tieneArchivo()) {
      this.reemplazarArchivo(file);
    } else {
      this.subirArchivo(file);
    }
  }

  // ===== Subir nuevo =====

  private subirArchivo(file: File) {
    const cfg = this.config();
    this.estado.set('subiendo');
    this.progreso.set(0);

    this.archivoService.upload(file, {
      flEmpresaNotKey: cfg.flEmpresaNotKey,
      coEmpresa: cfg.coEmpresa,
      coTipoEntidad: cfg.coTipoEntidad,
      coEntidad: cfg.coEntidad,
      coTipo: cfg.coTipo,
      flGaleria : 0,
    }).subscribe({
      next: ev => {
        this.progreso.set(ev.progreso);
        if (ev.completado && ev.archivo) {
          this.archivo.set(ev.archivo);
          this.previewLocal.set('');
          this.estado.set('completo');
          this.cargado.emit(ev.archivo);
        }
      },
      error: err => this.handleError(err, 'Error al subir')
    });
  }

  // ===== Reemplazar existente =====

  private async reemplazarArchivo(file: File) {
    const cfg = this.config();
    const archivoActual = this.archivo();
    if (!archivoActual) return;

    const ok = await this.confirmService.confirm(
      'Reemplazar Archivo?',
      'Ya existe un archivo. ¿Reemplazarlo?',
      'Sí, reemplazar'
    );

    if (!ok) {
      this.previewLocal.set('');
      return;
    }


    this.estado.set('reemplazando');
    this.progreso.set(0);

    this.archivoService.reemplazar(archivoActual.coArchivo, file, {
      flEmpresaNotKey: cfg.flEmpresaNotKey,
      coEmpresa: cfg.coEmpresa,
      coTipoEntidad: cfg.coTipoEntidad,
      coEntidad: cfg.coEntidad,
      coTipo: cfg.coTipo,
      flGaleria: 0
    }).subscribe({
      next: ev => {
        this.progreso.set(ev.progreso);
        if (ev.completado && ev.archivo) {
          this.archivo.set(ev.archivo);
          this.previewLocal.set('');
          this.estado.set('completo');
          this.reemplazado.emit(ev.archivo);
        }
      },
      error: err => this.handleError(err, 'Error al reemplazar')
    });
  }

  // ===== Eliminar =====

  async eliminar() {
    const arch = this.archivo();
    if (!arch) return;
    if (this.config().readonly) return;

    const ok = await this.confirmService.confirm(
      '¿Eliminar Archivo?',
      '¿Eliminar este archivo?',
      'Sí, eliminar'
    );

    if (!ok) return;


    const cfg = this.config();
    this.archivoService.delete({
      flEmpresaNotKey: cfg.flEmpresaNotKey,
      coEmpresa: cfg.coEmpresa,
      coArchivo: arch.coArchivo
    }).subscribe({
      next: () => {
        this.archivo.set(null);
        this.previewLocal.set('');
        this.estado.set('vacio');
        this.eliminado.emit(arch.coArchivo);
      },
      error: err => this.handleError(err, 'Error al eliminar')
    });
  }

  // ===== Helpers =====

  private setError(mensaje: string) {
    this.mensaje.set(mensaje);
    this.estado.set('error');
    this.previewLocal.set('');
    this.errorUpload.emit(mensaje);
  }

  private handleError(err: any, defaultMsg: string) {
    const msg = err?.error?.message ?? err?.message ?? defaultMsg;
    this.setError(msg);
  }

  formatearBytes(bytes: number): string {
    if (bytes < 1024) return `${bytes} B`;
    if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
    return `${(bytes / 1024 / 1024).toFixed(1)} MB`;
  }


  storageUrl(ruta: string | null | undefined): string {
    if (!ruta) return '';
    if (ruta.startsWith('http')) return ruta;
    const limpia = ruta.replace(/\\/g, '/').replace(/^\//, '');
    return `${environment.uploadsUrl}/${limpia}`;
  }


  // ===== Drag & Drop =====

  onDragOver(e: DragEvent) {
    e.preventDefault();
    e.stopPropagation();
    if (!this.config().readonly && !this.procesando()) {
      this.dragOver.set(true);
    }
  }

  onDragLeave(e: DragEvent) {
    e.preventDefault();
    e.stopPropagation();
    this.dragOver.set(false);
  }

  async onDrop(e: DragEvent) {
    e.preventDefault();
    e.stopPropagation();
    this.dragOver.set(false);

    if (this.config().readonly || this.procesando()) return;

    const file = e.dataTransfer?.files?.[0];
    if (!file) return;

    // Validar tamaño
    const maxSize = this.config().maxSizeMB * 1024 * 1024;
    if (file.size > maxSize) {
      this.setError(`El archivo excede ${this.config().maxSizeMB} MB`);
      return;
    }

    // Validar imagen (si aplica)
    if (this.imgService.esImagen(file)) {
      const validacion = await this.imgService.validar(file);
      if (!validacion.valido) {
        this.setError(validacion.errores.join('. '));
        return;
      }

      // Generar preview local mientras se sube
      this.previewLocal.set(await this.imgService.generarPreview(file));
    }

    this.mensaje.set('');

    // Decidir si subir o reemplazar
    if (this.tieneArchivo()) {
      this.reemplazarArchivo(file);
    } else {
      this.subirArchivo(file);
    }
  }


}
