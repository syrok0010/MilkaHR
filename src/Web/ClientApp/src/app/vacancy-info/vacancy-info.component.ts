import { Component } from '@angular/core';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { tuiInputDateOptionsProvider } from '@taiga-ui/kit';

import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputDateModule, TuiInputModule } from '@taiga-ui/legacy';

@Component({
  selector: 'app-vacancy-info',
  standalone: true,
  imports: [TuiTitle,TuiCardLarge,TuiSurface,TuiHeader,TuiInputModule,TuiInputDateModule],
  templateUrl: './vacancy-info.component.html',
})
export class VacancyInfoComponent {

}
