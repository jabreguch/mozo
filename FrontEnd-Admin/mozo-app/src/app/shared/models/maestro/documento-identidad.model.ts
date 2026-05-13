import { BaseModel } from "@sharedModels/base.model";

export interface DocumentoIdentidadModel extends BaseModel {
  coDocumentoIdentidad?: number;
  coPais?: string;
  noCodigo?: string;
  noDocumentoIdentidad?: string;
  noDocumentoIdentidadSigla?: string;
}
