import { Component, OnInit } from '@angular/core';
import { Todo } from '../models/todo';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo-detail',
  templateUrl: './todo-detail.component.html',
  styleUrls: ['./todo-detail.component.css'],
})
export class TodoDetailComponent implements OnInit {
  todo: Todo;
  isFinishedText: string;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.todo = data['todo'];
      this.isFinishedText = this.todo.isFinished ? 'Teljesített' : 'Folyamatban...';
    });
  }
}
