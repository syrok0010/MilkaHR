import { Component, inject } from '@angular/core';
import { ProfileCardComponent } from '../profile-card/profile-card.component';
import { CandidatesClient } from '../web-api-client';
import { toSignal } from '@angular/core/rxjs-interop';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-applicants-page',
  standalone: true,
  imports: [ProfileCardComponent, NgForOf],
  templateUrl: './applicants-page.component.html',
})
export class ApplicantsPageComponent {
  apiClient = inject(CandidatesClient);
  candidates = toSignal(
    this.apiClient.getAllCandidates(null, null, null, null, null, null),
  );
}
