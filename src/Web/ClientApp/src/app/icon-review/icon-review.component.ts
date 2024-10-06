import { Component } from '@angular/core';
import { TuiIcon, TuiSurface} from '@taiga-ui/core';
import { TuiTile } from '@taiga-ui/kit';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';

@Component({
  selector: 'app-icon-review',
  standalone: true,
  imports: [TuiIcon,TuiTile,TuiCardLarge,TuiSurface,TuiHeader],
  templateUrl: './icon-review.component.html',

})
export class IconReviewComponent {

}
