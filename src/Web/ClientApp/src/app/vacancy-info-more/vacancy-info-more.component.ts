import { Component } from '@angular/core';
import { TuiSurface } from '@taiga-ui/core';
import { TuiTile } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';

@Component({
  selector: 'app-vacancy-info-more',
  standalone: true,
  imports: [TuiCardLarge,TuiSurface,TuiHeader,TuiTile],
  templateUrl: './vacancy-info-more.component.html',

})
export class VacancyInfoMoreComponent {

}
