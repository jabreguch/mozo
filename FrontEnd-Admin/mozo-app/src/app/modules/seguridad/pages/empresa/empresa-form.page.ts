import { Component, computed, inject, signal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { ModalService } from '@app/shared/services/modal.service';
import { buildForm, getError, V } from '@app/core/global/form.helper';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ButtonControl } from "@app/shared/components/button/button.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { FormModalBase } from '@app/shared/components/form/form-modal-base';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { EmpresaService } from '../../services/empresa.service';
import { EmpresaModel } from '@app/shared/models/seguridad/empresa.model';
import { DocumentoIdentidadService } from '@app/modules/maestro/services/documento-identidad.service';
import { PaisService } from '@app/modules/maestro/services/pais.service';
import { MSG_FIELD_FORM } from '@app/core/global/constants';
import { PaisModel } from '@app/shared/models/maestro/pais.model';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { EmpresaModuloFormPage } from "../empresa-modulo/empresa-modulo-form.page";
import { RedSocialFormPage } from "@app/modules/maestro/pages/red-social/red-social-form.page";
import { ArchivoService } from '@app/modules/maestro/services/archivo.service';
import { FORMATO_ARCHIVO, TIPO_MAESTRO } from '@app/core/global/tipo.constants';
import { TipoArchivoCatalogoService } from '@app/core/services/tipo-archivo-catalogo.service';
import { ArchivoModel } from '@app/shared/models/maestro/archivo.model';
import { FileSingleComponent } from "@app/shared/components/archivo-uploader/file-single.control";


@Component({
  selector: 'mz-empresa-form-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonControl, FormFieldControl, EmpresaModuloFormPage, RedSocialFormPage, FileSingleComponent],
  templateUrl: './empresa-form.page.html'
})
export class EmpresaFormPage extends FormModalBase<EmpresaModel> {
  private readonly empresaService = inject(EmpresaService);
  private catalogo = inject(TipoArchivoCatalogoService);
  private readonly documentoIdentidadService = inject(DocumentoIdentidadService);
  private readonly paisService = inject(PaisService);
  public readonly modalService = inject(ModalService);
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;
  private readonly confirmService = inject(ConfirmService);
  protected readonly MSG_FIELD_FORM = MSG_FIELD_FORM;
  protected readonly TIPO_MAESTRO = TIPO_MAESTRO;

  readonly activeTab = signal<number>(0); // ← tab activo por defecto


  documentosIndentidades = toSignal(this.documentoIdentidadService.selAllActive(), { initialValue: [] });
  paises = toSignal(this.paisService.selAllActive(), { initialValue: [] });
  readonly selectedPais = signal<PaisModel | null>(null);

  //@ViewChild('empresaModuloFormPage', { static: false }) empresaModuloFormPage!: EmpresaModuloFormPage;

  //ngAfterViewInit(): void {
  // console.log('empresaModuloFormPage:', this.empresaModuloFormPage); // ← verificar que se detecta
  //}

  onPaisChange(event: Event): void {
    const coPais = Number((event.target as HTMLSelectElement).value);

    this.selectedPais.set(
      this.paises().find(m => m.coPais === coPais) ?? null
    );
  }


  menuItems: MenuControlModel[] = [
    {
      type: MenuControlTypeEnum.Edit,
      action: 'edit'
    },
    {
      type: MenuControlTypeEnum.Delete,
      action: 'deleteById'
    }

  ];



  override set data(payload: ModalPayload<EmpresaModel> | null) {
    this.activeTab.set(1); //
    this.clearForm();
    super.data = payload;
    if (!payload) return;
    const model = payload.model;
    if (this.action() === 'insert') {
    } else if (this.action() === 'update') {
      this.selById(model!);
    }
  }

  form = buildForm(this.fb, [
    { name: 'noEmpresa', validators: [...V.required, ...V.maxLength(150)] },
    { name: 'noEmpresaCorto', validators: [...V.maxLength(80)] },
    { name: 'noMision', validators: [...V.maxLength(800)] },
    { name: 'noVision', validators: [...V.maxLength(800)] },
    { name: 'txQuienSoy', validators: [...V.maxLength(7000)] },
    { name: 'noSeo', validators: [...V.maxLength(150)] },
    { name: 'coPais', validators: [...V.integer] },
    { name: 'coDocumentoIdentidad', validators: [...V.integer] },
    { name: 'nuDocumentoFiscal', validators: [...V.maxLength(20)] },
  ]);

  private selById(c: EmpresaModel): void {
    this.empresaService.selById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: c => {
          // this.se
          this.selectedPais.set(
            this.paises().find(m => m.coPais === c.coPais) ?? null
          );
          this.form.patchValue(c);
        }
      });
  }




  protected override onSave(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid) return;
    const raw = this.form.getRawValue();
    const request = this.buildRequest(raw);

    const observable$ = this.action() === 'insert'
      ? this.empresaService.insert(request)
      : this.empresaService.update(request);

    observable$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          if (this.action() === 'insert') {
            // Asignar el ID generado
            request.coEmpresa = response;
          }

          // Recargar el registro completo para actualizar la vista
          this.selById(request);

          // Cambiar a modo edición
          if (this.data) {
            this.data = {
              ...this.data,
              model: request,
              metaData: {
                action: 'update'
              },
            };
          }
          //else {
            // En update, también recargar para reflejar cambios
            //this.selById(request);
          //}

          this.handleSuccess('Empresa guardada exitosamente', request);
          //this.modalService.close();
        }
      });
  }

  private buildRequest(raw: any): EmpresaModel {
    return {
      coEmpresa: this.model()?.coEmpresa,
      noEmpresa: raw.noEmpresa,
      noEmpresaCorto: raw.noEmpresaCorto,
      noMision: raw.noMision,
      noVision: raw.noVision,
      txQuienSoy: raw.txQuienSoy,
      noSeo: raw.noSeo,
      coPais: raw.coPais,
      coDocumentoIdentidad: raw.coDocumentoIdentidad,
      nuDocumentoFiscal: raw.nuDocumentoFiscal,
      coMoneda: raw.coMoneda,
    };
  }

  getError(field: string): string | null {
    return getError(this.form, field);
  }

  logoConfig = computed(() => {
    const cfg = this.catalogo.buildConfig(
      FORMATO_ARCHIVO.seguridad.empresa.logo,
      this.model()?.coEmpresa ?? 0,
      { maxSizeMB: 1, flEmpresaNotKey: 1, coEmpresa: this.model()?.coEmpresa }
    );

    if (!cfg || this.model() === null) return null;

    return {
      coEmpresa: cfg.coEmpresa,
      flEmpresaNotKey: cfg.flEmpresaNotKey,
      coTipoEntidad: cfg.coTipoEntidad,
      coEntidad: cfg.coEntidad,
      coTipo: cfg.coTipo,
      accept: cfg.accept,
      label: cfg.label,
      maxSizeMB: cfg.maxSizeMB
    };
  });

  onLogoCargado(archivo: ArchivoModel) {
    console.log('Logo subido:', archivo);
  }

  onLogoReemplazado(archivo: ArchivoModel) {
    console.log('Logo reemplazado:', archivo);
  }

  onLogoEliminado(coArchivo: number) {
    console.log('Logo eliminado, ID:', coArchivo);
  }

  onError(mensaje: string) {
    console.error('Error:', mensaje);
  }


}
