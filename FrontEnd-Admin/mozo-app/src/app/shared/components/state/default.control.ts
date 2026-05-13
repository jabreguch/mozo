import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NgIcon, provideIcons } from "@ng-icons/core";
import { lucideCheckLine } from '@ng-icons/lucide';

@Component({
  selector: 'mz-default-control',
  standalone: true,
  templateUrl: './default.control.html',
   providers: [
    provideIcons({
      lucideCheckLine
    })
  ],
  imports: [NgIcon]
})
export class DefaultControl {

  @Input() default!: number | null;


}
