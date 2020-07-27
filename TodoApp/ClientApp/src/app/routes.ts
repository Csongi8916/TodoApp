import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './resolvers/todo-list.resolver';
import { TodoDetailResolver } from './resolvers/todo-detail.resolver';
import { TodoDetailComponent } from './todo-detail/todo-detail.component';

export const appRoutes: Routes = [
  { path: '', component: TodoListComponent, resolve: { todos: TodoListResolver } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'detail/:id',
        component: TodoDetailComponent,
        resolve: { users: TodoDetailResolver },
      },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
