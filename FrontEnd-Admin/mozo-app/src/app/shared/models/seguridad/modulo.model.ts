import { BaseModel } from "@sharedModels/base.model";

export interface ModuloModel extends BaseModel {
  coModulo?: number;
  noModulo?: string;
  noIcono?: string;
  noModuloDescripcion?: string;
  nuOrden?: number;
  flArea?: number;
  noArea?: string;
}
