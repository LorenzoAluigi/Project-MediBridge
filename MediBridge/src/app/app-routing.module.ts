import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { UserDashboardComponent } from './pages/user-dashboard/user-dashboard.component';
import { authGuard } from './auth.guard';
import { AboutComponent } from './pages/about/about.component';
import { DoctorDashboardComponent } from './pages/doctor-dashboard/doctor-dashboard.component';
import { roleGuard } from './role.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'userDashboard',
    component: UserDashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: 'about',
    component: AboutComponent,
  },
  {
    path: 'doctorDashboard',
    component: DoctorDashboardComponent,
    canActivate: [authGuard, roleGuard],
    data: { expectedRole: 'Doctor' },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
