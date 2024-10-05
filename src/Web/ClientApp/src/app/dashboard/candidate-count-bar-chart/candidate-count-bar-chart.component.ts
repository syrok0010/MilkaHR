import { Component } from '@angular/core';
import {
  TUI_ALWAYS_DASHED,
  TUI_ALWAYS_NONE,
  TuiAxes,
  TuiBarChart,
} from '@taiga-ui/addon-charts';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';

@Component({
  selector: 'app-candidate-count-bar-chart',
  standalone: true,
  imports: [
    TuiCardLarge,
    TuiSurface,
    TuiHeader,
    TuiTitle,
    TuiAxes,
    TuiBarChart,
  ],
  templateUrl: './candidate-count-bar-chart.component.html',
})
export class CandidateCountBarChartComponent {
  protected readonly horizontalLinesHandler = TUI_ALWAYS_DASHED;
  protected readonly verticalLinesHandler = TUI_ALWAYS_NONE;
}
