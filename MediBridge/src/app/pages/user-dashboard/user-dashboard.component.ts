import { ReportForm } from './../../classes/report-form';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth.service';
import { User } from 'src/app/classes/user';
import { UserDataService } from './user-data.service';
import { initFlowbite } from 'flowbite';
import { Flowbite } from 'src/app/flowbite.decorator';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.scss'],
})
@Flowbite()
export class UserDashboardComponent {
  showDashboard: boolean = true;
  showReportList: boolean = false;
  showUserSettings: boolean = false;
  showSearchDoctor: boolean = false;

  toggleReportList() {
    if (!this.showDashboard) {
      this.showReportList = true;
      this.showSearchDoctor = false;
      this.showUserSettings = false;
    } else {
      this.showDashboard = !this.showDashboard;
      this.showReportList = !this.showReportList;
    }
  }

  toggleDashboard() {
    this.showDashboard = true;
    this.showReportList = false;
    this.showUserSettings = false;
    this.showSearchDoctor = false;
  }

  toggleUserSettings() {
    if (!this.showDashboard) {
      this.showReportList = false;
      this.showSearchDoctor = false;
      this.showUserSettings = true;
    } else {
      this.showDashboard = !this.showDashboard;
      this.showUserSettings = !this.showUserSettings;
    }
  }

  toggleSearchDoctor() {
    if (!this.showDashboard) {
      this.showReportList = false;
      this.showSearchDoctor = true;
      this.showUserSettings = false;
    } else {
      this.showDashboard = !this.showDashboard;
      this.showSearchDoctor = !this.showSearchDoctor;
    }
  }

  constructor(
    public authSvc: AuthService,
    private fb: FormBuilder,
    private userDataSvc: UserDataService
  ) {
    this.authSvc.restoreUser();
  }

  ngOnInit(): void {}

  logout() {
    this.authSvc.logout();
  }
}
