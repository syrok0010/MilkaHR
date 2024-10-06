import { Component, inject } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { TuiButton, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiCheckbox } from '@taiga-ui/kit';
import { TuiInputModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { CandidatesClient } from '../../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  TuiTableCell,
  TuiTableDirective,
  TuiTableTbody,
  TuiTableTd,
  TuiTableTh,
  TuiTableThGroup,
  TuiTableTr,
} from '@taiga-ui/addon-table';

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
  ],
  templateUrl: './job-by-status-table.component.html',
})
export class JobByStatusTableComponent {
  apiClient = inject(CandidatesClient);
  //data = toSignal(this.apiClient.getApiCandidatesCandidatesByStatusByJob(), {
  //  initialValue: { Разработчик: [1, 2, 3, 4, 5] },
  //});
  data = { Разработчик: [1, 2, 3, 4, 5, 6] };
  dataProcessed = Object.keys(this.data).map((key) => [key, ...this.data[key]]);
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
