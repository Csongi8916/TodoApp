import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'todo');
  }

  getTodo(id: string): Observable<Todo> {
    return this.http.get<Todo>(this.baseUrl + 'todo/' + id);
  }
}
