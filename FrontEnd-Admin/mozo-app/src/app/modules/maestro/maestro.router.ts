import { Routes } from "@angular/router";

export const maestroRouter: Routes = [
  {

    path: '',
    children: [
       {
        path: 'tipoparticular',
        loadComponent: () =>
          import('./pages/tipo-particular/tipo-particular-list.page')
            .then(m => m.TipoParticularListPage)
      },
      {
        path: '',
        redirectTo: 'tipoparticular',
        pathMatch: 'full'
      },
      {
        path: '**',
        redirectTo: 'tipoparticular'
      }
    ]
  }
]

export default maestroRouter;
