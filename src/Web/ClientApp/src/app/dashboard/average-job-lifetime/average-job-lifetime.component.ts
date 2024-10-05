import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';

@Component({
  selector: 'app-average-job-lifetime',
  standalone: true,
  imports: [DatePipe, TuiCardLarge, TuiHeader, TuiSurface, TuiTitle],
  templateUrl: './average-job-lifetime.component.html',
})
export class AverageJobLifetimeComponent {}
