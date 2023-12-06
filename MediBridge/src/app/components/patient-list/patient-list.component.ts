import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Doctor } from 'src/app/classes/doctor';
import { PatientDoctor } from 'src/app/classes/patient-doctor';
import { User } from 'src/app/classes/user';
import { Flowbite } from 'src/app/flowbite.decorator';
import { DoctorDataService } from 'src/app/pages/doctor-dashboard/doctor-data.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.scss'],
})
@Flowbite()
export class PatientListComponent implements OnInit {
  Doctor!: Doctor;
  PatientList!: PatientDoctor[];

  @Output() PatientDetailEmitter: EventEmitter<any> = new EventEmitter<any>();

  constructor(private doctorDataSvc: DoctorDataService) {}

  ngOnInit(): void {
    this.getDoctorData();
  }
  showDetail(UserId: number | undefined) {
    console.log(UserId);

    if (UserId !== undefined) {
      this.PatientDetailEmitter.emit(UserId);
    }
  }

  getDoctorData() {
    this.doctorDataSvc.getDoctorById().subscribe((data) => {
      this.Doctor = data;
      console.log('doctor data', this.Doctor);

      this.getPatients(this.Doctor.DoctorId);
    });
  }

  getPatients(id: number) {
    this.doctorDataSvc.getPatientsByDoctor(id).subscribe((data) => {
      this.PatientList = data;
      console.log('get patient data', this.PatientList);
    });
  }
}
