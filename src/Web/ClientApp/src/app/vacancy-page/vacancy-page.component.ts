import { Component } from '@angular/core';
import { VacancyInfoComponent } from '../vacancy-info/vacancy-info.component';
import { VacancyInfoMoreComponent } from '../vacancy-info-more/vacancy-info-more.component';

@Component({
  selector: 'app-vacancy-page',
  standalone: true,
  imports: [VacancyInfoComponent, VacancyInfoMoreComponent],
  templateUrl: './vacancy-page.component.html',
})
export class VacancyPageComponent {}
