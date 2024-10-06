import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiCardLarge } from '@taiga-ui/layout';
import { TuiButton, TuiIcon, TuiSurface } from '@taiga-ui/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, TuiCardLarge, TuiSurface, TuiButton, TuiIcon],
  templateUrl: './sidebar.component.html',
})
export class SidebarComponent {}
