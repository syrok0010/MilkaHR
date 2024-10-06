import { Component, inject } from '@angular/core';
import { CandidateInfoComponent } from '../candidate-info/candidate-info.component';
import { CandidateVacancyComponent } from '../candidate-vacancy/candidate-vacancy.component';
import { JobsPlansComponent } from '../jobs-plans/jobs-plans.component';
import { IconCvComponent } from '../icon-cv/icon-cv.component';
import { IconReviewComponent } from '../icon-review/icon-review.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-candidat-page',
  standalone: true,
  imports: [
    CandidateInfoComponent,
    CandidateVacancyComponent,
    JobsPlansComponent,
    IconCvComponent,
    IconReviewComponent,
  ],
  templateUrl: './candidat-page.component.html',
  styles: ``,
})
export class CandidatPageComponent {
  route = inject(ActivatedRoute);
  getCandidateId(): number {
    return Number(this.route.snapshot.paramMap.get('id'));
  }
}
