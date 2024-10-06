import { Component } from '@angular/core';
import { CandidateInfoComponent } from '../candidate-info/candidate-info.component';
import { CandidateVacancyComponent } from '../candidate-vacancy/candidate-vacancy.component';

@Component({
  selector: 'app-candidat-page',
  standalone: true,
  imports: [CandidateInfoComponent, CandidateVacancyComponent],
  templateUrl: './candidat-page.component.html',
  styles: ``
})
export class CandidatPageComponent {

}
