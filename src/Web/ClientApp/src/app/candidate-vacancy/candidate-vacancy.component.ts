import { Component, computed, inject, input, OnInit } from '@angular/core';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiBadge, TuiTile } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { CandidatesClient, CandidateStatus } from '../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-candidate-vacancy',
  standalone: true,
  imports: [TuiBadge, TuiSurface, TuiCardLarge, TuiTile, TuiHeader, TuiTitle],
  templateUrl: './candidate-vacancy.component.html',
})
export class CandidateVacancyComponent implements OnInit {
  apiClient = inject(CandidatesClient);
  candidateId = input.required<number>();
  route = inject(ActivatedRoute);
  getCandidateId(): number {
    return Number(this.route.snapshot.paramMap.get('id'));
  }
  candidate = toSignal(this.apiClient.getCandidate(this.getCandidateId()));
  vacancies = computed(() =>
    this.candidate().jobStatuses.map((s) => [s.job.title, s.processingStatus]),
  );

  ngOnInit() {}

  getAppearance(processingStatus: string | CandidateStatus) {
    const p = processingStatus as CandidateStatus;
    switch (p) {
      case CandidateStatus.InterviewScheduled:
        return 'info';
      case CandidateStatus.Hired:
        return 'success';
      case CandidateStatus.Denied:
        return 'error';
      case CandidateStatus.CvCreated:
        return 'warning';
      case CandidateStatus.CvApproved:
        return 'accent';
      case CandidateStatus.InterviewCompleted:
        return 'primary';
    }
  }

  protected readonly CandidateStatus = CandidateStatus;
}
