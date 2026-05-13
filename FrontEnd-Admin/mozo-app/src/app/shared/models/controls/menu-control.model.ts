import { MenuControlTypeEnum } from "@app/shared/enum/menu-control-type.enum";

export interface MenuControlModel {
  label?: string;
  icon?: string;
  color?: string;
  type: MenuControlTypeEnum;
  action: string;
  cssButton?: string;
}
