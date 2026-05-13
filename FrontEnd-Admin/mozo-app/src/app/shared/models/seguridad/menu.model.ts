import { PaginaModel } from "@sharedModels/seguridad/pagina.model";
import { BaseModel } from "../base.model";

export interface MenuModel extends BaseModel {
  coMenu?: number;
  coPerfil?: number;
  noMenu?: string;
  nuOrden?: number;
  coPersona?: number;
  coModulo?: number;
  noModuloDescripcion?: string;
  paginaLst?: PaginaModel[];
}
