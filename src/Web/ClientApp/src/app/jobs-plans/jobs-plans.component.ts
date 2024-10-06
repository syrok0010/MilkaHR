import { DatePipe, NgForOf } from '@angular/common';
import { Component } from '@angular/core';
import { TuiRingChart } from '@taiga-ui/addon-charts';
import { TuiRepeatTimes } from '@taiga-ui/cdk';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiAvatar } from '@taiga-ui/kit';
import { TuiCardLarge, TuiCell, TuiHeader } from '@taiga-ui/layout';

@Component({
  selector: 'app-jobs-plans',
  standalone: true,
  imports: [TuiCardLarge,
    TuiHeader,
    TuiRingChart,
    TuiSurface,
    TuiTitle,
    DatePipe,
    TuiRepeatTimes,
    TuiCell,
    TuiAvatar,
    NgForOf,],
  templateUrl: './jobs-plans.component.html',
  styles: ``
})
export class JobsPlansComponent {
  apiClient = inject(RecruiterClient);
  interviews = toSignal(this.apiClient.getApiRecruiterInterviews());
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
