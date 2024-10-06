import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TuiSurface, TuiTitle } from '@taiga-ui/core';
import { TuiDataListWrapper, TuiFilterByInputPipe } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiInputModule } from '@taiga-ui/legacy';

@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [TuiCardLarge, TuiSurface, ReactiveFormsModule, TuiDataListWrapper, TuiFilterByInputPipe, TuiInputModule, TuiTitle, TuiHeader],
  templateUrl: './job-card.component.html',
  styles: ``,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JobCardComponent {

}
