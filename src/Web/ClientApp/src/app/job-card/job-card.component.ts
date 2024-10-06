import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiDataListWrapper, TuiFilterByInputPipe } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputModule } from '@taiga-ui/legacy';
import { Job, JobCategory, JobStatus, PriorityLevel } from '../web-api-client';
import { DatePipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [
    TuiCardLarge,
    TuiSurface,
    ReactiveFormsModule,
    TuiDataListWrapper,
    TuiFilterByInputPipe,
    TuiInputModule,
    TuiTitle,
    TuiHeader,
    NgIf,
    DatePipe,
  ],
  templateUrl: './job-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobCardComponent {
  job = input.required<Job>();
  protected readonly PriorityLevel = PriorityLevel;
  protected readonly JobStatus = JobStatus;
  protected readonly JobCategory = JobCategory;
}
