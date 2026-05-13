import { BaseModel } from "@sharedModels/base.model";
import { PerfilPaginaModel } from "./perfil-pagina.model";

export interface PerfilModel extends BaseModel  {
  coPerfil?: number;
  coModulo?: number;
  noPerfil?: string;
  flDefault: number;
  PerfilPaginaLst?: PerfilPaginaModel[]
}
