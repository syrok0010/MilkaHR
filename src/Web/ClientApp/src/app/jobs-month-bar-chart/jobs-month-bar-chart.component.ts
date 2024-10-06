import { map } from 'rxjs/operators';
import { Component, inject } from '@angular/core';
import { TuiAxes, TuiBarChart, TuiLineChart } from '@taiga-ui/addon-charts';
import { JobsClient } from '../web-api-client';
import { AsyncPipe } from '@angular/common';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiPoint, TuiSurface, TuiTitle } from '@taiga-ui/core';

@Component({
  selector: 'app-jobs-month-bar-chart',
  standalone: true,
  imports: [
    TuiAxes,
    TuiBarChart,
    AsyncPipe,
    TuiCardLarge,
    TuiSurface,
    TuiTitle,
    TuiHeader,
    TuiLineChart,
  ],
  templateUrl: './jobs-month-bar-chart.component.html',
})
export class JobsMonthBarChartComponent {
  jobService = inject(JobsClient);
  data$ = this.jobService.getJobsByMonthStats();
  xLabels$ = this.data$.pipe(map((x) => Object.keys(x)));
  values$ = this.data$.pipe(map((x) => [Object.values(x)]));
  labelsY: readonly string[] = ['1', '10', '30'];
  values: readonly TuiPoint[] = [
    [0, 12.23],
    [150, 31.12],
    [300, 49.65],
    [450, 28.2567],
    [600, 52.123525],
    [750, 19.532],
  ];
}
