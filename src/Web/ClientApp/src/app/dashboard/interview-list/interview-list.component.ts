import { Component, inject, signal } from '@angular/core';
import { TuiCardLarge, TuiCell, TuiHeader } from '@taiga-ui/layout';
import { TuiRingChart } from '@taiga-ui/addon-charts';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import {
  Candidate,
  Interview,
  Job,
  RecruiterClient,
} from '../../web-api-client';
import { DatePipe, NgForOf } from '@angular/common';
import { TuiRepeatTimes } from '@taiga-ui/cdk';
import { TuiAvatar } from '@taiga-ui/kit';

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
    TuiRepeatTimes,
    TuiCell,
    TuiAvatar,
    NgForOf,
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
