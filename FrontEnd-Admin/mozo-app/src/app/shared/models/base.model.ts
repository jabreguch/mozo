export interface BaseModel {
  flEstReg?: number;
  rowsCount?: number;
  pageSize?: number;
  pageIndex?: number;
  flEmpresaNotKey?: number;
}

export interface PagedResult<T> {
  rowsCount: number;
  data: T[];
}
