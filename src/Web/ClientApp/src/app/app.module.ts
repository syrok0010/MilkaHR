import { NG_EVENT_PLUGINS } from '@taiga-ui/event-plugins';
import { TuiRoot } from '@taiga-ui/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SidebarComponent } from './sidebar/sidebar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ApplicantsPageComponent } from './applicants-page/applicants-page.component';
import { JobsPageComponent } from './jobs-page/jobs-page.component';
import { CandidatPageComponent } from './candidat-page/candidat-page.component';
import { VacancyPageComponent } from './vacancy-page/vacancy-page.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: '',
        component: DashboardComponent,
      },
      {
        path: 'applicants',
        component: ApplicantsPageComponent,
      },
      {
        path: 'jobs',
        component: JobsPageComponent,
      },
      {
        path: 'candidate/:id',
        component: CandidatPageComponent,
      },
      {
        path: 'vacancy/:id',
        component: VacancyPageComponent,
      },
    ]),
    BrowserAnimationsModule,
    TuiRoot,
    SidebarComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    NG_EVENT_PLUGINS,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
