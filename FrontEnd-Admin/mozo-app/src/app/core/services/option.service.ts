import { Injectable, signal } from '@angular/core';
import { STORAGE_KEYS } from '@app/core/global/constants';
import { ModuloUsuarioModel } from '@app/shared/models/seguridad/modulo-usuario.model';


@Injectable({
  providedIn: 'root'
})
export class OptionService {
  private menusSignal = signal<ModuloUsuarioModel[]>([]);

  modulos = this.menusSignal.asReadonly();

  setMenus(menus: ModuloUsuarioModel[]) {
    this.menusSignal.set([...menus]);
    localStorage.setItem(STORAGE_KEYS.MENUS, JSON.stringify(menus));
  }

  loadMenus() {
    const data = localStorage.getItem(STORAGE_KEYS.MENUS);
    if (data) {
      this.menusSignal.set(JSON.parse(data));
    }
  }

  clear() {
    this.menusSignal.set([]);
    localStorage.removeItem(STORAGE_KEYS.MENUS);
  }

}
