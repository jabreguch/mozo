import { BaseModel } from "@sharedModels/base.model";

export interface PermisoModel  extends BaseModel  {
  coPermiso?: number;
  coPersona?: number;
  noUsuario?: string;
  noClave?: string;
}



