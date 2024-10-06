import { Component } from '@angular/core';
import { JobCardComponent } from '../job-card/job-card.component';

@Component({
  selector: 'app-jobs-page',
  standalone: true,
  imports: [JobCardComponent],
  templateUrl: './jobs-page.component.html',
})
export class JobsPageComponent {

}
