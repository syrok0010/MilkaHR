import { Component } from '@angular/core';
import {DashboardSummaryComponent} from "../summary/dashboard-summary.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DashboardSummaryComponent],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent {}
