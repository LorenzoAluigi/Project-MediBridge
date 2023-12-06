import { TherapeuticPlan } from './../../classes/therapeutic-plan';
import { AuthService } from 'src/app/auth.service';
import { MedicalCondition } from './../../classes/medical-condition';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { initFlowbite } from 'flowbite';
import { MedicationsComponent } from '../medications/medications.component';
import { Flowbite } from 'src/app/flowbite.decorator';

@Component({
  selector: 'app-dashboard-home-user',
  templateUrl: './dashboard-home-user.component.html',
  styleUrls: ['./dashboard-home-user.component.scss'],
})
@Flowbite()
export class DashboardHomeUserComponent implements OnInit {
  MedicalConditions: [MedicalCondition] | null = null;
  TherapeuticPlans: [TherapeuticPlan] | null = null;

  medCondForm!: FormGroup;
  TplanForm!: FormGroup;

  @ViewChild(MedicationsComponent) medicationsComponent!: MedicationsComponent;

  constructor(
    private authSvc: AuthService,
    private userDataSvc: UserDataService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getMedConditions();
    this.getTPlans();
    this.TplanForm = this.fb.group({
      expiryDate: this.fb.control(null, [Validators.required]),
      medicalCondition: this.fb.control(null, [Validators.required]),
      planDescription: this.fb.control(null, [Validators.required]),
      startDate: this.fb.control(null, [Validators.required]),
    });
    this.medCondForm = this.fb.group({
      description: this.fb.control(null, [Validators.required]),
      year: this.fb.control(null),
    });
  }

  getMedConditions() {
    this.userDataSvc
      .getMedicalCondition(this.authSvc.userUID)
      .subscribe((conditions) => {
        console.log(conditions);
        this.MedicalConditions = conditions;

        this.passMedConditionToMedicationsComponent();
      });
  }

  addMedCondition() {
    let medCondition = new MedicalCondition(
      this.medCondForm.value.description,
      this.authSvc.userUID,
      this.medCondForm.value.year
    );

    this.userDataSvc.addMedicalCondition(medCondition).subscribe((res) => {
      console.log('resp add med condition', res);
      this.getMedConditions();
    });
  }

  getTPlans() {
    this.userDataSvc
      .getTherapeuticPlans(this.authSvc.userUID)
      .subscribe((plans) => {
        console.log(plans);
        this.TherapeuticPlans = plans;
        console.log('tplan', this.TherapeuticPlans);
        this.passTplanDataToMedicationsComponent();
        console.log('get tplan', this.TherapeuticPlans);
      });
  }

  addTPlan() {
    console.log(this.TplanForm.value.startDate);

    let tPlan = new TherapeuticPlan(
      this.TplanForm.value.medicalCondition,
      this.TplanForm.value.planDescription,
      this.TplanForm.value.startDate,
      this.TplanForm.value.expiryDate,
      this.authSvc.userUID
    );
    console.log('add tplan', tPlan);
    this.userDataSvc
      .addTherapeuticPlans(this.authSvc.userUID, tPlan)
      .subscribe((data) => {
        console.log(data);
      });
  }

  passTplanDataToMedicationsComponent() {
    this.medicationsComponent.setTherapeuticPlans(this.TherapeuticPlans);
  }
  passMedConditionToMedicationsComponent() {
    this.medicationsComponent.setMedicalConditions(this.MedicalConditions);
  }
}
