import { Component, OnInit } from '@angular/core';
import { Todo } from '../models/todo';
import { TodoService } from '../services/todo.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../models/pagination';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  todos: Todo[];
  pagination: Pagination;

  constructor(private todoService: TodoService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.todos = data['todos'].result;
      this.pagination = data['todos'].pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadTodos();
  }

  loadTodos() {
    this.todoService.getTodos(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
      (res: PaginatedResult<Todo[]>) => {
        this.todos = res.result;
        this.pagination = res.pagination;
      },
      (error) => {
        console.log(error);
      },
    );
  }
}
