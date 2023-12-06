import { AuthService } from './../../auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Doctor } from 'src/app/classes/doctor';
import { MedicalCondition } from 'src/app/classes/medical-condition';
import { Medications } from 'src/app/classes/medications';
import { MyReport } from 'src/app/classes/my-report';
import { PatientDoctor } from 'src/app/classes/patient-doctor';
import { PswChange } from 'src/app/classes/psw-change';
import { ReportForm } from 'src/app/classes/report-form';
import { TherapeuticPlan } from 'src/app/classes/therapeutic-plan';
import { User } from 'src/app/classes/user';

@Injectable({
  providedIn: 'root',
})
export class UserDataService {
  apiUrl: string = 'https://localhost:44327';

  getReportUrl: string = this.apiUrl + '/api/reports/';
  reportPostUrl: string = this.apiUrl + '/api/report';

  getMedicalConditinUrl: string = this.apiUrl + '/api/MedicalConditions/';
  postMedicalConditionUrl: string = this.apiUrl + '/api/MedicalCondition';

  getTherapeuticPlansUrl: string = this.apiUrl + '/api/TherapeuticPlans/';
  addTherapeuticPlanUrl: string = this.apiUrl + '/api/therapeuticPlan';

  getMedicationsUrl: string = this.apiUrl + '/api/Medications/';
  addMedicationUrl: string = this.apiUrl + '/api/Medications/';

  getUserByIdUrl: string = this.apiUrl + '/api/Users/';
  putUserUrl: string = this.apiUrl + '/api/Users/';
  changePswUrl: string = this.apiUrl + '/api/changePassword/';

  getPatientDoctorByPatientIdUrl: string =
    this.apiUrl + '/api/getPatientDoctorByUserId/';
  addPatientDoctorUrl: string = this.apiUrl + '/api/PatientDoctors';

  constructor(private http: HttpClient) {}

  getReport(id: string) {
    return this.http.get<[MyReport]>(this.getReportUrl + id);
  }
  addReport(report: ReportForm) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<ReportForm>(this.reportPostUrl, report, httpOptions);
  }

  getMedicalCondition(id: string) {
    return this.http.get<[MedicalCondition]>(this.getMedicalConditinUrl + id);
  }

  addMedicalCondition(condition: MedicalCondition) {
    return this.http.post<MedicalCondition>(
      this.postMedicalConditionUrl,
      condition
    );
  }
  getTherapeuticPlans(id: string) {
    return this.http.get<[TherapeuticPlan]>(this.getTherapeuticPlansUrl + id);
  }

  addTherapeuticPlans(id: string, tPlan: TherapeuticPlan) {
    return this.http.post<TherapeuticPlan>(this.addTherapeuticPlanUrl, tPlan);
  }

  getMedications(id: string) {
    return this.http.get<[Medications]>(this.getMedicationsUrl + id);
  }

  addMedication(id: string, medication: Medications) {
    return this.http.post<Medications>(this.addMedicationUrl, medication);
  }

  editUser(user: User, id: string) {
    return this.http.put<User>(this.putUserUrl + id, user);
  }

  changePassword(passwordDto: PswChange, id: string) {
    return this.http.patch<PswChange>(this.changePswUrl + id, passwordDto);
  }

  searchDoctor(query: string) {
    const Url = `${this.apiUrl}/api/doctors/search?query=${query}`;
    console.log(Url);

    return this.http.get<Doctor[]>(Url);
  }

  getPatientDoctor(userId: string) {
    return this.http.get<PatientDoctor[]>(
      this.getPatientDoctorByPatientIdUrl + userId
    );
  }

  linkWhitDoctor(paitentDoctor: PatientDoctor) {
    return this.http.post<PatientDoctor>(
      this.addPatientDoctorUrl,
      paitentDoctor
    );
  }
}
