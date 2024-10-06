import { Component, computed, signal } from '@angular/core';
import { DashboardSummaryComponent } from '../summary/dashboard-summary.component';
import { JobsMonthBarChartComponent } from '../jobs-month-bar-chart/jobs-month-bar-chart.component';
import { ToDoListComponent } from '../to-do-list/to-do-list.component';
import { RelevantJobsRingChartComponent } from '../relevant-jobs-ring-chart/relevant-jobs-ring-chart.component';
import { CandidateCountBarChartComponent } from './candidate-count-bar-chart/candidate-count-bar-chart.component';
import { InterviewListComponent } from './interview-list/interview-list.component';
import { AverageJobLifetimeComponent } from './average-job-lifetime/average-job-lifetime.component';
import { JobByStatusTableComponent } from './job-by-status-table/job-by-status-table.component';
import { TuiSegmented } from '@taiga-ui/kit';
import { toObservable } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    DashboardSummaryComponent,
    JobByStatusTableComponent,
    JobsMonthBarChartComponent,
    ToDoListComponent,
    RelevantJobsRingChartComponent,
    CandidateCountBarChartComponent,
    InterviewListComponent,
    AverageJobLifetimeComponent,
    TuiSegmented,
  ],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  monthsIndex = signal(0);
  months = computed(() => {
    switch (this.monthsIndex()) {
      case 0:
        return 1;
      case 1:
        return 3;
      case 2:
        return 6;
      case 3:
        return 12;
    }
    return 0;
  });
  months$ = toObservable(this.months);
}
