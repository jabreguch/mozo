/**
 * Payload genérico para cualquier modal
 * model: la entidad principal a gestionar
 * relations: entidades relacionadas (claves foráneas)
 */
export interface ModalPayload<T = any> {
  model: T | null;  // Entidad principal
  relations?: Record<string, any>;  // Entidades relacionadas
  metaData?: {
    action?: 'update' | 'insert' | 'view';
    [key: string]: any;
  };
}

export interface ModalControlModel<T = any> {
  modalName: string;
  title: string;
  size?: 'xs' | 'sm' | 'md' | 'lg' | 'xl' | 'xl2' | 'xl3' | 'xl4' | 'xl5' | 'xl6' | 'xl7' | 'full';
  data: ModalPayload<T>;
}

