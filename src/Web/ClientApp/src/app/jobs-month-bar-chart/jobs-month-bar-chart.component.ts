import { map } from 'rxjs/operators';
import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {TuiAxes, TuiBarChart} from '@taiga-ui/addon-charts';
import {tuiCeil} from '@taiga-ui/cdk';
import { JobsClient } from '../web-api-client';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-jobs-month-bar-chart',
  standalone: true,
  imports: [TuiAxes, TuiBarChart, AsyncPipe],
  templateUrl: './jobs-month-bar-chart.component.html',
  styles: ``
})
export class JobsMonthBarChartComponent {
  jobService = inject(JobsClient);
  data$ = this.jobService.getJobsByMonthStats();
  xLabels$ = this.data$.pipe(map(x => ['6', '7', '8', '9', "10"]));
  values$ = this.data$.pipe(map(x => [[1, 2, 3, 4, 5]]));
  labelsY: readonly string[] = ["1", "10"];
}
