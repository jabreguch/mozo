import { BaseModel } from "@sharedModels/base.model";

export interface EmpresaModuloModel extends BaseModel {
  coEmpresaModulo?: number;
  coModulo?: number;
  coEmpresa?: number;

  /*Otras propiedades */
  nuOrden?: number;
  noModulo?: string;
}
