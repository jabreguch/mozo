import { BaseModel } from "@sharedModels/base.model";

export interface HorarioModel extends BaseModel {
  CoHorario?: number;
  CoDia?: number;
  NoDia: string;
  HoInicio: string;
  HoFinal: string;
}
