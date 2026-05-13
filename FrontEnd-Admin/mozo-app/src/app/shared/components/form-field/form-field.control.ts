import { Component, input, HostBinding } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'mz-form-field-control',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './form-field.control.html',
  styleUrls: ['./form-field.control.css']
})
export class FormFieldControl {

  label = input('');
  errorMessage = input<string | null>(null);
  readonly = input(false);
  hint = input('');

  colSpan = input<number>(12);

  private spanClasses: Record<number, string> = {
    1: 'col-span-12 md:col-span-1',
    2: 'col-span-12 md:col-span-2',
    3: 'col-span-12 md:col-span-3',
    4: 'col-span-12 md:col-span-4',
    5: 'col-span-12 md:col-span-5',
    6: 'col-span-12 md:col-span-6',
    7: 'col-span-12 md:col-span-7',
    8: 'col-span-12 md:col-span-8',
    9: 'col-span-12 md:col-span-9',
    10: 'col-span-12 md:col-span-10',
    11: 'col-span-12 md:col-span-11',
    12: 'col-span-12 md:col-span-12',
  };

  @HostBinding('class')
  get hostClasses(): string {
    return this.spanClasses[this.colSpan()] ?? this.spanClasses[12];
  }

  @HostBinding('style.display')
  display = 'block'; // 🔥 clave para grid
}
