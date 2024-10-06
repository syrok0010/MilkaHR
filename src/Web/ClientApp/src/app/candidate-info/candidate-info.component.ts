import { Component, inject, input, OnInit } from '@angular/core';
import {
  TuiAlertService,
  TuiButton,
  TuiSurface,
  TuiTitle,
} from '@taiga-ui/core';
import { TuiBadge } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputDateModule, TuiInputModule } from '@taiga-ui/legacy';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TuiDay } from '@taiga-ui/cdk';
import {
  Candidate,
  CandidatesClient,
  UpdateCandidateByIdCommand,
} from '../web-api-client';
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
    TuiButton,
  ],
  templateUrl: './candidate-info.component.html',
})
export class CandidateInfoComponent implements OnInit {
  candidate: Candidate;
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
  private readonly alerts = inject(TuiAlertService);
  candidateId = input.required<number>();

  async ngOnInit() {
    const candidate = await firstValueFrom(
      this.apiClient.getCandidate(this.candidateId()),
    );
    this.candidate = candidate;
    this.form.setValue({
      name: candidate.name,
      lastName: candidate.lastName,
      middleName: candidate.middleName,
      email: candidate.email,
      phone: candidate.phone,
      address: candidate.address,
      education: candidate.education,
      tags: candidate.tags,
      lastJob: candidate.lastJob,
      birthDate: TuiDay.fromUtcNativeDate(new Date(candidate.birthDate)),
    });
  }

  async save() {
    await firstValueFrom(
      this.apiClient.updateCandidate(
        this.candidateId(),
        new UpdateCandidateByIdCommand({
          id: this.candidate.id,
          name: this.form.value.name,
          middleName: this.form.value.middleName,
          lastName: this.form.value.lastName,
          email: this.form.value.email,
          phone: this.form.value.phone,
          address: this.form.value.address,
          workExperience: this.candidate.workExperience,
          lastJob: this.form.value.lastJob,
          education: this.form.value.education,
          photo: this.candidate.photo,
          cvs: this.candidate.cvs,
        }),
      ),
    );
    this.alerts
      .open('Успешно сохранено', { appearance: 'success' })
      .subscribe();
  }
}
