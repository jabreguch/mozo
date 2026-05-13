import { Component, DestroyRef, inject, Input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // ← FormsModule para ngModel
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { RedSocialService } from '@moduleMaestro/services/red-social.service';
import { RedSocialModel } from '@app/shared/models/maestro/red-social.model';
import { EmpresaModel } from '@app/shared/models/seguridad/empresa.model';
import { PersonaModel } from '@app/shared/models/maestro/persona.model';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { TipoGeneralService } from '@moduleMaestro/services/tipo-general.service';
import { TIPO_MAESTRO } from '@app/core/global/tipo.constants';

// Tipo interno para manejar filas nuevas sin coRedSocial
type RedSocialRow = RedSocialModel & { _tempId?: number };

type Seccion = { coTipo: number; noTipo: string };

@Component({
  selector: 'mz-red-social-form-page',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonControl],
  templateUrl: './red-social-form.page.html'
})
export class RedSocialFormPage {

  private readonly redSocialService = inject(RedSocialService);
  private readonly tipoGeneralService = inject(TipoGeneralService);
  private readonly confirmService = inject(ConfirmService);
  private readonly toastr = inject(ToastrService);
  protected destroyRef = inject(DestroyRef);
  protected readonly TIPO_MAESTRO = TIPO_MAESTRO;
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;
  readonly empresa = signal<EmpresaModel | null>(null);
  readonly persona = signal<PersonaModel | null>(null);
  readonly items = signal<RedSocialRow[]>([]); // ← lista unificada
  readonly editandoId = signal<number | null>(null); // coRedSocial o _tempId
  //readonly etiquetas = signal<any[]>([]);
  //readonly tiposUrl = signal<any[]>([]);

  etiquetas = toSignal(this.tipoGeneralService.selAllActive({ coGrupo: TIPO_MAESTRO.general.etiquetaRedSocial.grupo }), { initialValue: [] });
  tiposUrl = toSignal(this.tipoGeneralService.selAllActive({ coGrupo: TIPO_MAESTRO.general.urlRedSocial.grupo }), { initialValue: [] });


  private tempIdCounter = -1; // IDs negativos para filas nuevas

  readonly secciones: Seccion[] = [
    { coTipo: TIPO_MAESTRO.general.redSocial.items.telefonoMovil, noTipo: 'Teléfono móvil' },
    { coTipo: TIPO_MAESTRO.general.redSocial.items.telefonoFijo, noTipo: 'Teléfono fijo' },
    { coTipo: TIPO_MAESTRO.general.redSocial.items.correoElectronico, noTipo: 'Correo electrónico' },
    { coTipo: TIPO_MAESTRO.general.redSocial.items.redSocial, noTipo: 'Redes sociales' },
  ];

  @Input() set data(entidad: any) {
    this.empresa.set(null);
    this.persona.set(null);
    this.items.set([]);
    this.editandoId.set(null);
    if (!entidad) return;
    if (this.isPersona(entidad)) this.persona.set(entidad);
    else if (this.isEmpresa(entidad)) this.empresa.set(entidad);
    this.selAll();
  }

  // ← filtrar items por tipo para el template
  getItemsPorTipo(coTipo: number): RedSocialRow[] {
    return this.items().filter(x => x.coTipoRedSocial === coTipo);
  }

  private selAll(): void {
    const redSocial: RedSocialModel = {};
    if (this.empresa()) {
      redSocial.flPersona = 0;
      redSocial.coEmpresa = this.empresa()!.coEmpresa;
      redSocial.flEmpresaNotKey = 1;
    } else if (this.persona()) {
      redSocial.flPersona = 1;
      redSocial.coPersona = this.persona()!.coPersona;
      redSocial.coEmpresa = this.persona()!.coEmpresa;
      redSocial.flEmpresaNotKey = 0;
    }

    this.redSocialService.selAll(redSocial)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(r => this.items.set(r));
  }

  agregarFila(coTipo: number): void {
    // cancelar edición previa si existe
    if (this.editandoId() !== null) this.cancelarEdicionActual();

    const tempId = this.tempIdCounter--;
    const nueva: RedSocialRow = {
      _tempId: tempId,
      coTipoRedSocial: coTipo,
      coEmpresa: this.empresa()?.coEmpresa,
      coPersona: this.persona()?.coPersona,
      flPersona: this.persona() ? 1 : 0,
      noRedSocial: '',
      flWhatsapp: 0,
    };

    this.items.update(list => [...list, nueva]);
    this.editandoId.set(tempId); // ← abrir en edición inmediatamente
  }

  editarFila(item: RedSocialRow): void {
    if (this.editandoId() !== null) this.cancelarEdicionActual();
    this.editandoId.set(item.coRedSocial ?? item._tempId!);
  }

  guardarFila(item: RedSocialRow): void {
    if (!item.noRedSocial?.trim()) {
      this.toastr.warning('El valor es obligatorio', 'Atención');
      return;
    }

    const esNuevo = !item.coRedSocial;
    const request$ = esNuevo
      ? this.redSocialService.insert(item)
      : this.redSocialService.update(item);

    request$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (id) => {
          if (esNuevo) {
            // ← asignar coRedSocial real devuelto por el backend
            this.items.update(list =>
              list.map(x => x._tempId === item._tempId
                ? { ...x, coRedSocial: id, _tempId: undefined }
                : x
              )
            );
          }
          this.editandoId.set(null);
          this.toastr.success('Guardado correctamente', 'Éxito');
        },
        error: () => this.toastr.error('Error al guardar', 'Error')
      });
  }

  cancelarEdicion(item: RedSocialRow): void {
    const esNuevo = !item.coRedSocial;
    if (esNuevo) {
      // ← eliminar fila temporal si cancela
      this.items.update(list => list.filter(x => x._tempId !== item._tempId));
    }
    this.editandoId.set(null);
  }

  async eliminarFila(item: RedSocialRow): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar registro?', '', 'Sí, eliminar'
    );
    if (!ok) return;

    if (!item.coRedSocial) {
      this.items.update(list => list.filter(x => x._tempId !== item._tempId));
      return;
    }

    this.redSocialService.deleteById(item)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.items.update(list =>
            list.filter(x => x.coRedSocial !== item.coRedSocial)
          );
          this.toastr.success('Eliminado correctamente', 'Éxito');
        },
        error: () => this.toastr.error('Error al eliminar', 'Error')
      });
  }

  private cancelarEdicionActual(): void {
    // eliminar filas temporales huérfanas
    this.items.update(list => list.filter(x => x.coRedSocial != null));
    this.editandoId.set(null);
  }

  private isEmpresa(e: any): e is EmpresaModel {
    return e?.coEmpresa != null && e?.coPersona == null;
  }

  private isPersona(e: any): e is PersonaModel {
    return e?.coEmpresa != null && e?.coPersona != null;
  }
}
