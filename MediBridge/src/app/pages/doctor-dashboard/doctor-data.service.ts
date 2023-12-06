import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Injectable } from '@angular/core';
import { Doctor } from 'src/app/classes/doctor';
import { PatientDoctor } from 'src/app/classes/patient-doctor';
import { Patient } from 'src/app/classes/patient';

@Injectable({
  providedIn: 'root',
})
export class DoctorDataService {
  apiUrl: string = 'https://localhost:44327';
  getDoctorUrl: string = this.apiUrl + '/api/Doctor/';
  getPatientsByDoctorUrl: string =
    this.apiUrl + '/api/getPatientDoctorByDoctorId/';
  getPatientUrl: string = this.apiUrl + '/api/getPatient/';
  getfileUrl: string = this.apiUrl + '/api/file/';

  constructor(private authSvc: AuthService, private http: HttpClient) {}

  getDoctorById() {
    return this.http.get<Doctor>(this.getDoctorUrl + this.authSvc.userUID);
  }

  getPatientsByDoctor(doctorID: number) {
    return this.http.get<PatientDoctor[]>(
      this.getPatientsByDoctorUrl + `${doctorID}`
    );
  }
  getPatient(PatientId: string) {
    return this.http.get<Patient>(this.getPatientUrl + PatientId);
  }

  getFile64(id: string, fileName: string) {
    return this.http.get<any>(this.getfileUrl + `${id}/${fileName}`);
  }
}
