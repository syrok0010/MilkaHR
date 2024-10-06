import { Component } from '@angular/core';
import { VacancyInfoComponent } from '../vacancy-info/vacancy-info.component';

@Component({
  selector: 'app-vacancy-page',
  standalone: true,
  imports: [VacancyInfoComponent],
  templateUrl: './vacancy-page.component.html',
  styles: ``
})
export class VacancyPageComponent {

}
