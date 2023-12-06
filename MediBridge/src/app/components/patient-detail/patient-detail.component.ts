import { Patient } from '../../classes/patient';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MedicalCondition } from 'src/app/classes/medical-condition';
import { TherapeuticPlan } from 'src/app/classes/therapeutic-plan';
import { MedicationsComponent } from '../medications/medications.component';
import { AuthService } from 'src/app/auth.service';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';
import { Medications } from 'src/app/classes/medications';
import { DoctorDataService } from 'src/app/pages/doctor-dashboard/doctor-data.service';
import { MyReport } from 'src/app/classes/my-report';
import { Flowbite } from 'src/app/flowbite.decorator';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss'],
})
@Flowbite()
export class PatientDetailComponent implements OnChanges, OnInit {
  MedicalConditions: [MedicalCondition] | null = null;
  TherapeuticPlans: [TherapeuticPlan] | null = null;
  medications!: Medications[] | null;
  reports!: MyReport[] | null;
  Patient!: Patient;

  selectedPatientId!: string;
  fileSelected!: string;

  @ViewChild(MedicationsComponent) medicationsComponent!: MedicationsComponent;
  @Input() patientId: number | undefined;

  constructor(
    private authSvc: AuthService,
    private userDataSvc: UserDataService,
    private DoctorDataSvc: DoctorDataService
  ) {}
  ngOnChanges(changes: SimpleChanges): void {
    if ('patientId' in changes) {
      // Il valore dell'input 'patientId' è cambiato
      const newPatientId = changes['patientId'].currentValue;
      this.selectedPatientId = newPatientId;
      console.log('onchange', newPatientId);

      this.getPatientData(this.selectedPatientId);
      this.getMedConditions(this.selectedPatientId);
      this.getTPlans(this.selectedPatientId);
      this.getMedication(this.selectedPatientId);
      this.getReport(this.selectedPatientId);

      // Puoi fare qualcosa con il nuovo valore di 'patientId'
      console.log('Nuovo valore di patientId:', newPatientId);
    }
  }

  ngOnInit(): void {}

  getPatientData(id: string) {
    this.DoctorDataSvc.getPatient(id).subscribe((data) => {
      this.Patient = data;
      console.log('get patient in patient detail', this.Patient);
    });
  }

  getMedConditions(id: string) {
    console.log(id);

    this.userDataSvc.getMedicalCondition(id).subscribe((conditions) => {
      console.log(conditions);
      this.MedicalConditions = conditions;
    });
  }

  getTPlans(id: string) {
    this.userDataSvc.getTherapeuticPlans(id).subscribe((plans) => {
      console.log(plans);
      this.TherapeuticPlans = plans;
    });
  }

  getMedication(id: string) {
    this.userDataSvc.getMedications(id).subscribe((res) => {
      this.medications = res;
      console.log(res);
    });
  }

  getReport(id: string) {
    this.userDataSvc.getReport(id).subscribe((res) => {
      console.log(res);
      this.reports = res;
    });
  }

  getFile(filename: string) {
    const encodedFilename = this.removePdfExtension(filename);
    console.log('nome codificato', encodedFilename);

    this.DoctorDataSvc.getFile64(
      this.selectedPatientId,
      encodedFilename
    ).subscribe((data) => {
      this.fileSelected = data.file64;
      console.log('bindfile', this.fileSelected);
    });
  }

  removePdfExtension(filename: string): string {
    // Verifica se la stringa termina con ".pdf" e rimuovilo
    if (filename.toLowerCase().endsWith('.pdf')) {
      return filename.slice(0, -4); // Rimuovi gli ultimi 4 caratteri (".pdf")
    }
    return filename;
  }
}
