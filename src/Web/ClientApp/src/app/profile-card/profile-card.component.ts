import {NgForOf} from '@angular/common';
import {ChangeDetectionStrategy, Component} from '@angular/core';
import {TuiTagModule} from '@taiga-ui/legacy';
import {TuiButton, TuiTitle, TuiSurface, TuiOption} from '@taiga-ui/core';
import { TuiCardLarge } from '@taiga-ui/layout';
import { TuiBadge } from '@taiga-ui/kit';

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [NgForOf, TuiButton, TuiCardLarge, TuiSurface, TuiTitle, TuiOption, TuiTagModule, TuiBadge],
  templateUrl: './profile-card.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileCardComponent {
  protected tags: readonly string[] = [
    'Taiga UI',
    'is an open-source library',
    'for awesome people',
];

protected handleTagEdited(newCaption: string, currentIndex: number): void {
    this.tags = this.tags
        .map((caption, index) => (index === currentIndex ? newCaption : caption))
        .filter(Boolean);
}
}
