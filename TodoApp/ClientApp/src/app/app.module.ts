import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './resolvers/todo-list.resolver';
import { TodoCardComponent } from './todo-card/todo-card.component';

@NgModule({
  declarations: [AppComponent, NavMenuComponent, TodoListComponent, TodoCardComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [TodoListResolver],
  bootstrap: [AppComponent],
})
export class AppModule {}
