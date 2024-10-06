import { Component, input } from '@angular/core';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiBadge } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputModule } from '@taiga-ui/legacy';


@Component({
  selector: 'app-candidate-info',
  standalone: true,
  imports: [TuiInputModule, TuiHeader, TuiHeader, TuiTitle, TuiCardLarge, TuiSurface, TuiBadge],
  templateUrl: './candidate-info.component.html',

})
export class CandidateInfoComponent {

}
