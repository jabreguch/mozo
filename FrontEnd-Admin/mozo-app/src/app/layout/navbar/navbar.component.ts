import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from "@angular/router";
import { AsyncPipe } from '@angular/common';
import { ModalService } from '@sharedServices/modal.service';
import { AuthService } from '@app/core/services/auth.service';
import { NgIcon } from "@ng-icons/core";
import { LoginPage } from "@app/features/auth/pages/login/login.page";
import { ModalControl } from "@app/shared/components/modal/modal.control";
import { PermisoModel } from '@app/shared/models/seguridad/permiso.model';

@Component({
  selector: '[app-navbar-component]',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
  imports: [RouterOutlet, AsyncPipe, NgIcon, LoginPage, ModalControl]
})
export class NavbarComponent {

  private auth = inject(AuthService);
  private modalService = inject(ModalService);
  private router = inject(Router);

  user$ = this.auth.user$;

  openLogin() {
    const login: PermisoModel = {
      coPermiso: 0,
      coPersona: 0,
      noUsuario: '',
      noClave: ''
    };

    this.modalService.open<PermisoModel>({
      modalName: 'login-page',
      title: 'Login',
      size: 'sm',
      data: {
        model: login!
      }
    });

    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>('input[formControlName="noUsuario"]');
      input?.focus();
    }, 200);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/logout'], { replaceUrl: true }); // reemplaza el historial
  }

}
