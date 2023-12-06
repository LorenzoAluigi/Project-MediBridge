import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { AuthService } from 'src/app/auth.service';
import { User } from 'src/app/classes/user';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  isDarkMode: boolean = false;
  role!: string[] | undefined;
  user!: User | null;

  constructor(
    private authSvc: AuthService,
    private userDataSvc: UserDataService
  ) {}

  logout() {
    this.authSvc.logout();
  }

  ngOnInit() {
    this.authSvc.isLoggedIn$.subscribe((isLoggedIn) => {
      if (isLoggedIn) {
        this.authSvc.getUserData(this.authSvc.userUID).subscribe((userData) => {
          this.user = userData;
          console.log('navbar', this.user);
          this.role = userData.roles;
        });
      } else {
        this.user = null;
      }
    });
    this.isDarkMode = localStorage.getItem('isDarkMode') === 'true';
    if (localStorage.getItem('isDarkMode') === 'true') {
      document.body.classList.add('dark');
    }
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
    if (this.isDarkMode) {
      localStorage.setItem('isDarkMode', 'true');
      document.body.classList.add('dark'); // Applica la dark mode al corpo del documento
    } else {
      localStorage.setItem('isDarkMode', 'false');
      if (!this.isDarkMode) {
        document.body.classList.remove('dark');
      }
    }
  }
}
