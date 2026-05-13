import { MenuModel } from "./menu.model";

export interface ModuloUsuarioModel {
  coModuloUsuario: number;
  coModulo: number;
  coPersona: number;
  coPerfil: number;
  nuOrden: number;
  noModulo: string;
  noPerfil: string;
  noIcono: string;
  noArea: string;
  noCarpetaVirtual: string;
  menuLst: MenuModel[];
}
