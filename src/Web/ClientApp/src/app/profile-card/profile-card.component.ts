import { NgForOf } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { TuiTagModule } from '@taiga-ui/legacy';
import { TuiButton, TuiOption, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiBadge } from '@taiga-ui/kit';
import { Candidate, CandidateStatus } from '../web-api-client';
import { TuiDay } from '@taiga-ui/cdk';
import { RouterLink } from '@angular/router';
import { JobsPlansComponent } from '../jobs-plans/jobs-plans.component';

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [
    NgForOf,
    TuiButton,
    TuiCardLarge,
    TuiSurface,
    TuiTitle,
    TuiOption,
    TuiTagModule,
    TuiBadge,
    TuiHeader,
    RouterLink,
    JobsPlansComponent
  ],
  templateUrl: './profile-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileCardComponent {
  candidate = input.required<Candidate>();
  protected tags: readonly string[] = [
    'Taiga UI',
    'is an open-source library',
    'for awesome people',
  ];

  age = computed(() => {
    const c = this.candidate();
    const birthDate = TuiDay.fromUtcNativeDate(new Date(c.birthDate));
    const today = TuiDay.currentUtc();
    const daysBetween = TuiDay.lengthBetween(birthDate, today);
    return Math.floor(daysBetween / 365.25);
  });
  protected readonly CandidateStatus = CandidateStatus;

  getAppearance(processingStatus: CandidateStatus) {
    switch (processingStatus) {
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
}
