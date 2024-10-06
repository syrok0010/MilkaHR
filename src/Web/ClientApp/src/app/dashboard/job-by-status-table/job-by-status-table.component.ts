import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
} from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { TuiButton, TuiScrollbar, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiCheckbox } from '@taiga-ui/kit';
import { TuiInputModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { CandidatesClient } from '../../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  TuiComparator,
  TuiTableCell,
  TuiTableDirective,
  TuiTableTbody,
  TuiTableTd,
  TuiTableTh,
  TuiTableThGroup,
  TuiTableTr,
} from '@taiga-ui/addon-table';
import { tuiDefaultSort } from '@taiga-ui/cdk';
import { WaIntersectionRoot } from '@ng-web-apis/intersection-observer';

@Component({
  selector: 'app-job-by-status-table',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    TuiButton,
    TuiCardLarge,
    TuiCheckbox,
    TuiHeader,
    TuiInputModule,
    TuiSurface,
    TuiTextfieldControllerModule,
    TuiTitle,
    TuiTableTbody,
    TuiTableTh,
    TuiTableThGroup,
    TuiTableDirective,
    TuiTableTd,
    TuiTableTr,
    TuiTableCell,
    WaIntersectionRoot,
    TuiScrollbar,
  ],
  templateUrl: './job-by-status-table.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobByStatusTableComponent {
  apiClient = inject(CandidatesClient);
  data = toSignal(this.apiClient.getApiCandidatesCandidatesByStatusByJob(6));
  dataProcessed = computed(() =>
    Object.entries(this.data()).map(([key, value]) => [key, ...value]),
  );
  columns: ReadonlyArray<string> = [
    'job',
    'cvCreated',
    'cvApproved',
    'interviewScheduled',
    'interviewCompleted',
    'hired',
    'denied',
  ];
}
