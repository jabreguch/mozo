import { Component, computed, ElementRef, HostListener, inject, Input, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgIconComponent } from '@ng-icons/core';
import { DEFAULT_MENU_BUTTONS } from '@app/core/global/constants';
import { MenuControlTypeEnum, ButtonControlSizeEnum } from '@app/shared/enum/menu-control-type.enum';


//interface MenuButtonConfig extends Omit<MenuControlModel, 'action'> { }

@Component({
  selector: 'mz-button-control',
  standalone: true,
  imports: [CommonModule, NgIconComponent],
  template: `
    <button
      [type]="type()"
      [class]="defaultButton()?.cssButton + ' ' + moreClass()"
      (click)="handleClick()"
    >
        <ng-icon
          [name]="defaultButton()?.icon"
          class="text-xl"
          aria-hidden="true"
        />
      {{ label() != null? label():  defaultButton()?.label }}
    </button>
  `
})
export class ButtonControl {
  view = input<MenuControlTypeEnum>(MenuControlTypeEnum.Save);
  shortcut = input<string | null>(null);
  protected readonly defaultButton = computed(() => DEFAULT_MENU_BUTTONS.get(this.view()));

  label = input(null as string | null);
  type = input<'button' | 'submit' | 'reset'>('button');

  moreClass = input<string>('');

  /*
  <button class="btn btn-xs">Xsmall</button>
  <button class="btn btn-sm">Small</button>
  <button class="btn">Medium</button>
  <button class="btn btn-lg">Large</button>
  <button class="btn btn-xl">Xlarge</button>
  */

  private readonly el = inject(ElementRef);

  clicked = output<void>();

  handleClick() {
    this.clicked.emit();
  }

  @HostListener('document:keydown', ['$event'])
  handleKeydown(event: KeyboardEvent) {
    const key = this.shortcut();
    if (!key) return;

    const modifierKeys = ['control', 'alt', 'shift', 'meta'];
    if (modifierKeys.includes(event.key.toLowerCase())) return;

    const parts: string[] = [];
    if (event.ctrlKey) parts.push('ctrl');
    if (event.altKey) parts.push('alt');
    if (event.shiftKey) parts.push('shift');
    parts.push(event.key.toLowerCase());

    if (parts.join('+') === key.toLowerCase()) {
      event.preventDefault();
      // ✅ Simula click real en el botón (respeta type="submit")
      this.el.nativeElement.querySelector('button')?.click();
    }
  }


}
