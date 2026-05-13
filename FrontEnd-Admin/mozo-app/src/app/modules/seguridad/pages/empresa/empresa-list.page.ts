import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { MenuControl } from "@app/shared/components/menu/menu.control";
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { StateControl } from "@app/shared/components/state/state.control";
import { ModalService } from '@app/shared/services/modal.service';
import { ModalControl } from "@app/shared/components/modal/modal.control";
import { ToastrService } from 'ngx-toastr';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ConfirmService } from '@app/shared/services/confirm.service';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { EmpresaFormPage } from "./empresa-form.page";
import { EmpresaModel } from '@app/shared/models/seguridad/empresa.model';
import { EmpresaService } from '../../services/empresa.service';
import { PaginationControl } from "@app/shared/components/pagination/pagination.control";
import { EmpresaModuloService } from '../../services/empresa-modulo.service';
import { EmpresaModuloModel } from '@app/shared/models/seguridad/empresa-modulo.model';


@Component({
  selector: 'mz-empresa-list-page',
  standalone: true,
  templateUrl: './empresa-list.page.html',
  styleUrl: './empresa-list.page.css',
  imports: [MenuControl, ModalControl, StateControl, ButtonControl, EmpresaFormPage, PaginationControl],
})

export class EmpresaListPage implements OnInit {
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;

  private readonly empresaService = inject(EmpresaService);
  private readonly empresaModuloService = inject(EmpresaModuloService);
  private readonly modalService = inject(ModalService);
  private readonly toastr = inject(ToastrService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly confirmService = inject(ConfirmService);

  readonly empresaModulos = signal<Map<number, EmpresaModuloModel[]>>(new Map());


  pageIndex = signal(1);
  pageSize = 10;
  rowsCount = signal(0);

  get pageCount(): number {
    return Math.ceil(this.rowsCount() / this.pageSize);
  }

  goToPage(p: number) {
    this.pageIndex.set(p);
    this.selAll();
  }

  readonly empresas = signal<EmpresaModel[]>([]);

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


  onMenuAction(action: string, c: EmpresaModel | null): void {
    const actions: Record<string, () => void> = {
      insert: () => this.openModal(null),
      edit: () => this.openModal(c),
      updateState: () => this.updateState(c!),
      deleteById: () => this.deleteById(c!)
    };

    actions[action]?.();
  }

  private openModal(c: EmpresaModel | null): void {
    const payload: ModalPayload<EmpresaModel> = {
      model: c,
      metaData: {
        action: c ? 'update' : 'insert'
      },
    };

    this.modalService.open<EmpresaModel>({
      modalName: 'empresa-form-page',
      title: (c ? 'Editar' : 'Nuevo') + ' Empresa',
      size: 'lg',
      data: payload
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="noEmpresa"]');
      input?.focus();
    }, 200);
  }

  private updateState(empresa: EmpresaModel): void {

    const flEstReg = empresa.flEstReg === 1 ? 0 : 1;

    this.empresaService
      .updateState({ ...empresa, flEstReg: flEstReg })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.empresas.update(items =>
            items.map(x =>
              x.coEmpresa === empresa.coEmpresa ? { ...x, flEstReg: flEstReg } : x
            )
          );
          this.toastr.success('Empresa actualizada', 'Éxito');
          //this.selAll();
        }
      });

  }

  private selAll(): void {
    const empresa: EmpresaModel = {};
    empresa.pageIndex = this.pageIndex() - 1;
    empresa.pageSize = this.pageSize;
    this.empresaService
      .selAll(empresa)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(r => {
        this.empresas.set(r.data);
        this.rowsCount.set(r.rowsCount);
        this.selAllEmpresaModulo(r.data);
      });
  }

  private selAllEmpresaModulo(empresas: EmpresaModel[]) {
    //const subset = empresas.slice(0, 5); // opcional

    empresas.forEach(emp => {
      let empresaModulo: EmpresaModuloModel = { coEmpresa: emp.coEmpresa }
      this.empresaModuloService
        .selAll(empresaModulo)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe((r: EmpresaModuloModel[]) => {
          //r.forEach(e => {
//            e.coEmpresa = emp.coEmpresa;
  //        });

          const map = this.empresaModulos();
          map.set(emp.coEmpresa!, r);
          this.empresaModulos.set(new Map(map));
        });
    });
  }

  getModulosTexto(coEmpresa: number): string {
    const mods = this.empresaModulos().get(coEmpresa);
    if (!mods) return '...'; // cargando
    return mods
      .map(m => m.noModulo)
      .join(', ');
  }


  private async deleteById(c: EmpresaModel): Promise<void> {
    const ok = await this.confirmService.confirm(
      '¿Eliminar Empresa?',
      'Se eliminará la Empresa',
      'Sí, eliminar'
    );

    if (!ok) return;

    this.empresaService
      .deleteById(c)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.empresas.update(items =>
            items.filter(x => x.coEmpresa !== c.coEmpresa)
          );
          this.toastr.success('Empresa eliminado', 'Éxito');
        }
      });
  }

  onSaved(c: EmpresaModel): void {
    this.selAll();
  }

}
