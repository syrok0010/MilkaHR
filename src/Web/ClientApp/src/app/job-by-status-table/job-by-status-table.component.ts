import {
  CdkFixedSizeVirtualScroll,
  CdkVirtualForOf,
  CdkVirtualScrollViewport,
} from '@angular/cdk/scrolling';
import {ChangeDetectionStrategy, Component} from '@angular/core';
import type {TuiComparator} from '@taiga-ui/addon-table';
import {TuiTable} from '@taiga-ui/addon-table';
import {TuiDay, tuiToInt} from '@taiga-ui/cdk';
import {TuiScrollable, TuiScrollbar} from '@taiga-ui/core';

@Component({
  selector: 'app-job-by-status-table',
  standalone: true,
  imports: [
    CdkFixedSizeVirtualScroll,
    CdkVirtualForOf,
    CdkVirtualScrollViewport,
    TuiScrollable,
    TuiScrollbar,
    TuiTable,
],
  templateUrl: './job-by-status-table.component.html',
  styles: ``
})
export class JobByStatusTableComponent {

}
