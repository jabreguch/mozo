import { CredencialModel } from "@sharedModels/seguridad/auth/credencial.model";

export interface CredencialResponseModel{
  credencial: CredencialModel;
  noToken: string;
  noTokenRefresh: string;
}
