import { Component } from '@angular/core';
import { DashboardSummaryComponent } from '../summary/dashboard-summary.component';
import { JobByStatusTableComponent } from '../job-by-status-table/job-by-status-table.component';
import { JobsMonthBarChartComponent } from '../jobs-month-bar-chart/jobs-month-bar-chart.component';
import { ToDoListComponent } from '../to-do-list/to-do-list.component';
import { RelevantJobsRingChartComponent } from '../relevant-jobs-ring-chart/relevant-jobs-ring-chart.component';
import { CandidateCountBarChartComponent } from './candidate-count-bar-chart/candidate-count-bar-chart.component';

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
  ],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {}
