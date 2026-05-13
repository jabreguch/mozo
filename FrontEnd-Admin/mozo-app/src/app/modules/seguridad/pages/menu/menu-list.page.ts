import { Component, DestroyRef, effect, inject, signal } from '@angular/core';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ModuloService } from '@moduleSeguridad/services/modulo.service';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { MSG_FIELD_FORM } from '@app/core/global/constants';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { MenuControl } from "@app/shared/components/menu/menu.control";
import { StateControl } from "@app/shared/components/state/state.control";
import { MenuModel } from '@app/shared/models/seguridad/menu.model';
import { MenuService } from '../../services/menu.service';
import { ModalService } from '@app/shared/services/modal.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { FormsModule } from '@angular/forms';
import { ModalControl } from "@app/shared/components/modal/modal.control";
import { MenuFormPage } from "./menu-form.page";
import { PaginaModel } from '@app/shared/models/seguridad/pagina.model';
import { PaginaService } from '../../services/pagina.service';
import { PaginaFormPage } from "../pagina/pagina-form.page";

@Component({
  selector: 'mz-menu-list-page',
  standalone: true,
  imports: [ButtonControl, FormFieldControl, MenuControl, StateControl, FormsModule, ModalControl, MenuFormPage, PaginaFormPage],
  templateUrl: './menu-list.page.html',
  styleUrl: './menu-list.page.css',
})
export class MenuListPage {
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;
  private readonly moduloService = inject(ModuloService);
  private readonly menuService = inject(MenuService);
  private readonly paginaService = inject(PaginaService);
  protected readonly MSG_FIELD_FORM = MSG_FIELD_FORM;


  private readonly modalService = inject(ModalService);
  private readonly toastr = inject(ToastrService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly confirmService = inject(ConfirmService);

  modulos = toSignal(this.moduloService.selAllActive(), { initialValue: [] });

  readonly menus = signal<MenuModel[]>([]);
  readonly selectedModulo = signal<number | null>(null);

  constructor() {
    effect(() => {
      this.selArbol();
    });
  }

  onModuloChange(coModulo: number): void {
    this.selectedModulo.set(coModulo);
  }

  onMenuAction(action: string, c: MenuModel | null): void {

    const actions: Record<string, () => void> = {
      insert: () => this.openModalMenu(null),
      edit: () => this.openModalMenu(c),
      updateState: () => this.updateState(c!),
      deleteById: () => this.deleteById(c!),
      newPagina:()=> this.openModalPagina(c!, null)
    };

    actions[action]?.();
  }

  onMenuPaginaAction(action: string, menu: MenuModel, pagina: PaginaModel | null): void {
    const actions: Record<string, () => void> = {
      //insert: () => this.openModalPagina(menu, null),
      edit: () => this.openModalPagina(menu, pagina),
      updateState: () => this.updateStatePagina(pagina!),
      deleteById: () => this.deletePaginaById(pagina!)
    };

    actions[action]?.();
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
      type: MenuControlTypeEnum.New,
      action: 'newPagina',
      label: 'Agregar sub Menú'
    }
  ];

  readonly menuPaginaItems: MenuControlModel[] = [
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
    }
  ];

  private async openModalMenu(c: MenuModel | null): Promise<void> {
    if (this.selectedModulo() == null) {
      const ok = await this.confirmService.error(
        'Crear Menú',
        `Falta seleccionar el Módulo`
      );
      return;
    }

    const modulo: ModuloModel = this.modulos().find(m => m.coModulo == this.selectedModulo())!;

    const payload: ModalPayload<MenuModel> = {
      model: c,
      relations: {
        modulo: modulo
      },
      metaData: {
        action: c ? 'update' : 'insert'
      }
    };

    this.modalService.open<MenuModel>({
      modalName: 'menu-form-page-modal',
      title: (c ? 'Editar' : 'Nuevo') + ' Menú',
      size: 'sm',
      data: payload
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="nuOrden"]');
      input?.focus();
    }, 200);

  }


  private async openModalPagina(menu: MenuModel, pagina: PaginaModel | null): Promise<void> {
    if (this.selectedModulo() == null) {
      const ok = await this.confirmService.error(
        'Crear sub Menú',
        `Falta seleccionar el Módulo`
      );
      return;
    }

    const modulo: ModuloModel = this.modulos().find(m => m.coModulo == this.selectedModulo())!;

    const payload: ModalPayload<PaginaModel> = {
      model: pagina,
      relations: {
        modulo: modulo,
        menu: menu
      },
      metaData: {
        action: pagina ? 'update' : 'insert'
      }
    };

    this.modalService.open<PaginaModel>({
      modalName: 'pagina-form-page-modal',
      title: (pagina ? 'Editar' : 'Nuevo') + ' sub Menú',
      size: 'lg',
      data: payload
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="nuOrden"]');
      input?.focus();
    }, 200);

  }


  private updateState(c: MenuModel): void {

    const flEstReg = c.flEstReg === 1 ? 0 : 1;

    this.menuService
      .updateState({ ...c, flEstReg: flEstReg })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.menus.update(items =>
            items.map(x =>
              x.coMenu === c.coMenu ? { ...x, flEstReg: flEstReg } : x
            )
          );
          this.toastr.success('Menú actualizado', 'Éxito');
          //this.selAll();
        }
      });
  }

  private updateStatePagina(c: PaginaModel): void {

    const flEstReg = c.flEstReg === 1 ? 0 : 1;
    // const modulo: ModuloModel = this.modulos().find(m => m.coModulo == this.selectedModulo())!;
    this.paginaService
      .updateState({ ...c, flEstReg: flEstReg })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.menus.update(menus =>
            menus.map(menu =>
              menu.coMenu === c.coMenu
                ? {
                  ...menu,
                  paginaLst: (menu.paginaLst ?? []).map(p =>
                    p.coPagina === c.coPagina
                      ? { ...p, flEstReg }
                      : p
                  )
                }
                : menu
            )
          );
          this.toastr.success('sub Menú actualizado', 'Éxito');
          //this.selAll();
        }
      });
  }



  private selArbol(): void {
    const coModulo = this.selectedModulo();
    if (coModulo !== null) {
      const c: MenuModel = { coModulo: coModulo } as MenuModel;
      this.menuService
        .selArbol(c)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(response => this.menus.set(response));
    } else {
      this.menus.set([]);
    }
  }


  private async deleteById(c: MenuModel): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar perfil?',
      'Se eliminará el registro',
      'Sí, eliminar'
    );

    if (!ok) return;

    this.menuService
      .deleteById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.menus.update(items =>
            items.filter(x => x.coMenu !== c.coMenu)
          );
          this.toastr.success('Perfil eliminado', 'Éxito');
        }
      });
  }


  private async deletePaginaById(c: PaginaModel): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar perfil?',
      'Se eliminará el registro',
      'Sí, eliminar'
    );

    if (!ok) return;

    this.paginaService
      .deleteById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.menus.update(menus =>
            menus.map(menu =>
              menu.coMenu === c.coMenu
                ? {
                  ...menu,
                  paginaLst: (menu.paginaLst ?? []).filter(y => y.coPagina !== c.coPagina)
                } : menu
            )
          );

          this.toastr.success('sub Menu eliminado', 'Éxito');
        }
      });
  }

  onSaved(): void {
    this.selArbol();
  }


}
