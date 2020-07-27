import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Todo } from '../models/todo';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todo-card',
  templateUrl: './todo-card.component.html',
  styleUrls: ['./todo-card.component.css'],
})
export class TodoCardComponent implements OnInit {
  @Input() todo: Todo;
  // @Output() delete: EventEmitter<Todo> = new EventEmitter<Todo>();

  constructor(private todoService: TodoService) {}

  ngOnInit() {}

  deleteTodo() {
    this.todoService.deleteTodo(this.todo.id).subscribe(
      (next) => {
        console.log('Todo deleted successfully!');
      },
      (error) => {
        console.log('error');
      },
    );
  }
}
