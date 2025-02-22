import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { AsyncPipe } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  signal,
} from '@angular/core';
import { TuiRingChart } from '@taiga-ui/addon-charts';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { JobsClient, PriorityLevel } from '../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter } from 'rxjs';

@Component({
  selector: 'app-relevant-jobs-ring-chart',
  standalone: true,
  imports: [
    AsyncPipe,
    TuiRingChart,
    TuiHeader,
    TuiCardLarge,
    TuiSurface,
    TuiTitle,
  ],
  templateUrl: './relevant-jobs-ring-chart.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RelevantJobsRingChartComponent {
  private apiClient = inject(JobsClient);

  index = signal(NaN);
  label = computed(() => {
    const i = this.index();
    if (Number.isNaN(i)) {
      return 'Всего';
    }
    return PriorityLevel[this.jobsByPriority()[i].level];
  });
  count = computed(() => {
    const i = this.index();
    if (Number.isNaN(i)) {
      return this.jobsByPriority()
        .map((x) => x.opened)
        .reduce((s, a) => s + a, 0);
    }
    return this.jobsByPriority()[i].opened;
  });
  values = computed(() => this.jobsByPriority().map((e) => e.opened));
  labels: string[] = Object.values(PriorityLevel).map(
    (level) => PriorityLevel[level],
  );
  jobsByPriority = toSignal(
    this.apiClient.getJobsCountByPriority(6).pipe(filter((x) => !!x)),
  );
}
