import { Component, Input, Output, EventEmitter, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'mz-pagination-control',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pagination.control.html'
})
export class PaginationControl {

  @Input() page = 1;
  @Input() totalPages = 1;

  @Output() change = new EventEmitter<number>();

  _page = signal(1);
  _totalPages = signal(1);

  ngOnChanges() {
    this._page.set(this.page);
    this._totalPages.set(this.totalPages);
  }

  // 🔥 solo 5 páginas visibles
  pages = computed(() => {
    const total = this._totalPages();
    const current = this._page();

    const start = Math.max(1, current - 2);
    const end = Math.min(total, current + 2);

    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
  });

  goTo(p: number) {
    if (p < 1 || p > this._totalPages()) return;
    this.change.emit(p);
  }

  prev() {
    this.goTo(this._page() - 1);
  }

  next() {
    this.goTo(this._page() + 1);
  }
}
