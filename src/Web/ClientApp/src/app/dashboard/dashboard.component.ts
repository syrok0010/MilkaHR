import { Component } from '@angular/core';
import {DashboardSummaryComponent} from "../summary/dashboard-summary.component";
import { JobByStatusTableComponent } from '../job-by-status-table/job-by-status-table.component';
import { JobsMonthBarChartComponent } from '../jobs-month-bar-chart/jobs-month-bar-chart.component';
import {
  TUI_ALWAYS_DASHED,
  TUI_ALWAYS_NONE,
  TuiAxes,
  TuiBarChart,
  TuiLineChart
} from "@taiga-ui/addon-charts";
import {TuiCardLarge, TuiHeader} from "@taiga-ui/layout";
import {TuiSurface, TuiTitle} from "@taiga-ui/core";
import { ToDoListComponent } from '../to-do-list/to-do-list.component';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DashboardSummaryComponent, JobByStatusTableComponent, JobsMonthBarChartComponent, TuiAxes, TuiCardLarge, TuiHeader, TuiLineChart, TuiSurface, TuiTitle, TuiBarChart,ToDoListComponent],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent {
  protected readonly horizontalLinesHandler = TUI_ALWAYS_DASHED;
  protected readonly verticalLinesHandler = TUI_ALWAYS_NONE;
}
