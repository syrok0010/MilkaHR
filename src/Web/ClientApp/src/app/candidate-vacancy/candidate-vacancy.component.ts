import { Component } from '@angular/core';
import { TuiSurface } from '@taiga-ui/core';
import { TuiBadge, TuiTile } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';

@Component({
  selector: 'app-candidate-vacancy',
  standalone: true,
  imports: [TuiBadge,TuiSurface,TuiCardLarge,TuiTile,TuiHeader],
  templateUrl: './candidate-vacancy.component.html',

})
export class CandidateVacancyComponent {

}
