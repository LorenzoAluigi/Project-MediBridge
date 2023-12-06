import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';
import { AuthService } from './../../auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { initFlowbite } from 'flowbite';
import { User } from 'src/app/classes/user';
import { PswChange } from 'src/app/classes/psw-change';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss'],
})
export class UserSettingsComponent implements OnInit {
  userForm!: FormGroup;
  user: User | null = null;
  changePasswordForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authSvc: AuthService,
    private userDataSvc: UserDataService
  ) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      gender: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      cf: [null, [Validators.required]],
      address: [null, [Validators.required]],
      city: [null, [Validators.required]],
      province: [null, [Validators.required]],
      country: [null, [Validators.required]],
      email: [null, [Validators.required]],
    });

    this.changePasswordForm = this.fb.group({
      CurrentPsw: [null, [Validators.required]],
      NewPsw: [null, [Validators.required]],
      ConfirmPsw: [null, Validators.required],
    });

    this.authSvc.isLoggedIn$.subscribe((isLoggedIn) => {
      if (isLoggedIn) {
        this.authSvc.getUserData(this.authSvc.userUID).subscribe((userData) => {
          this.user = userData;

          this.userForm.patchValue({
            firstName: this.user?.FirstName,
            lastName: this.user?.LastName,
            gender: this.user?.Gender,
            dateOfBirth: this.formatDate(this.user?.DateOfBirth),
            cf: this.user?.CF,
            address: this.user?.Address,
            city: this.user?.City,
            province: this.user?.Province,
            country: this.user?.Country,
            email: this.user?.Email,
          });
        });
      } else {
        this.user = null;
      }
    });
  }

  formatDate(dateString: string): string {
    if (!dateString) {
      return '';
    }
    const date = new Date(dateString);
    const year = date.getFullYear().toString().padStart(4, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  editUser() {
    const updatedUser: User = {
      UserId: this.authSvc.userUID,
      FirstName: this.userForm.get('firstName')?.value || '',
      LastName: this.userForm.get('lastName')?.value || '',
      Gender: this.userForm.get('gender')?.value || '',
      DateOfBirth:
        this.formatDate(this.userForm.get('dateOfBirth')?.value) || '',
      CF: this.userForm.get('cf')?.value || '',
      Address: this.userForm.get('address')?.value || '',
      City: this.userForm.get('city')?.value || '',
      Province: this.userForm.get('province')?.value || '',
      Country: this.userForm.get('country')?.value || '',
      Email: this.userForm.get('email')?.value || '',
    };
    this.userDataSvc
      .editUser(updatedUser, this.authSvc.userUID)
      .subscribe((data) => {
        console.log(data);
      });
  }
  changePassword() {
    console.log('change psw start');

    let currentPassword = this.changePasswordForm.get('CurrentPsw')?.value;
    let newPassword = this.changePasswordForm.get('NewPsw')?.value;
    let confirmPassword = this.changePasswordForm.get('ConfirmPsw')?.value;
    if (newPassword !== currentPassword && newPassword === confirmPassword) {
      let changePswDto = new PswChange(currentPassword, newPassword);
      this.userDataSvc
        .changePassword(changePswDto, this.authSvc.userUID)
        .subscribe((data) => {
          console.log(data);
        });
    }
  }
}
