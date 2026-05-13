import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ModuloService } from '@moduleSeguridad/services/modulo.service';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { ModalService } from '@app/shared/services/modal.service';
import { buildForm, getError, V } from '@app/core/global/form.helper';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ButtonControl } from "@app/shared/components/button/button.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { FormModalBase } from '@app/shared/components/form/form-modal-base';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';

@Component({
  selector: 'mz-modulo-form-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonControl, FormFieldControl],
  templateUrl: './modulo-form.page.html'
})
export class ModuloFormPage extends FormModalBase<ModuloModel> {
  private readonly moduloService = inject(ModuloService);
  public readonly modalService = inject(ModalService);
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;

  override set data(payload: ModalPayload<ModuloModel> | null) {
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
    { name: 'nuOrden', value: 0, validators: [...V.required, ...V.integer] },
    { name: 'noModulo', validators: [...V.required, ...V.maxLength(30)] },
    { name: 'noModuloDescripcion', validators: [...V.maxLength(200)] },
    { name: 'noArea', validators: [...V.maxLength(20)] },
    { name: 'noIcono', validators: [...V.maxLength(60)] },
    //{ name: 'flEstReg', value: true, validators: V.required},
    { name: 'flArea', value: 1 }
  ]);

  private selById(c: ModuloModel): void {
    this.moduloService.selById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: c => {
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
      ? this.moduloService.insert(request)
      : this.moduloService.update(request);

    observable$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          if (this.action() === 'insert') {
            request.coModulo = response;
          }
          this.handleSuccess('Módulo guardado exitosamente', request);
          this.modalService.close();
        }
      });
  }

  private buildRequest(raw: any): ModuloModel {
    return {
      coModulo: this.model()?.coModulo,
      noModulo: raw.noModulo ?? '',
      noModuloDescripcion: raw.noModuloDescripcion ?? '',
      noArea: raw.noArea ?? '',
      nuOrden: raw.nuOrden ?? 0,
      flArea: raw.flArea == true ? 1 : 0,
      noIcono: raw.noIcono ?? ''
    };
  }


  getError(field: string): string | null {
    return getError(this.form, field);
  }


}
