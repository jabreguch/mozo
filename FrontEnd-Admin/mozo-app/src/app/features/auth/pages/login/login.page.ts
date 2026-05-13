import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@app/core/services/auth.service';
import { buildForm, getError, V } from '@app/core/global/form.helper';
import { ModalService } from '@app/shared/services/modal.service';
import { switchMap, tap } from 'rxjs/operators';
import { OptionService } from '@app/core/services/option.service';
import { PermisoModel } from '@app/shared/models/seguridad/permiso.model';
import { FormModalBase } from '@app/shared/components/form/form-modal-base';
import { ModalPayload } from '@app/shared/models/controls/modal-control.model';
import { TipoArchivoCatalogoService } from '@app/core/services/tipo-archivo-catalogo.service';
import { from } from 'rxjs';
import { ModuloUsuarioModel } from '@app/shared/models/seguridad/modulo-usuario.model';

@Component({
  selector: 'mz-login-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.page.html',
  styleUrl: './login.page.css',
})
export class LoginPage extends FormModalBase<PermisoModel> {
  private auth = inject(AuthService);
  private router = inject(Router);
  public modalService = inject(ModalService);
  private optionService = inject(OptionService);
  private tipoArchivoCatalogo = inject(TipoArchivoCatalogoService);


  override set data(payload: ModalPayload<PermisoModel> | null) {
    this.clearForm();
    super.data = payload;
    if (!payload) return;
    const model = payload.model;
    this.form.patchValue({ noUsuario: '' });
  }


  form = buildForm(this.fb, [
    { name: 'noUsuario', validators: V.username },
    { name: 'noClave', validators: V.password }
  ]);

  private buildRequest(raw: any): PermisoModel {
    return {
      noUsuario: raw.noUsuario ?? '',
      noClave: raw.noClave ?? ''
    };
  }


  protected override onSave(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid) return;

    const raw = this.form.getRawValue();
    const request = this.buildRequest(raw);

    this.auth.login(request)
      .pipe(
        switchMap(() => this.auth.getMenus()),
        tap(menus => this.optionService.setMenus(menus)),
        switchMap(() => from(this.tipoArchivoCatalogo.cargar()))
      )
      .subscribe({
        next: () => {  // ⬅️ Sin parámetro, no se usa
          this.modalService.close();
          this.toastr.success('Bienvenido', 'Login exitoso');
          this.form.reset();
          setTimeout(() => this.router.navigate(['/home']), 100);
        },
        error: (err) => {
          console.error('Error en login:', err);
          this.toastr.error('Error en el login', 'Login fallido');
        }
      });
  }

  getError(field: string): string | null {
    return getError(this.form, field);
  }
}
