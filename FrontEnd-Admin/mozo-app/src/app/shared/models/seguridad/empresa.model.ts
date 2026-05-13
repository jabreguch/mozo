import { BaseModel } from "@sharedModels/base.model";
import { HorarioModel } from "@sharedModels/empresa/horario.interface";
import { RedSocialModel } from "@app/shared/models/maestro/red-social.model";
import { EmpresaModuloModel } from "@app/shared/models/seguridad/empresa-modulo.model";

export interface EmpresaModel extends BaseModel {
  coEmpresa?: number;
  noVision?: string;
  noMision?: string;
  txQuienSoy?: string;
  noDireccion?: string;
  nuDocumento?: string;
  noSeo?: string;
  noEmpresa?: string;
  noEmpresaCorto?: string;
  coPais?: number;
  coMoneda?: number;
  coDocumentoIdentidad?: number;
  nuDocumentoFiscal?: string;
  empresaModuloLst?: EmpresaModuloModel[];
  horarioCol?: HorarioModel[];
  redSocialLst?: RedSocialModel[];
}
