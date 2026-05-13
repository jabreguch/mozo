export interface ArchivoSingleConfig {
  // Identificación
  coEmpresa?: number | null;
  flEmpresaNotKey?: number;
  coTipoEntidad: number;
  coEntidad: number;
  coTipo: number;

  // UI
  accept: string;
  label: string;
  maxSizeMB: number;

  // Comportamiento
  readonly?: boolean;
  autoLoad?: boolean;  // Si carga el archivo existente al iniciar (default true)

  //flGaleria: number;  // Indica si solo se inserta un archivo o varios
}
