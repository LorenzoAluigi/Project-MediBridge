import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-doctor-dashboard',
  templateUrl: './doctor-dashboard.component.html',
  styleUrls: ['./doctor-dashboard.component.scss'],
})
export class DoctorDashboardComponent {
  showUserSettings: boolean = false;
  showPatientsList: boolean = true;
  showPatientDetail: boolean = false;

  selectedPatientId: number | undefined;

  constructor(private authSvc: AuthService) {}
  toggleDashboard() {
    this.showPatientsList = true;
    this.showUserSettings = false;
    this.showPatientDetail = false;
  }

  toggleUserSettings() {
    if (!this.showPatientsList) {
      this.showUserSettings = true;
      this.showPatientDetail = false;
    } else {
      this.showPatientsList = !this.showPatientsList;
      this.showUserSettings = !this.showUserSettings;
      this.showPatientDetail = false;
    }
  }

  handlePatientDetail($event: any): void {
    this.selectedPatientId = $event;
    this.showPatientDetail = true;
    this.showUserSettings = false;
    this.showPatientsList = false;
  }

  logout() {
    this.authSvc.logout();
  }
}
