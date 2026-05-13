import { Routes } from "@angular/router";

const seguridadRouter: Routes = [
  {

    path: '',
    //component: MainComponent,
    children: [
      {
        path: 'empresa',
        loadComponent: () =>
          import('./pages/empresa/empresa-list.page')
            .then(m => m.EmpresaListPage)
      },
      {
        path: 'menu',
        loadComponent: () =>
          import('./pages/menu/menu-list.page')
            .then(m => m.MenuListPage)
      },
      {
        path: 'modulo',
        loadComponent: () =>
          import('./pages/modulo/modulo-list.page')
            .then(m => m.ModuloListPage)
      },
      {
        path: 'pagina/new',
        loadComponent: () =>
          import('./pages/pagina/pagina-form.page')
            .then(m => m.PaginaFormPage)
      },
      {
        path: 'perfil',
        loadComponent: () =>
          import('./pages/perfil/perfil-list.page')
            .then(m => m.PerfilListPage)
      },
      {
        path: '',
        redirectTo: 'empresa',
        pathMatch: 'full'
      },
      {
        path: '**',
        redirectTo: 'empresa'
      }

    ]
  }
]

export default seguridadRouter;
