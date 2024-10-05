import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import {TuiCardLarge, TuiHeader} from "@taiga-ui/layout";
import {TuiSurface, TuiTitle} from "@taiga-ui/core";

interface Task {
  title: string;
  completed: boolean;

}

@Component({
  selector: 'app-to-do-list',
  imports:[NgFor,TuiCardLarge,TuiHeader,TuiSurface],
  templateUrl: './to-do-list.component.html',
  standalone: true
})
export class ToDoListComponent {
  newTask: string = '';
  tasks: Task[] = [
    { title: 'Finish report', completed: false },
    { title: 'Grocery shopping', completed: false },
    { title: 'Call mom', completed: true }
  ];

  get allTasksCompleted(): boolean {
    return this.tasks.every(task => task.completed);
  }

  toggleAllTaskCompletion() {
    const allCompleted = this.allTasksCompleted;
    this.tasks.forEach(task => task.completed = !allCompleted);
  }

  onInputChange(event: Event) {
    this.newTask = (event.target as HTMLInputElement).value;
  }
  onTaskTitleChange(index: number, event: Event) {
    this.tasks[index].title = (event.target as HTMLInputElement).value;
  }

  addTask() {
    if (this.newTask.trim() !== '') {
      this.tasks.push({ title: this.newTask, completed: false });
      this.newTask = '';
    }
  }



  toggleTaskCompletion(index: number) {
    this.tasks[index].completed = !this.tasks[index].completed;
  }

  removeTask(index: number) {
    this.tasks.splice(index, 1);
  }
  clearCompletedTasks() {
    this.tasks = this.tasks.filter(task => !task.completed);
  }
}
