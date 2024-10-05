import { Component, inject } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { TuiButton, TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiCheckbox } from '@taiga-ui/kit';
import { TuiInputModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { CandidatesClient } from '../../web-api-client';

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
  ],
  templateUrl: './job-by-status-table.component.html',
})
export class JobByStatusTableComponent {
  apiClient = inject(CandidatesClient);
}
