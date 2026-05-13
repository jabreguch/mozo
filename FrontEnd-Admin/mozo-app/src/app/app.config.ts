import { APP_INITIALIZER, ApplicationConfig, inject, provideAppInitializer, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/interceptors/auth-interceptor';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

import { provideIcons } from '@ng-icons/core';


// Importa solo los iconos que vas a usar (Lucide)
import {
  lucideHome,
  lucidePackage,
  lucideMenu,
  lucideMoreVertical,
  lucidePanelRightOpen, // Desplegar menu

  lucideShieldUser, // Módulo de Seguridad
  lucideDatabaseZap, // Módulo de Maestros
  lucideSchool, // Módulo de Empresa
  lucideBuilding2, // Módulo de Condominio
  lucideFileStack, // Módulo de Tramite documentario
  lucideLifeBuoy, // Módulo de Soporte tecnico
  lucideCalculator, // Módulo de Contabilidad
  lucideGraduationCap, // Módulo de Matricula
  lucideShoppingBasket, // Módulo de Producto

  lucideUser,
  lucideUsers,
  lucidePlus, // Nuevo
  lucidePencil, // Editar
  lucideTrash2, // Eliminar

  lucideSquareCheck, // Active
  lucideSquare, // Inactive


  lucideSave, // Guardar
  lucideList,
  lucideEye,
  lucideLogOut, // Salir
  lucideCheckLine,   // Defect
  lucideToggleRight // Toogle

} from '@ng-icons/lucide';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { TipoArchivoCatalogoService } from './core/services/tipo-archivo-catalogo.service';




export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor, loadingInterceptor, errorInterceptor])),
    provideAnimations(),
    provideToastr({
      closeButton: true,
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true
    }),
    provideIcons({
      lucideHome,
      lucidePackage,
      lucideMenu,
      lucideMoreVertical,
      lucidePanelRightOpen, // Desplegar menu
      lucideDatabaseZap, // Módulo de Maestros
      lucideShieldUser, // Módulo de Seguridad
      lucideSchool, // Módulo de Empresa
      lucideBuilding2, // Módulo de Condominio
      lucideFileStack, // Módulo de Tramite documentario
      lucideLifeBuoy, // Módulo de Soporte tecnico
      lucideCalculator, // Módulo de Contabilidad
      lucideGraduationCap, // Módulo de Matricula
      lucideShoppingBasket, // Módulo de Producto


      lucideUser,
      lucideUsers,

      lucidePlus, // Nuevo
      lucidePencil, // Editar
      lucideTrash2, // Eliminar

      lucideSquareCheck, // Active
      lucideSquare, // Inactive

      lucideSave, // Guardar
      lucideList,
      lucideEye,
      lucideLogOut, // Salir
      lucideCheckLine,   // Defecto
      lucideToggleRight // Toogle
    })
  ]
};



