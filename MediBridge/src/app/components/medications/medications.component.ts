import { MedicalCondition } from './../../classes/medical-condition';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthService } from 'src/app/auth.service';
import { Medications } from 'src/app/classes/medications';
import { TherapeuticPlan } from 'src/app/classes/therapeutic-plan';
import { Flowbite } from 'src/app/flowbite.decorator';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';

@Component({
  selector: 'app-medications',
  templateUrl: './medications.component.html',
  styleUrls: ['./medications.component.scss'],
})
@Flowbite()
export class MedicationsComponent implements OnInit {
  medications!: Medications[] | null;
  therapeuticPlans: TherapeuticPlan[] | null = null;
  medicalConditions: MedicalCondition[] | null = null;

  addMedicationForm!: FormGroup;

  constructor(
    private authSvc: AuthService,
    private userDataSvc: UserDataService,
    private fb: FormBuilder
  ) {}
  setTherapeuticPlans(plans: TherapeuticPlan[] | null) {
    this.therapeuticPlans = plans;
    console.log('tplan in medication', this.therapeuticPlans);
  }
  setMedicalConditions(conditions: MedicalCondition[] | null) {
    this.medicalConditions = conditions;
    console.log('med cond in medication', this.medicalConditions);
  }
  ngOnInit(): void {
    this.getMedication(this.authSvc.userUID);

    this.addMedicationForm = this.fb.group({
      medicationName: this.fb.control(null, [Validators.required]),
      dailyDosage: this.fb.control(null, [Validators.required]),
      medicalCondition: this.fb.control(null, [Validators.required]),
      planID: this.fb.control(null),
    });
  }

  getMedication(id: string) {
    this.userDataSvc.getMedications(id).subscribe((res) => {
      this.medications = res;
      console.log('medication', this.medications);
    });
  }

  addMedicaiton() {
    let planId = this.addMedicationForm.value.planID;

    let medication = new Medications(
      this.addMedicationForm.value.medicationName,
      this.addMedicationForm.value.dailyDosage,
      this.addMedicationForm.value.medicalCondition,
      planId
    );
    console.log('medication', medication);

    this.userDataSvc
      .addMedication(this.authSvc.userUID, medication)
      .subscribe((res) => {
        this.getMedication(this.authSvc.userUID);
      });
  }
}
