import { NgForOf } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { TuiTagModule } from '@taiga-ui/legacy';
import { TuiButton, TuiTitle, TuiSurface, TuiOption } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiBadge } from '@taiga-ui/kit';
import { Candidate } from '../web-api-client';
import { TuiDay } from '@taiga-ui/cdk';

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [
    NgForOf,
    TuiButton,
    TuiCardLarge,
    TuiSurface,
    TuiTitle,
    TuiOption,
    TuiTagModule,
    TuiBadge,
    TuiHeader,
  ],
  templateUrl: './profile-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileCardComponent {
  candidate = input.required<Candidate>();
  protected tags: readonly string[] = [
    'Taiga UI',
    'is an open-source library',
    'for awesome people',
  ];

  age = computed(() => {
    const c = this.candidate();
    const birthDate = TuiDay.fromUtcNativeDate(new Date(c.birthDate));
    const today = TuiDay.currentUtc();
    const daysBetween = TuiDay.lengthBetween(birthDate, today);
    return Math.floor(daysBetween / 365.25);
  });
}
