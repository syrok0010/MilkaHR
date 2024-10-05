import { Component } from '@angular/core';
import {DashboardSummaryComponent} from "../summary/dashboard-summary.component";
import { JobByStatusTableComponent } from '../job-by-status-table/job-by-status-table.component';
import { JobsMonthBarChartComponent } from '../jobs-month-bar-chart/jobs-month-bar-chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DashboardSummaryComponent, JobByStatusTableComponent, JobsMonthBarChartComponent],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent {}
