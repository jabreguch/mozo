import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { OptionService } from '@app/core/services/option.service';
import { NgIcon } from "@ng-icons/core";

@Component({
  selector: '[app-sidebar-component]',
  imports: [RouterLink, RouterLinkActive, NgIcon],
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  private optionService = inject(OptionService);

  modulos = this.optionService.modulos;

  constructor() {
    this.optionService.loadMenus();
  }

}

