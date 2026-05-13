import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { ModuloService } from '@moduleSeguridad/services/modulo.service';
import { ModuloModel } from '@app/shared/models/seguridad/modulo.model';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { MenuControl } from "@app/shared/components/menu/menu.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { StateControl } from "@app/shared/components/state/state.control";
import { ModalService } from '@app/shared/services/modal.service';
import { ModuloFormPage } from "./modulo-form.page";
import { ModalControl } from "@app/shared/components/modal/modal.control";
import { ToastrService } from 'ngx-toastr';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';


@Component({
  selector: 'mz-modulo-list-page',
  standalone: true,
  templateUrl: './modulo-list.page.html',
  styleUrl: './modulo-list.page.css',
  imports: [ModuloFormPage, MenuControl, ModalControl, StateControl, ButtonControl],
})

export class ModuloListPage implements OnInit {
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;

  private readonly moduloService = inject(ModuloService);
  private readonly modalService = inject(ModalService);
  private readonly toastr = inject(ToastrService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly confirmService = inject(ConfirmService);

  readonly modulos = signal<ModuloModel[]>([]);

  ngOnInit(): void {
    this.selAll();
  }


  menuItems: MenuControlModel[] = [
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


  onMenuAction(action: string, c: ModuloModel | null): void {
    const actions: Record<string, () => void> = {
      insert: () => this.openModal(null),
      edit: () => this.openModal(c),
      updateState: () => this.updateState(c!),
      deleteById: () => this.deleteById(c!)
    };

    actions[action]?.();
  }

  private openModal(c: ModuloModel | null): void {
    const payload: ModalPayload<ModuloModel> = {
      model: c,
      metaData: {
        action: c ? 'update' : 'insert'
      },
    };

    this.modalService.open<ModuloModel>({
      modalName: 'modulo-form-page',
      title: (c ? 'Editar' : 'Nuevo') + ' Módulo',
      size: 'lg',
      data: payload
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="nuOrden"]');
      input?.focus();
    }, 200);
  }

  private updateState(modulo: ModuloModel): void {

    const flEstReg = modulo.flEstReg === 1 ? 0 : 1;

    this.moduloService
      .updateState({ ...modulo, flEstReg: flEstReg })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.modulos.update(items =>
            items.map(x =>
              x.coModulo === modulo.coModulo ? { ...x, flEstReg: flEstReg } : x
            )
          );
          this.toastr.success('Módulo actualizado', 'Éxito');
          //this.selAll();
        }
      });

  }

  private selAll(): void {
    this.moduloService
      .selAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(response => this.modulos.set(response));
  }


  private async deleteById(c: ModuloModel): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar módulo?',
      `Se eliminará ${c.noModulo}`,
      'Sí, eliminar'
    );

    if (!ok) return;

    this.moduloService
      .deleteById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.modulos.update(items =>
            items.filter(x => x.coModulo !== c.coModulo)
          );
          this.toastr.success('Módulo eliminado', 'Éxito');
        }
      });
  }

  onSaved(c: ModuloModel): void {
    this.selAll();
  }

}
