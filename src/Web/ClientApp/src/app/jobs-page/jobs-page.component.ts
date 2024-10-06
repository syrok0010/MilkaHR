import { Component, inject } from '@angular/core';
import { JobCardComponent } from '../job-card/job-card.component';
import { JobsClient } from '../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { J } from '@angular/cdk/keycodes';

@Component({
  selector: 'app-jobs-page',
  standalone: true,
  imports: [JobCardComponent],
  templateUrl: './jobs-page.component.html',
})
export class JobsPageComponent {
  apiClient = inject(JobsClient);
  jobs = toSignal(this.apiClient.getAllJobs(null, null, null, null));
}
