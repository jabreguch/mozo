import { NgClass } from '@angular/common';
import { Component, Input, HostListener, OnInit, ChangeDetectionStrategy, signal, computed, inject } from '@angular/core';
import { ModalControlModel } from '@app/shared/models/controls/modal-control.model';
import { ModalService } from '@app/shared/services/modal.service';

type ModalSize = 'xs' | 'sm' | 'md' | 'lg' | 'xl' | 'xl2' | 'xl3' | 'xl4' | 'xl5' | 'xl6' | 'xl7' | 'full';

const MODAL_SIZES: Record<ModalSize, string> = {
  xs: 'max-w-xs', sm: 'max-w-sm', md: 'max-w-md', lg: 'max-w-lg', xl: 'max-w-xl',
  xl2: 'max-w-2xl', xl3: 'max-w-3xl', xl4: 'max-w-4xl', xl5: 'max-w-5xl',
  xl6: 'max-w-6xl', xl7: 'max-w-7xl', full: 'max-w-full'
};

@Component({
  selector: 'mz-modal-control',
  standalone: true,
  imports: [NgClass],
  templateUrl: './modal.control.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ModalControl<T = any> implements OnInit {
  @Input() name!: string;

  private readonly modalService = inject(ModalService);

  protected readonly currentModal$ = signal<string | null>(null);
  protected readonly title$ = signal<string>('Ventana');
  protected readonly size$ = signal<ModalSize>('md');
  //public readonly data$ = signal<T | null>(null);
  public readonly data$ = signal<any>(null);

  protected readonly modalWidthClass = computed(() => MODAL_SIZES[this.size$()]);
  protected readonly isOpen = computed(() => this.currentModal$() === this.name);

  ngOnInit(): void {
    this.modalService.modalState$.subscribe((modal: ModalControlModel<T> | null) => {
      if (!modal) {
        this.currentModal$.set(null);
        this.data$.set(null);
        return;
      }

      this.currentModal$.set(modal.modalName);

      if (modal.modalName === this.name) {
        this.title$.set(modal.title);
        this.size$.set(modal.size ?? 'md');
        this.data$.set(modal.data ?? null);       // ✅ Nuevo
      }
    });
  }

  close(): void {
    this.modalService.close();
  }
/*
  @HostListener('document:keydown.escape')
  onEsc(): void {
    if (this.isOpen()) {
      this.close();
    }
  }
    */
}
