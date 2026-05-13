import { Component, inject, signal } from '@angular/core';
import { MainComponent } from "./layout/main/main.component";
import { LoadingService } from './core/services/loading.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [MainComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('mozo-app');
   loadingService = inject(LoadingService); // 👈 agrega esto
}
