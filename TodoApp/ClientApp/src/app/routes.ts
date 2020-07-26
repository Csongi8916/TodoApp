import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './resolvers/todo-list.resolver';

export const appRoutes: Routes = [
  { path: '', component: TodoListComponent, resolve: { todos: TodoListResolver } },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
