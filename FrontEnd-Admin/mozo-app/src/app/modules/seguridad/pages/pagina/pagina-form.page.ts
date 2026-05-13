import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ModalService } from '@app/shared/services/modal.service';
import { buildForm, getError, V } from '@app/core/global/form.helper';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ButtonControl } from "@app/shared/components/button/button.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { FormModalBase } from '@app/shared/components/form/form-modal-base';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { PaginaModel } from '@app/shared/models/seguridad/pagina.model';
import { PaginaService } from '../../services/pagina.service';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { MenuModel } from '@app/shared/models/seguridad/menu.model';

@Component({
  selector: 'mz-pagina-form-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonControl, FormFieldControl],
  templateUrl: './pagina-form.page.html'
})
export class PaginaFormPage extends FormModalBase<PaginaModel> {
  private readonly paginaService = inject(PaginaService);
  public readonly modalService = inject(ModalService);
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;

  readonly modulo = signal<ModuloModel | null>(null);
  readonly menu = signal<MenuModel | null>(null);

  override set data(payload: ModalPayload<PaginaModel> | null) {
    this.clearForm();
    super.data = payload;
    if (!payload) return;
    this.modulo.set(this.getRelation<ModuloModel>('modulo')!);
    this.menu.set(this.getRelation<MenuModel>('menu')!);
    const model = payload.model;
    if (this.action() === 'insert') {
    } else if (this.action() === 'update') {
      this.selById(model!);
    }
  }

  form = buildForm(this.fb, [
    { name: 'nuOrden', value: 0, validators: [...V.required, ...V.integer] },
    { name: 'noOpcion', validators: [...V.required, ...V.maxLength(50)] },
    { name: 'noControlador', validators: [...V.required, ...V.maxLength(50)] },
    { name: 'noAccion', validators: [...V.required, ...V.maxLength(50)] },
    { name: 'txDescripcion', validators: [...V.maxLength(150)] },
    //{ name: 'coArea', validators: [...V.required] },
  ]);

  private selById(c: PaginaModel): void {
    console.log(c);
    this.paginaService.selById(c)
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

    request.coModulo = this.modulo()?.coModulo;
    request.coArea = this.menu()?.coModulo;
    request.coMenu = this.menu()?.coMenu;

    const observable$ = this.action() === 'insert'
      ? this.paginaService.insert(request)
      : this.paginaService.update(request);

    observable$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          if (this.action() === 'insert') {
            request.coModulo = response;
          }
          this.handleSuccess('Página guardado exitosamente', request);
          this.modalService.close();
        }
      });
  }

  private buildRequest(raw: any): PaginaModel {
    return {
      coPagina: this.model()?.coPagina,
      nuOrden: raw.nuOrden,
      noOpcion: raw.noOpcion,
      noControlador: raw.noControlador,
      noAccion: raw.noAccion,
      coTipoPagina: 1
    };
  }

  getError(field: string): string | null {
    return getError(this.form, field);
  }


}
