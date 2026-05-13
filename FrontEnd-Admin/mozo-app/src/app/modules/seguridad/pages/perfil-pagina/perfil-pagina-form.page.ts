import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ModalService } from '@app/shared/services/modal.service';
import { FormFieldControl } from "@app/shared/components/form-field/form-field.control";
import { ButtonControl } from "@app/shared/components/button/button.control";
import { FormModalBase } from '@app/shared/components/form/form-modal-base';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { MenuModel } from '@app/shared/models/seguridad/menu.model';
import { PerfilPaginaService } from '../../services/perfil-pagina.service';
import { PerfilModel } from '@app/shared/models/seguridad/perfil.model';
import { PerfilPaginaModel } from '@app/shared/models/seguridad/perfil-pagina.model';
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';


type PaginaFormValue = {
  coPagina: number;
  flEstReg: boolean;
};

type MenuFormValue = {
  coMenu: number;
  flEstReg: boolean;
  paginas: PaginaFormValue[];
};

@Component({
  selector: 'mz-perfil-pagina-form-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonControl],
  templateUrl: './perfil-pagina-form.page.html'
})
export class PerfilPaginaFormPage extends FormModalBase<PerfilModel> {
  private readonly perfilPaginaService = inject(PerfilPaginaService);
  public readonly modalService = inject(ModalService);
  readonly menus = signal<MenuModel[]>([]);
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;
  override set data(payload: ModalPayload<PerfilModel> | null) {
    this.clearForm();
    super.data = payload;
    if (!payload) return;
    const model = payload.model;
    this.selAll(model!);
  }

  //form = buildForm(this.fb, []);
  form = this.fb.group({
    menus: this.fb.array<FormGroup>([])
  });

  protected override onSave(): void {

    const raw = this.form.getRawValue() as { menus: MenuFormValue[] };

    const perfil: PerfilModel = this.model()!;

    const lista: PerfilPaginaModel[] = [];

    raw.menus.forEach(menu => {

      // 🔹 MENÚ
      /*
      lista.push({
        coMenu: menu.coMenu,
        coPagina: null!,
        flEstReg: menu.flEstReg ? 1 : 0
      });
      */
      // 🔹 PÁGINAS
      menu.paginas
        .filter(p => p.flEstReg)
        .forEach(p => {
          lista.push({
            coMenu: menu.coMenu,
            coPagina: p.coPagina
          });
        });

    });

    perfil.PerfilPaginaLst = lista;

    console.log('DATA A ENVIAR', perfil);

    this.perfilPaginaService.insert(perfil)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        console.log('Guardado correctamente');
      });

    this.handleSuccess('Se guardo exitosamente');
    this.modalService.close();
  }




  // =========================
  // BUILD FORM DINÁMICO
  // =========================
  // =========================
  // BUILD FORM
  // =========================
  private buildForm(menus: MenuModel[]) {

    const menuArray = this.fb.array<FormGroup>([]);

    menus.forEach(menu => {

      const paginasArray = this.fb.array<FormGroup>([]);

      (menu.paginaLst || []).forEach(pagina => {
        paginasArray.push(
          this.fb.group({
            coPagina: new FormControl(pagina.coPagina),
            flEstReg: new FormControl(!!pagina.flEstReg)
          })
        );
      });

      menuArray.push(
        this.fb.group({
          coMenu: new FormControl(menu.coMenu),
          flEstReg: new FormControl(!!menu.flEstReg),
          paginas: paginasArray
        })
      );
    });

    this.form.setControl('menus', menuArray);

    this.setupTreeBehavior(); // 🔥 clave
  }




  // =========================
  // GETTERS
  // =========================
  get menusArray(): FormArray {
    return this.form.get('menus') as FormArray;
  }

  paginasArray(i: number): FormArray {
    return this.menusArray.at(i).get('paginas') as FormArray;
  }

  private selAll(perfil: PerfilModel): void {
    this.perfilPaginaService
      .selAll(perfil)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(response => {
        this.menus.set(response);
        this.buildForm(response);
      });
  }




 private setupTreeBehavior() {

    this.menusArray.controls.forEach((menuGroup, i) => {

      const menuCtrl = menuGroup.get('flEstReg');
      const paginas = menuGroup.get('paginas') as FormArray;

      // MENU → PAGINAS
      menuCtrl?.valueChanges
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe((checked: boolean) => {
          paginas.controls.forEach(p => {
            p.get('flEstReg')?.setValue(checked, { emitEvent: false });
          });
        });

      // PAGINAS → MENU
      paginas.controls.forEach(paginaGroup => {

        paginaGroup.get('flEstReg')?.valueChanges
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(() => {

            const all = paginas.controls.every(p => p.get('flEstReg')?.value);
            const none = paginas.controls.every(p => !p.get('flEstReg')?.value);

            if (all) {
              menuCtrl?.setValue(true, { emitEvent: false });
            } else if (none) {
              menuCtrl?.setValue(false, { emitEvent: false });
            } else {
              menuCtrl?.setValue(false, { emitEvent: false });
            }

          });

      });

    });
  }

  // =========================
  // GLOBAL SELECT
  // =========================
  selectAll(checked: boolean) {
    this.menusArray.controls.forEach(menu => {
      menu.get('flEstReg')?.setValue(checked);
    });
  }

  allChecked(): boolean {
    return this.menusArray.controls.every(menu => {
      const paginas = menu.get('paginas') as FormArray;
      return paginas.controls.every(p => p.get('flEstReg')?.value);
    });
  }

  isGlobalIndeterminate(): boolean {
    let total = 0;
    let checked = 0;

    this.menusArray.controls.forEach(menu => {
      const paginas = menu.get('paginas') as FormArray;

      paginas.controls.forEach(p => {
        total++;
        if (p.get('flEstReg')?.value) checked++;
      });
    });

    return checked > 0 && checked < total;
  }

  isIndeterminate(i: number): boolean {
    const paginas = this.paginasArray(i).controls;
    const checked = paginas.filter(p => p.get('flEstReg')?.value).length;

    return checked > 0 && checked < paginas.length;
  }



}
