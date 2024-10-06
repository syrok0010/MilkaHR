import { Component, computed, inject } from '@angular/core';
import { ProfileCardComponent } from '../profile-card/profile-card.component';
import { CandidatesClient, JobsClient } from '../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { NgForOf } from '@angular/common';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import {
  TuiInputRangeModule,
  TuiMultiSelectModule,
  TuiTextfieldControllerModule,
} from '@taiga-ui/legacy';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { startWith, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-applicants-page',
  standalone: true,
  imports: [
    ProfileCardComponent,
    NgForOf,
    TuiSurface,
    TuiCardLarge,
    TuiHeader,
    TuiTitle,
    TuiInputRangeModule,
    TuiMultiSelectModule,
    TuiTextfieldControllerModule,
    ReactiveFormsModule,
  ],
  templateUrl: './applicants-page.component.html',
})
export class ApplicantsPageComponent {
  filters = new FormGroup({
    age: new FormControl<[number, number]>([18, 80]),
    experience: new FormControl<[number, number]>([0, 80]),
    jobs: new FormControl<string[]>([]),
  });

  apiClient = inject(CandidatesClient);
  candidates = toSignal(
    this.filters.valueChanges.pipe(
      tap((f) => console.log('call', f)),
      startWith(null),
      switchMap((f) =>
        this.apiClient.getAllCandidates(
          null,
          f?.age[0],
          f?.age[1],
          f?.experience[0],
          f?.experience[1],
          null,
          f?.jobs,
          null,
        ),
      ),
    ),
  );

  jobsClient = inject(JobsClient);
  allJobs = toSignal(this.jobsClient.getAllJobs(null, null, null, null));
  allJobsNames = computed(() => this.allJobs().map((e) => e.title));
}
