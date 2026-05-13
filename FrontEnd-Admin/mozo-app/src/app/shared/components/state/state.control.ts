import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NgIcon, provideIcons } from "@ng-icons/core";
import { lucideSquare, lucideSquareCheck } from '@ng-icons/lucide';

@Component({
  selector: 'mz-state-control',
  standalone: true,
  templateUrl: './state.control.html',
   providers: [
    provideIcons({
      lucideSquareCheck,
      lucideSquare
    })
  ],
  imports: [NgIcon]
})
export class StateControl {

  @Input() state!: number | null;


}
