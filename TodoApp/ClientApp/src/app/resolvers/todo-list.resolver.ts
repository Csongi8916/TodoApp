import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TodoService } from '../services/todo.service';
import { Todo } from '../models/todo';

@Injectable()
export class TodoListResolver implements Resolve<Todo[]> {
  pageNumber = 1;
  pageSize = 25;

  constructor(private todoService: TodoService, private router: Router) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Todo[]> {
    return this.todoService.getTodos(this.pageNumber, this.pageSize).pipe(
      catchError((error) => {
        console.log(error);
        this.router.navigate(['/']);
        return of(null);
      }),
    );
  }
}
