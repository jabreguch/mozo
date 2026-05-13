import { Directive, Input, Output, EventEmitter, inject, signal, computed } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DestroyRef, effect } from '@angular/core';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';

/**
 * Base para todos los formularios modales
 * NO usa OnInit - reacciona a cambios de @Input
 */
@Directive()
export abstract class FormModalBase<T = any> {
  protected fb = inject(FormBuilder);
  protected toastr = inject(ToastrService);
  protected destroyRef = inject(DestroyRef);

  @Input() set data(payload: ModalPayload<T> | null) {
    this.payload$.set(payload);
  }

  @Output() saved = new EventEmitter<T | null>();

  protected payload$ = signal<ModalPayload<T> | null>(null);
  protected isLoading = signal(false);

  // ✅ Computed para facilitar acceso
  protected model = computed(() => this.payload$()?.model ?? null);
  protected relations = computed(() => this.payload$()?.relations ?? {});
  protected metadata = computed(() => this.payload$()?.metaData ?? {});

  protected action = computed<'insert' | 'update' | string>(() => {
    const meta = this.metadata();
    return (meta?.action as string) || (this.model() === null ? 'insert' : 'update');
  });

  // ✅ Subclases deben definir el formulario
  abstract form: FormGroup;

  constructor() {
    // ✅ Usar effect() para reaccionar a cambios de datos
    effect(() => {
      const payload = this.payload$();
      if (payload) {
        this.onDataChanged(payload);
      }
    });
  }

  /**
   * Se ejecuta cada vez que los datos del modal cambian
   * Subclases pueden override para cargar datos específicos
   */
  protected onDataChanged(payload: ModalPayload<T>): void {
    // Implementar en subclases si necesitan
  }

  /**
   * Obtener una relación específica
   */
  protected getRelation<R = any>(key: string): R | null {
    return this.relations()[key] ?? null;
  }

  /**
   * Obtener todas las relaciones
   */
  protected getAllRelations(): Record<string, any> {
    return this.relations();
  }

  /**
   * Validar formulario y guardar
   */
  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.onSave();
  }

  /**
   * Método abstracto - cada subclase implementa su guardado
   */
  protected abstract onSave(): void;

  /**
   * Lógica común de éxito
   */
  protected handleSuccess(
    message: string = 'Se grabaron los cambios',
    model?: T
  ): void {
    this.isLoading.set(false);
    this.toastr.success(message, 'Éxito');

    const dataToEmit = model || this.model();
    if (dataToEmit !== null) {
      this.saved.emit(dataToEmit);
    }
  }

  /**
   * Lógica común de error
   */
  protected handleError(error: any, message: string = 'Error al guardar'): void {
    this.isLoading.set(false);
    this.toastr.error(message, 'Error');
    console.error(error);
  }

  /**
   * Reset del formulario
   */
  protected resetForm(): void {
    this.form.reset();
  }

  /**
    * ✅ Obtener valores del formulario (sin controles deshabilitados)
    */
  protected getFormValue(): any {
    return this.form.getRawValue();
  }

  /**
  * ✅ Obtener solo valores válidos del formulario
  */
  protected getFormValueIfValid(): T | null {
    return this.form.valid ? (this.form.value as T) : null;
  }

  /**
   * Marcar todos los campos como touched
   */
  protected markAllAsTouched(): void {
    this.form.markAllAsTouched();
  }

  /**
  * ✅ Limpiar form (reset + mark pristine)
  */
  protected clearForm(): void {
    this.form.reset();
    this.form.markAsPristine();
    this.form.markAsUntouched();
  }

  /**
   * ✅ Deshabilitar form
   */
  protected disableForm(): void {
    this.form.disable();
  }

  /**
 * ✅ Habilitar form
 */
  protected enableForm(): void {
    this.form.enable();
  }

  /**
 * ✅ Obtener estado del form
 */
  protected getFormStatus(): {
    valid: boolean;
    pristine: boolean;
    touched: boolean;
    dirty: boolean;
  } {
    return {
      valid: this.form.valid,
      pristine: this.form.pristine,
      touched: this.form.touched,
      dirty: this.form.dirty
    };
  }

}
