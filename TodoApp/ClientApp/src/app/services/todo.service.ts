import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo';
import { PaginatedResult } from '../models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTodos(page?, itemsPerPage?, finishState?, searchedTitle?: string): Observable<PaginatedResult<Todo[]>> {
    const paginatedResult: PaginatedResult<Todo[]> = new PaginatedResult<Todo[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    if (finishState != null) {
      params = params.append('isFinished', finishState);
    }
    if (searchedTitle != null) {
      params = params.append('title', searchedTitle);
    }

    return this.http
      .get<Todo[]>(this.baseUrl + 'todo', { observe: 'response', params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;

          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }),
      );
  }

  getTodo(id: string): Observable<Todo> {
    return this.http.get<Todo>(this.baseUrl + 'todo/' + id);
  }

  updateTodo(id: string, todo: Todo) {
    return this.http.put(this.baseUrl + 'todo/' + id, todo);
  }

  createTodo(todo: Todo) {
    return this.http.post(this.baseUrl + 'todo/', todo);
  }

  deleteTodo(id: string) {
    return this.http.delete(this.baseUrl + 'todo/' + id);
  }
}
