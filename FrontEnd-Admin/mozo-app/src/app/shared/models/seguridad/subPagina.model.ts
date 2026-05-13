import { BaseModel } from "@sharedModels/base.model";
export interface SubPaginaModel  extends BaseModel  {
  coModulo: number;
  CoMenu: number;
  coArea: number;
  coPagina: number;
  nuOrden: number;
  noControlador: string;
  noAccion: string;
  noOpcion: string;
  noParametro: string;
  txDescripcion: string;
}
