import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { UserDashboardComponent } from './pages/user-dashboard/user-dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardHomeUserComponent } from './components/dashboard-home-user/dashboard-home-user.component';
import { ReportListComponent } from './components/report-list/report-list.component';
import { UserSettingsComponent } from './components/user-settings/user-settings.component';
import { FooterComponent } from './components/footer/footer.component';
import { MedicationsComponent } from './components/medications/medications.component';
import { AboutComponent } from './pages/about/about.component';
import { SearchDoctorComponent } from './components/search-doctor/search-doctor.component';
import { DoctorDashboardComponent } from './pages/doctor-dashboard/doctor-dashboard.component';
import { PatientListComponent } from './components/patient-list/patient-list.component';
import { PatientDetailComponent } from './components/patient-detail/patient-detail.component';
import { MedicalConditionComponent } from './components/medical-condition/medical-condition.component';
import { ExamplePdfViewerComponent } from './example-pdf-viewer/example-pdf-viewer.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    UserDashboardComponent,
    SidebarComponent,
    DashboardHomeUserComponent,
    ReportListComponent,
    UserSettingsComponent,
    FooterComponent,
    MedicationsComponent,
    AboutComponent,
    SearchDoctorComponent,
    DoctorDashboardComponent,

    PatientListComponent,
    PatientDetailComponent,
    MedicalConditionComponent,
    ExamplePdfViewerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxExtendedPdfViewerModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
