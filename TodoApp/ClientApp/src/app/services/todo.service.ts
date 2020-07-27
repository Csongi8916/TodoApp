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

  getTodos(page?, itemsPerPage?): Observable<PaginatedResult<Todo[]>> {
    const paginatedResult: PaginatedResult<Todo[]> = new PaginatedResult<Todo[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
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

  /*getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'todo');
  }*/

  getTodo(id: string): Observable<Todo> {
    return this.http.get<Todo>(this.baseUrl + 'todo/' + id);
  }
}
