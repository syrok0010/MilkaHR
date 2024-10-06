import { AsyncPipe, NgFor } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiButton, TuiScrollbar, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiInputModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { TuiCheckbox } from '@taiga-ui/kit';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { CreateNoteCommand, Note, RecruiterClient } from '../web-api-client';
import { WaIntersectionRoot } from '@ng-web-apis/intersection-observer';

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
    TuiScrollbar,
    WaIntersectionRoot,
  ],
  templateUrl: './to-do-list.component.html',
  standalone: true,
})
export class ToDoListComponent implements OnInit {
  newTask: string = '';
  tasks = new BehaviorSubject<Note[]>([]);
  addTaskControl = new FormControl<string>('');
  allTasksCompleted$ = this.tasks.pipe(map((v) => v.every((t) => t.completed)));

  apiClient = inject(RecruiterClient);

  toggleAllTaskCompletion() {
    const allCompleted = this.tasks.value.every((t) => t.completed);
    this.tasks.next([
      ...this.tasks.value.map((t) => {
        t.completed = !allCompleted;
        return t;
      }),
    ]);
  }

  async ngOnInit() {
    this.tasks.next(await firstValueFrom(this.apiClient.getAllNotes()));
  }

  async addTask() {
    if (this.addTaskControl.value.trim() === '') {
      return;
    }

    const text = this.addTaskControl.value;
    const result = await firstValueFrom(
      this.apiClient.createNote(
        new CreateNoteCommand({
          text,
        }),
      ),
    );
    this.tasks.next([...this.tasks.value, result]);
    this.addTaskControl.reset('');
  }

  async toggleTaskCompletion(index: number) {
    await firstValueFrom(
      this.apiClient.completeNote(this.tasks.value[index].id),
    );
    this.tasks.next(
      this.tasks.value.map((e, i) => {
        if (i == index) e.completed = !e.completed;
        return e;
      }),
    );
  }

  async removeTask(index: number) {
    await firstValueFrom(this.apiClient.deleteNote(this.tasks.value[index].id));
    this.tasks.next([...this.tasks.value.filter((e, i) => i !== index)]);
  }
  async clearCompletedTasks() {
    const toDelete = [];
    for (let i = 0; i < this.tasks.value.length; i++) {
      const element = this.tasks.value[i];
      if (element.completed) toDelete.push(i);
    }

    for (const toDeleteElement of toDelete) {
      await this.removeTask(toDeleteElement);
    }
  }
}
