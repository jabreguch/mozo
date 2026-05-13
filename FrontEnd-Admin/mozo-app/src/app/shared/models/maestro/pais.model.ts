import { BaseModel } from "@sharedModels/base.model";
import { MonedaModel } from './moneda.model';

export interface PaisModel extends BaseModel {
  coPais?: number;
  noPais?: string;
  noCodigoIso2?: string;
  noCodigoIso3?: string;
  noPrefijoTelefono?: string;
  coMoneda?: number;

  /*Campos anexos*/
  moneda: MonedaModel;

}
