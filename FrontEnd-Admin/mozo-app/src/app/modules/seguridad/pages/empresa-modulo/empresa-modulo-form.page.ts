import { Component, DestroyRef, inject, Input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { EmpresaModuloService } from '../../services/empresa-modulo.service';
import { EmpresaModuloModel } from '@app/shared/models/seguridad/empresa-modulo.model';
import { EmpresaModel } from '@app/shared/models/seguridad/empresa.model';
import { ButtonControl } from "@app/shared/components/button/button.control";
import { ToastrService } from 'ngx-toastr';


type ModuloFormValue = {
  coEmpresaModulo?: number;
  coModulo: number;
  flEstReg: boolean;
  isChanged: boolean;
};


@Component({
  selector: 'mz-empresa-modulo-form-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonControl],
  templateUrl: './empresa-modulo-form.page.html'
})
export class EmpresaModuloFormPage {

  private readonly toastr = inject(ToastrService);
  private readonly empresaModuloService = inject(EmpresaModuloService);
  protected destroyRef = inject(DestroyRef);
  protected fb = inject(FormBuilder);
  protected readonly MenuControlTypeEnum = MenuControlTypeEnum;

  readonly empresa = signal<EmpresaModel>(null!);
  readonly empresaModulos = signal<EmpresaModuloModel[]>([]);

  form = this.fb.group({
    modulos: this.fb.array<FormGroup>([])
  });

  get modulosArray(): FormArray {
    return this.form.get('modulos') as FormArray;
  }

  @Input() set data(empresa: EmpresaModel | null) {
    if (!empresa) return;
    this.empresa.set(empresa);
    this.selAll();
  }



  private selAll() {
    this.modulosArray.clear();
    this.empresaModulos.set([]);
    let empresaModulo: EmpresaModuloModel = { coEmpresa: this.empresa().coEmpresa }
    this.empresaModuloService
      .selSelectAndUnSelectAll(empresaModulo)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((r: EmpresaModuloModel[]) => {
        this.empresaModulos.set(r);
        this.buildForm(r);
      });
  }



  onSave(): void {
    const raw = this.form.getRawValue() as { modulos: ModuloFormValue[] };
    const changedIndexes: number[] = [];

    const observables: Observable<any>[] = raw.modulos
      .filter((modulo, index) => {
        if (modulo.isChanged) {
          changedIndexes.push(index);
          return true;
        }
        return false;
      })
      .map(modulo => {
        const empresaModulo: EmpresaModuloModel = {
          coEmpresa: this.empresa().coEmpresa,
          coEmpresaModulo: modulo.coEmpresaModulo,
          coModulo: modulo.coModulo,
          flEstReg: modulo.flEstReg ? 1 : 0
        };

        // null = no existe en BD → insert, number = ya existe → update
        return empresaModulo.coEmpresaModulo == null
          ? this.empresaModuloService.insert(empresaModulo)
          : this.empresaModuloService.deleteById(empresaModulo);
      });

    if (observables.length === 0) {
      this.toastr.info('No se encontrarón cambios en Módulos para las Empresa', 'Información');
      return;
    }

    // Fire all requests in parallel and wait for all to complete
    forkJoin([...observables])
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          changedIndexes.forEach(index => {
            this.modulosArray.at(index).get('isChanged')!
              .setValue(false, { emitEvent: false });
          });
          this.toastr.success('Se guardaron ' + observables.length + ' cambios de los Módulos para las Empresa', 'Éxito');
        }
      });
  }



  private buildForm(modulos: EmpresaModuloModel[]): void {
    // Clear previous controls before repopulating
    this.modulosArray.clear();

    modulos.forEach(modulo => {
      const group = this.fb.group({
        coEmpresaModulo: [modulo.coEmpresaModulo ?? null],
        coModulo: [modulo.coModulo],
        flEstReg: [modulo.flEstReg ?? false],
        isChanged: [false]  // tracks whether this row was touched
      });

      // Mark isChanged when flEstReg checkbox is toggled
      group.get('flEstReg')!.valueChanges
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(() => group.get('isChanged')!.setValue(true, { emitEvent: false }));

      this.modulosArray.push(group);
    });
  }










}
