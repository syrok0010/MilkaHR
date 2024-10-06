import { Component } from '@angular/core';
import { TuiButton, TuiSurface } from '@taiga-ui/core';
import { TuiTile } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiTextareaModule } from '@taiga-ui/legacy';

@Component({
  selector: 'app-vacancy-info-more',
  standalone: true,
  imports: [
    TuiCardLarge,
    TuiSurface,
    TuiHeader,
    TuiTile,
    TuiTextareaModule,
    TuiButton,
  ],
  templateUrl: './vacancy-info-more.component.html',
})
export class VacancyInfoMoreComponent {}
