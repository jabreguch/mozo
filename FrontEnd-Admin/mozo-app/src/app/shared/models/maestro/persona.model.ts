import { BaseModel } from "@sharedModels/base.model";

export interface PersonaModel extends BaseModel {
  coEmpresa?: number;
  coPersona?: number;
  coDocumentoIdentidad?: number;
  nuDocumento?: string;
  noPersona?: string;
  noApellidoP?: string;
  noApellidoM?: string;
  noDireccion?: string;
  noCodigoPostal?: string;
  coEstadoCivil?: number;
  noReferencia?: string;
  txDescripcion?: string;
  coProfesion?: number;
  coSexo?: number;
  coRubro?: number;
  coPais?: number;
  feNacimiento?: string;
}





