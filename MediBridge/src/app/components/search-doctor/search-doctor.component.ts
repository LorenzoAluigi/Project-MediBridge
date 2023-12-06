import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { initFlowbite } from 'flowbite';
import { AuthService } from 'src/app/auth.service';
import { Doctor } from 'src/app/classes/doctor';
import { PatientDoctor } from 'src/app/classes/patient-doctor';
import { Flowbite } from 'src/app/flowbite.decorator';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';

@Component({
  selector: 'app-search-doctor',
  templateUrl: './search-doctor.component.html',
  styleUrls: ['./search-doctor.component.scss'],
})
@Flowbite()
export class SearchDoctorComponent implements OnInit {
  searchForm!: FormGroup;

  searchResult!: Doctor[];

  doctorLink!: PatientDoctor[];

  constructor(
    private userDataSvc: UserDataService,
    private fb: FormBuilder,
    private authSvc: AuthService
  ) {}
  ngOnInit(): void {
    this.searchForm = this.fb.group({
      query: this.fb.control(null),
    });
    this.getPatientDoctor();
  }

  search() {
    let queryString = this.searchForm.value.query;

    console.log(queryString);

    if (queryString.length >= 3) {
      this.userDataSvc.searchDoctor(queryString).subscribe((data) => {
        this.searchResult = [];
        this.searchResult = data;
        console.log(this.searchResult);
      });
    } else {
      this.searchResult = [];
    }
  }

  getPatientDoctor() {
    console.log('get patient doctor');

    this.userDataSvc
      .getPatientDoctor(this.authSvc.userUID)
      .subscribe((data) => {
        console.log(data);
        this.doctorLink = data;
        console.log(data);
      });
  }

  linkDoctor(DoctorId: number) {
    console.log(DoctorId);

    let patientDoctor = new PatientDoctor(DoctorId, this.authSvc.userUID);
    console.log(patientDoctor);

    this.userDataSvc.linkWhitDoctor(patientDoctor).subscribe((data) => {
      console.log(data);
    });
  }
}
