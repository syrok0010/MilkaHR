import { AsyncPipe, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiButton, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiInputModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { TuiCheckbox } from '@taiga-ui/kit';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

interface Task {
  title: string;
  completed: boolean;
}

@Component({
  selector: 'app-to-do-list',
  imports: [
    NgFor,
    TuiCardLarge,
    TuiHeader,
    TuiSurface,
    TuiTitle,
    TuiButton,
    TuiInputModule,
    ReactiveFormsModule,
    TuiCheckbox,
    AsyncPipe,
    TuiTextfieldControllerModule,
  ],
  templateUrl: './to-do-list.component.html',
  standalone: true,
})
export class ToDoListComponent {
  newTask: string = '';
  tasks = new BehaviorSubject<Task[]>([
    { title: 'Finish report', completed: false },
    { title: 'Grocery shopping', completed: false },
    { title: 'Call mom', completed: true },
  ]);
  addTaskControl = new FormControl<string>('');

  allTasksCompleted$ = this.tasks.pipe(map((v) => v.every((t) => t.completed)));

  toggleAllTaskCompletion() {
    const allCompleted = this.tasks.value.every((t) => t.completed);
    this.tasks.next([
      ...this.tasks.value.map((t) => {
        t.completed = !allCompleted;
        return t;
      }),
    ]);
  }

  onInputChange(event: Event) {
    this.newTask = (event.target as HTMLInputElement).value;
  }
  onTaskTitleChange(index: number, event: Event) {
    this.tasks[index].title = (event.target as HTMLInputElement).value;
  }

  addTask() {
    if (this.addTaskControl.value.trim() !== '') {
      this.tasks.next([
        ...this.tasks.value,
        { title: this.addTaskControl.value, completed: false },
      ]);
      this.addTaskControl.reset('');
    }
  }

  toggleTaskCompletion(index: number) {
    this.tasks.next(
      this.tasks.value.map((e, i) => {
        if (i == index) e.completed = !e.completed;
        return e;
      }),
    );
  }

  removeTask(index: number) {
    this.tasks.next([...this.tasks.value.filter((e, i) => i !== index)]);
  }
  clearCompletedTasks() {
    this.tasks.next([...this.tasks.value.filter((e) => !e.completed)]);
  }
}
