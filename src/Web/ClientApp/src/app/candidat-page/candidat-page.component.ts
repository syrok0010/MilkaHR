import { Component } from '@angular/core';
import { CandidateInfoComponent } from '../candidate-info/candidate-info.component';
import { CandidateVacancyComponent } from '../candidate-vacancy/candidate-vacancy.component';
import { JobsPlansComponent } from '../jobs-plans/jobs-plans.component';

@Component({
  selector: 'app-candidat-page',
  standalone: true,
  imports: [
    CandidateInfoComponent,
    CandidateVacancyComponent,
    JobsPlansComponent,
  ],
  templateUrl: './candidat-page.component.html',
  styles: ``,
})
export class CandidatPageComponent {}
