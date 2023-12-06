import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { initFlowbite } from 'flowbite';
import { AuthService } from 'src/app/auth.service';
import { MyReport } from 'src/app/classes/my-report';
import { ReportForm } from 'src/app/classes/report-form';
import { Flowbite } from 'src/app/flowbite.decorator';
import { UserDataService } from 'src/app/pages/user-dashboard/user-data.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.scss'],
})
@Flowbite()
export class ReportListComponent implements OnInit {
  // form report's attributes
  reportForm!: FormGroup;
  fileName!: string;
  fileByteArray!: Uint8Array;

  reports: MyReport[] | null = null;

  editReportForm!: FormGroup;
  selectedEditItem!: MyReport;

  constructor(
    public authSvc: AuthService,
    private fb: FormBuilder,
    private userDataSvc: UserDataService
  ) {}
  ngOnInit(): void {
    this.reportForm = this.fb.group({
      description: this.fb.control(null, [Validators.required]),
      date: this.fb.control(null, [Validators.required]),
      type: this.fb.control(null, [Validators.required]),
      file: this.fb.control(null, [Validators.required]),
    });

    this.editReportForm = this.fb.group({
      description: this.fb.control(null, [Validators.required]),
      date: this.fb.control(null, [Validators.required]),
      type: this.fb.control(null, [Validators.required]),
      file: this.fb.control(null, [Validators.required]),
    });
    this.getUserReport();
  }

  getUserReport() {
    this.userDataSvc.getReport(this.authSvc.userUID).subscribe((res) => {
      console.log(res);
      this.reports = res;
    });
  }

  // gestione dell'inserimento dei report
  onFileChange(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      this.readFileAsArrayBuffer(event.target.files[0]);
    }
  }

  readFileAsArrayBuffer(file: File) {
    this.fileName = file.name;
    const reader = new FileReader();
    reader.onload = (event) => {
      const arrayBuffer = reader.result as ArrayBuffer;
      this.fileByteArray = new Uint8Array(arrayBuffer);
    };
    reader.readAsArrayBuffer(file);
  }

  submitReport() {
    let report: ReportForm = new ReportForm(
      this.reportForm.value.description,
      this.authSvc.userUID,
      this.reportForm.value.date,
      this.reportForm.value.type,
      this.fileByteArray,
      this.fileName
    );
    console.log(this.reportForm);

    this.userDataSvc.addReport(report).subscribe((res) => {
      console.log(res);
      this.getUserReport();
    });
  }

  openEditModal(report: MyReport) {
    this.selectedEditItem = report;
    this.editReportForm.patchValue({
      description: this.selectedEditItem.description,
      date: this.selectedEditItem.reportDate,
      type: this.selectedEditItem.reportsType,
    });
  }
}
