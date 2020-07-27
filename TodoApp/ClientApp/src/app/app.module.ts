import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './resolvers/todo-list.resolver';
import { TodoDetailResolver } from './resolvers/todo-detail.resolver';
import { TodoCardComponent } from './todo-card/todo-card.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TodoDetailComponent } from './todo-detail/todo-detail.component';

@NgModule({
  declarations: [AppComponent, NavMenuComponent, TodoListComponent, TodoCardComponent, TodoDetailComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
  ],
  providers: [TodoListResolver, TodoDetailResolver],
  bootstrap: [AppComponent],
})
export class AppModule {}
