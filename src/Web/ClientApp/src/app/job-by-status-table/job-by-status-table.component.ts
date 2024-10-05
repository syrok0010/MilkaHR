import {Component} from '@angular/core';

@Component({
  selector: 'app-job-by-status-table',
  standalone: true,
  imports: [
  ],
  templateUrl: './job-by-status-table.component.html',
})
export class JobByStatusTableComponent {
  data = null;
  columns = null;
}
