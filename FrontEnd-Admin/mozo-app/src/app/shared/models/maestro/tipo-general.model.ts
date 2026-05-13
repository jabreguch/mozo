import { BaseModel } from "@sharedModels/base.model";

export interface TipoGeneralModel extends BaseModel {
  coGrupo?: number;
  coTipo?: number;
  coModulo?: number;
  noTipo?: string;
  noSigla?: string;
  flDefault?: number;
  nuOrden?: number;
  noComando?: string;
  valor?: any;
}
