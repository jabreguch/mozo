import { Component, inject } from '@angular/core';
import { NavbarComponent } from "@layout/navbar/navbar.component";
import { SidebarComponent } from "@layout/sidebar/sidebar.component";


@Component({
  selector: 'app-main',
  imports: [NavbarComponent, SidebarComponent],
   standalone: true,
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent {

}
