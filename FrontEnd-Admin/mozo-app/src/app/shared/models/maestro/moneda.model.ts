import { BaseModel } from "@sharedModels/base.model";

export interface MonedaModel extends BaseModel {
  coMoneda?: number;
  noCodigoIso?: string;
  nuCodigoIso?: number;
  noMoneda?: string;
  noSimbolo?: string;
}

