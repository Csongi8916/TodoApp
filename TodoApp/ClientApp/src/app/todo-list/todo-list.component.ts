import { Component, OnInit } from '@angular/core';
import { Todo } from '../models/todo';
import { TodoService } from '../services/todo.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  todos: Todo[];

  constructor(private todoService: TodoService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.todos = data['todos'];
    });
  }
}
