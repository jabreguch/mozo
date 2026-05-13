import { NgClass } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, computed, signal } from '@angular/core';
import { DEFAULT_MENU_BUTTONS } from '@app/core/global/constants';
import { MenuControlTypeEnum } from '@app/shared/enum/menu-control-type.enum';
import { MenuControlModel } from '@app/shared/models/controls/menu-control.model';
import { NgIcon, provideIcons } from '@ng-icons/core';
import {
  lucideMoreVertical,
  lucidePencil,
  lucideTrash2,
  lucideSquareCheck,
  lucideDownload,
  lucideKey,
  lucidePlus,
  lucideSave,
  lucideSquare,
  lucideLogOut,
  lucideToggleRight // Toogle
} from '@ng-icons/lucide';

@Component({
  selector: 'mz-menu-control',
  standalone: true,
  imports: [NgIcon, NgClass],
  providers: [
    provideIcons({
      lucideMoreVertical,
      lucidePencil,
      lucideTrash2,
      lucideSquareCheck,
      lucideDownload,
      lucideKey,
      lucidePlus,
      lucideSave,
      lucideSquare,
      lucideLogOut,
      lucideToggleRight // Toogle
    })
  ],
  templateUrl: './menu.control.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MenuControl {
  @Input() set items(value: MenuControlModel[]) {
    this.items$.set(value);
  }
  @Input() set state(value: number | null) {
    this.state$.set(value);
  }

  @Output() action = new EventEmitter<string>();

  protected readonly items$ = signal<MenuControlModel[]>([]);
  protected readonly state$ = signal<number | null>(null);

  protected readonly enrichedItems = computed(() =>
    this.items$().map(item => this.enrichMenuItem(item))
  );

  private enrichMenuItem(item: MenuControlModel): MenuControlModel {
    const defaultButton = DEFAULT_MENU_BUTTONS.get(item.type);

    if (!defaultButton) {
      console.warn(`Tipo de menú no reconocido: ${item.type}`);
      return item;
    }

    const enriched = { ...defaultButton, ...item } as MenuControlModel;

    if (item.type === MenuControlTypeEnum.State) {
      const isInactive = this.state$() === 0;
      enriched.label = isInactive ? 'Activar' : 'Inactivar';
      enriched.color = isInactive ? 'text-success-700' : 'text-gray-700';
      enriched.icon = isInactive ? 'lucideSquareCheck' : 'lucideSquare';
    }

    return enriched;
  }

  onAction(item: MenuControlModel): void {
    item.action && this.action.emit(item.action);
  }
}
