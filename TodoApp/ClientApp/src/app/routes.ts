import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './resolvers/todo-list.resolver';
import { TodoDetailResolver } from './resolvers/todo-detail.resolver';
import { TodoDetailComponent } from './todo-detail/todo-detail.component';
import { TodoEditComponent } from './todo-edit/todo-edit.component';
import { TodoEditResolver } from './resolvers/todo-edit.resolver';

export const appRoutes: Routes = [
  { path: '', component: TodoListComponent, resolve: { todos: TodoListResolver } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'detail/:id',
        component: TodoDetailComponent,
        resolve: { todo: TodoDetailResolver },
      },
      {
        path: 'edit/:id',
        component: TodoEditComponent,
        resolve: { todo: TodoEditResolver },
      },
      {
        path: 'create',
        component: TodoEditComponent,
      },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
