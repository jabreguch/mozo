import { Component, DestroyRef, effect, inject, signal } from '@angular/core';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ModuloService } from '@moduleSeguridad/services/modulo.service';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { MSG_FIELD_FORM } from '@app/core/global/constants';
import { ModalControl } from "@app/shared/components/modal/modal.control";
import { PerfilFormPage } from "./perfil-form.page";
import { StateControl } from "@app/shared/components/state/state.control";
import { MenuControl } from "@app/shared/components/menu/menu.control";
import { PerfilService } from '@moduleSeguridad/services/perfil.service';
import { ModalService } from '@app/shared/services/modal.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { PerfilModel } from '@app/shared/models/seguridad/perfil.model';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { FormsModule } from '@angular/forms';
import { DefaultControl } from "@app/shared/components/state/default.control";
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { PerfilPaginaFormPage } from "../perfil-pagina/perfil-pagina-form.page";

@Component({
  selector: 'mz-perfil-list-page',
  standalone: true,
  imports: [FormsModule, ButtonControl, FormFieldControl, ModalControl, PerfilFormPage, StateControl, MenuControl, DefaultControl, PerfilPaginaFormPage],
  templateUrl: './perfil-list.page.html',
  styleUrl: './perfil-list.page.css',
})
export class PerfilListPage {
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;
  private readonly moduloService = inject(ModuloService);
  protected readonly MSG_FIELD_FORM = MSG_FIELD_FORM;

  modulos = toSignal(this.moduloService.selAllActive(), { initialValue: [] });

  private readonly perfilService = inject(PerfilService);
  private readonly modalService = inject(ModalService);
  private readonly toastr = inject(ToastrService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly confirmService = inject(ConfirmService);

  readonly perfiles = signal<PerfilModel[]>([]);
  readonly selectedModulo = signal<number | null>(null);

  constructor() {
    effect(() => {
      this.selAll();
    });
  }

  onModuloChange(coModulo: number): void {
    this.selectedModulo.set(coModulo);
  }

  readonly menuItems: MenuControlModel[] = [
    {
      type: MenuControlTypeEnum.Edit,
      action: 'edit'
    },
    {
      type: MenuControlTypeEnum.Delete,
      action: 'deleteById'
    },
    {
      type: MenuControlTypeEnum.State,
      action: 'updateState'
    },
    {
      type: MenuControlTypeEnum.ToggleOn,
      action: 'updatePerfilPagina',
      label: 'Asignar Menus y sub Menus'
    }
  ];



  onMenuAction(action: string, c: PerfilModel | null): void {

    const actions: Record<string, () => void> = {
      insert: () => this.openModal(null),
      edit: () => this.openModal(c),
      updateState: () => this.updateState(c!),
      deleteById: () => this.deleteById(c!),
      updatePerfilPagina: ()=> this.openModalPerfilPagina(c!)
    };

    actions[action]?.();
  }

 private async openModalPerfilPagina(c: PerfilModel): Promise<void> {

    const modulo: ModuloModel = this.modulos().find(m => m.coModulo == this.selectedModulo())!;

    const payload: ModalPayload<PerfilModel> = {
      model: c,  // null para crear, perfil para editar
      relations: {
        modulo: modulo
      }
    };

    this.modalService.open<PerfilModel>({
      modalName: 'perfil-pagina-form-page-modal',
      title: 'Actualizar Menus y sub Menus',
      size: 'lg',
      data: payload
    });

   // setTimeout(() => {
     // const input = document.querySelector<HTMLInputElement>('input[formControlName="noPerfil"]');
      //input?.focus();
    //}, 200);
  }

  private async openModal(c: PerfilModel | null): Promise<void> {
    if (this.selectedModulo() == null) {
      const ok = await this.confirmService.error(
        'Crear perfil',
        `Falta seleccionar el Módulo`
      );
      return;
    }

    const modulo: ModuloModel = this.modulos().find(m => m.coModulo == this.selectedModulo())!;

    const payload: ModalPayload<PerfilModel> = {
      model: c,  // null para crear, perfil para editar
      relations: {
        modulo: modulo
      },
      metaData: {
        action: c ? 'update' : 'insert'
      }
    };

    this.modalService.open<PerfilModel>({
      modalName: 'perfil-form-page-modal',
      title: (c ? 'Editar' : 'Nuevo') + ' Perfil',
      size: 'sm',
      data: payload
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="noPerfil"]');
      input?.focus();
    }, 200);
  }

  private updateState(c: PerfilModel): void {

    const flEstReg = c.flEstReg === 1 ? 0 : 1;

    this.perfilService
      .updateState({ ...c, flEstReg: flEstReg })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.perfiles.update(items =>
            items.map(x =>
              x.coPerfil === c.coPerfil ? { ...x, flEstReg: flEstReg } : x
            )
          );
          this.toastr.success('Perfil actualizado', 'Éxito');
          //this.selAll();
        }
      });
  }


  private selAll(): void {
    const coModulo = this.selectedModulo();
    if (coModulo !== null) {
      const c: PerfilModel = { coModulo: coModulo } as PerfilModel;
      this.perfilService
        .selAll(c)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(response => this.perfiles.set(response));
    } else {
      this.perfiles.set([]);
    }
  }


  private async deleteById(c: PerfilModel): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar perfil?',
      `Se eliminará ${c.noPerfil}`,
      'Sí, eliminar'
    );

    if (!ok) return;

    this.perfilService
      .deleteById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.perfiles.update(items =>
            items.filter(x => x.coPerfil !== c.coPerfil)
          );
          this.toastr.success('Perfil eliminado', 'Éxito');
        }
      });
  }

  onSaved(): void {
    this.selAll();
  }

}
