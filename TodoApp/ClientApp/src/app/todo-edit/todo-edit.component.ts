import { Component, OnInit, ViewChild } from '@angular/core';
import { Todo } from '../models/todo';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todo-edit',
  templateUrl: './todo-edit.component.html',
  styleUrls: ['./todo-edit.component.css'],
})
export class TodoEditComponent implements OnInit {
  todo: Todo;
  isEditMode: boolean;

  @ViewChild('editForm', { static: true }) editForm: NgForm;

  constructor(private route: ActivatedRoute, private router: Router, private todoService: TodoService) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.todo = data['todo'];
      this.isEditMode = this.todo ? true : false;
      if (!this.isEditMode) {
        this.todo = { id: '', title: '', description: '', isFinished: false };
      }
    });
  }

  sumbitTodo() {
    const id: string = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.updateTodo(id);
    } else {
      this.createTodo();
    }
  }

  updateTodo(id: string) {
    this.todoService.updateTodo(id, this.todo).subscribe(
      (next) => {
        console.log('Todo updated successfully!');
        this.router.navigate(['/']);
      },
      (error) => {
        console.log('Todo updated successfully!');
      },
    );
  }

  createTodo() {
    this.todoService.createTodo(this.todo).subscribe(
      (next) => {
        console.log('Todo created successfully!');
        this.router.navigate(['/']);
      },
      (error) => {
        console.log('Todo created successfully!');
      },
    );
  }
}
