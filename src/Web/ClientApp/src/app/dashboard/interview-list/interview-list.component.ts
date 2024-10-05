import { Component, inject, signal } from '@angular/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiRingChart } from '@taiga-ui/addon-charts';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import {
  Candidate,
  Interview,
  Job,
  RecruiterClient,
} from '../../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-interview-list',
  standalone: true,
  imports: [
    TuiCardLarge,
    TuiHeader,
    TuiRingChart,
    TuiSurface,
    TuiTitle,
    DatePipe,
  ],
  templateUrl: './interview-list.component.html',
})
export class InterviewListComponent {
  apiClient = inject(RecruiterClient);
  //interviews = toSignal(this.apiClient.getApiRecruiterInterviews());
  interviews = signal([
    new Interview({
      candidate: new Candidate({
        name: 'Вадим',
        lastName: 'Сыров',
        middleName: 'Александрович',
      }),
      job: new Job({
        title: 'Стажер-разработчик',
      }),
      timing: new Date(Date.now()),
    }),
    new Interview({
      candidate: new Candidate({
        name: 'Валерия',
        lastName: 'Сырова',
        middleName: 'Александрович',
      }),
      job: new Job({
        title: 'Стажер-разработчик',
      }),
      timing: new Date(Date.now() - 10000000),
    }),
  ]);
}
