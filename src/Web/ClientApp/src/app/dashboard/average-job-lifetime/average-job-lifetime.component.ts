import { Component, computed, inject, Inject, signal } from '@angular/core';
import { DatePipe, NgForOf } from '@angular/common';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { JobsClient } from '../../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { TuiLegendItem, TuiPieChart } from '@taiga-ui/addon-charts';
import { TuiHovered } from '@taiga-ui/cdk';

@Component({
  selector: 'app-average-job-lifetime',
  standalone: true,
  imports: [
    DatePipe,
    TuiCardLarge,
    TuiHeader,
    TuiSurface,
    TuiTitle,
    TuiPieChart,
    TuiLegendItem,
    TuiHovered,
    NgForOf,
  ],
  templateUrl: './average-job-lifetime.component.html',
})
export class AverageJobLifetimeComponent {
  apiClient = inject(JobsClient);
  data = signal({ Разработка: 5, Тестирование: 10 });
  //data = toSignal(this.apiClient.getAverageJobLifetime())
  values = computed(() => Object.values(this.data()));
  labels = computed(() => Object.keys(this.data()));
  activeItemIndex = signal(NaN);

  protected isItemActive(index: number): boolean {
    return this.activeItemIndex() === index;
  }

  protected onHover(index: number, hovered: boolean): void {
    this.activeItemIndex.set(hovered ? index : 0);
  }

  protected readonly JSON = JSON;
}
