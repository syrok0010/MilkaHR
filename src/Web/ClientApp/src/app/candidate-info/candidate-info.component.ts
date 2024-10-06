import { Component, inject, input, OnInit } from '@angular/core';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiBadge } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputDateModule, TuiInputModule } from '@taiga-ui/legacy';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TuiDay } from '@taiga-ui/cdk';
import { CandidatesClient } from '../web-api-client';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-candidate-info',
  standalone: true,
  imports: [
    TuiInputModule,
    TuiHeader,
    TuiHeader,
    TuiTitle,
    TuiCardLarge,
    TuiSurface,
    TuiBadge,
    ReactiveFormsModule,
    TuiInputDateModule,
  ],
  templateUrl: './candidate-info.component.html',
})
export class CandidateInfoComponent implements OnInit {
  form = new FormGroup({
    lastName: new FormControl<string>(''),
    name: new FormControl<string>(''),
    middleName: new FormControl<string>(''),
    email: new FormControl<string>(''),
    phone: new FormControl<string>(''),
    birthDate: new FormControl<TuiDay>(null),
    address: new FormControl<string>(''),
    education: new FormControl<string>(''),
    tags: new FormControl<string[]>([]),
    lastJob: new FormControl<string>(''),
  });

  apiClient = inject(CandidatesClient);
  candidateId = input.required<number>();

  async ngOnInit() {
    const candidate = await firstValueFrom(
      this.apiClient.getCandidate(this.candidateId()),
    );
    console.log(candidate.birthDate)
    this.form.setValue({
      name: candidate.name,
      lastName: candidate.lastName,
      middleName: candidate.middleName,
      email: candidate.middleName,
      phone: candidate.phone,
      address: candidate.address,
      education: candidate.education,
      tags: candidate.tags,
      lastJob: candidate.lastJob,
      birthDate: TuiDay.fromUtcNativeDate(new Date(candidate.birthDate)),
    });
  }
}
