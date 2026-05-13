import { MenuControlTypeEnum } from "@app/shared/enum/menu-control-type.enum";
import { MenuControlModel } from "@app/shared/models/controls/menu-control.model";

export const MSG_FIELD_FORM =
{
  COMBO_REQUIRED: ".:Seleccione:.",
  COMBO_CRITERIA: ".:Todos:."

}

export const API_ROUTES = {
  seguridad: {
    modulo: '/seguridad/modulo',
    menu: '/seguridad/menu',
    pagina: '/seguridad/pagina',
    perfilPagina: '/seguridad/perfil-pagina',
    perfil: '/seguridad/perfil',
    empresa: '/seguridad/empresa',
    empresaModulo: '/seguridad/empresa-modulo'
  },
  maestro: {
    documentoIdentidad: '/maestro/documento-identidad',
    pais: '/maestro/pais',
    redSocial: '/maestro/red-social',
    tipoGeneral: '/maestro/tipo-general',
    tipoParticular: '/maestro/tipo-particular',
    archivo: '/maestro/archivo'
  },
  login: {
    permiso: '/login/permiso',
    menu: '/login/menu'
  }
};


export const STORAGE_KEYS = {
  TOKEN: 'app_token',
  MENUS: 'app_menus',
  USER: 'app_user',
  REFRESH_TOKEN: 'app_refresh_token',
  TIPO_ARCHIVO_CATALOGO: 'cat_tipo_archivo'
} as const;


export type MenuButtonConfig = Omit<MenuControlModel, 'action'>;

export const DEFAULT_MENU_BUTTONS: ReadonlyMap<MenuControlTypeEnum, MenuButtonConfig> = new Map([
  [MenuControlTypeEnum.New, {
    label: 'Nuevo',
    icon: 'lucidePlus',
    color: 'text-black',
    type: MenuControlTypeEnum.New,
    cssButton: 'btn btn-neutral'
  }],
  [MenuControlTypeEnum.Edit, {
    label: 'Editar',
    icon: 'lucidePencil',
    color: 'text-blue-700',
    type: MenuControlTypeEnum.Edit,
    cssButton: 'btn btn-outline btn-primary'
  }],
  [MenuControlTypeEnum.Save, {
    label: 'Guardar',
    icon: 'lucideSave',
    color: 'text-success-700',
    type: MenuControlTypeEnum.Save,
    cssButton: 'btn btn-primary'
  }],
  [MenuControlTypeEnum.Cancel, {
    label: 'Salir',
    icon: 'lucideLogOut',
    color: 'text-warning-700',
    type: MenuControlTypeEnum.Cancel,
    cssButton: 'btn btn-warning'
  }],
  [MenuControlTypeEnum.Delete, {
    label: 'Eliminar',
    icon: 'lucideTrash2',
    color: 'text-red-700',
    type: MenuControlTypeEnum.Delete,
    cssButton: 'btn btn-soft btn-error'
  }],
  [MenuControlTypeEnum.State, {
    label: 'Inactivar',
    icon: 'lucideSquare',
    color: 'text-gray-700',
    type: MenuControlTypeEnum.State,
    cssButton: 'btn btn-soft btn-success'
  }],
  [MenuControlTypeEnum.ToggleOn, {
    label: 'Activar',
    icon: 'lucideToggleRight',
    color: 'text-success-700',
    type: MenuControlTypeEnum.State,
    cssButton: 'btn btn-soft btn-success'
  }],
  //lucideToggleRight
]);
