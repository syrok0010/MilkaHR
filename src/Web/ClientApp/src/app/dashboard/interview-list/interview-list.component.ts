import { Component, inject, signal } from '@angular/core';
import { TuiCardLarge, TuiCell, TuiHeader } from '@taiga-ui/layout';
import { TuiRingChart } from '@taiga-ui/addon-charts';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { EventType, RecruiterClient } from '../../web-api-client';
import { DatePipe, NgForOf } from '@angular/common';
import { TuiRepeatTimes } from '@taiga-ui/cdk';
import { TuiAvatar } from '@taiga-ui/kit';
import { toSignal } from '@angular/core/rxjs-interop';

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
  interviews = toSignal(this.apiClient.getRecruiterInterviews());
  getIcon(type: EventType): string {
    switch (type) {
      case EventType.Interview:
        return 'notepad-text';
      case EventType.Meeting:
        return 'users';
      case EventType.Ride:
        return 'car';
      case EventType.VideoConference:
        return 'video';
    }
  }
}
