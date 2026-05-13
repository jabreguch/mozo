import { BaseModel } from "@sharedModels/base.model";

export interface PaginaModel extends BaseModel  {
  coModulo?: number;
  coMenu?: number;
  coArea?: number;
  coPagina?: number;
  nuOrden?: number;
  noArea?: string;
  noControlador?: string;
  noAccion?: string;
  noOpcion?: string;
  coTipoPagina:number;
}
