import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {AdminComponent} from "./components/admin/admin.component";
import {AuthGuard} from "./auth-guard/auth.guard";
import {ScheduleComponent} from "./components/schedule/schedule.component";
import {LessonPageComponent} from "./components/lesson-page/lesson-page.component";

const routes: Routes = [
  {path: '',  component: LoginComponent, pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {
    path: 'admin', component: AdminComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'administrator'
    }
  },
  {path: 'schedule', component: ScheduleComponent},
  {
    path: 'lesson/:id', component: LessonPageComponent,
    canActivate: [AuthGuard],
    data: {
      role: ['administrator', 'teacher', 'student']
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
