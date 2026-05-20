import { Component, Input, TemplateRef, ContentChild } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'mz-generic-grid',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div [class]="getGridClasses()">
      <div *ngFor="let item of items; let i = index" 
           [class]="getItemClasses()">
        <ng-container 
          [ngTemplateOutlet]="itemTemplate"
          [ngTemplateOutletContext]="{ $implicit: item, index: i }">
        </ng-container>
      </div>
    </div>
  `,
  styles: [`
    :host {
      display: block;
      width: 100%;
    }
  `]
})
export class GenericGridCControl<T> {
  @Input() items: T[] = [];
  @Input() columnas: number = 3; // Columnas en desktop
  @Input() mostrarBordes: boolean = true;
  @Input() espaciado: 2 | 4 | 6 | 8 = 4;
  @Input() bordeGrosor: 1 | 2 | 4 = 2;
  @Input() bordeColor: string = 'border-base-300';
  @Input() redondeado: 'sm' | 'md' | 'lg' | 'xl' | '2xl' = 'lg';
  @Input() sombra: boolean = true;
  @Input() hover: boolean = true;
  @Input() bgColor: string = 'bg-base-100';
  
  @ContentChild('itemTemplate') itemTemplate!: TemplateRef<any>;

  getGridClasses(): string {
    const colsMap: { [key: number]: string } = {
      1: 'lg:grid-cols-1',
      2: 'lg:grid-cols-2',
      3: 'lg:grid-cols-3',
      4: 'lg:grid-cols-4',
      5: 'lg:grid-cols-5',
      6: 'lg:grid-cols-6',
    };

    const gapMap = {
      2: 'gap-2',
      4: 'gap-4',
      6: 'gap-6',
      8: 'gap-8',
    };

    return `grid grid-cols-1 ${colsMap[this.columnas]} ${gapMap[this.espaciado]}`;
  }

  getItemClasses(): string {
    const classes = [this.bgColor];
    
    if (this.mostrarBordes) {
      const bordeMap = {
        1: 'border',
        2: 'border-2',
        4: 'border-4',
      };
      classes.push(bordeMap[this.bordeGrosor]);
      classes.push(this.bordeColor);
    }
    
    classes.push(`rounded-${this.redondeado}`);
    
    if (this.sombra) {
      classes.push('shadow-lg');
    }
    
    if (this.hover) {
      classes.push('hover:shadow-2xl hover:scale-102 transition-all duration-200');
    }
    
    return classes.join(' ');
  }
}