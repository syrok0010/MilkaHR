import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiCardLarge } from '@taiga-ui/layout';
import { TuiSurface } from '@taiga-ui/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, TuiCardLarge, TuiSurface],
  templateUrl: './sidebar.component.html',
})
export class SidebarComponent {}
