import { BaseModel } from "@sharedModels/base.model";

export interface RedSocialModel extends BaseModel {
  coEmpresa?: number;
  coRedSocial?: number;
  coEtiqueta?: number;
  coTipoUrl?: number;
  coTipoRedSocial?: number;
  noRedSocial?: string;
  coPersona?: number;
  flPersona?: number;
  flWhatsapp?: number;

  

  noIconoTipoUrl?: string;
  noEtiqueta?: string;
  noTipoRedSocial?: string;
  noTipoUrl?: string;

}
