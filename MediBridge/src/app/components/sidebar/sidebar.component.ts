import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { initFlowbite } from 'flowbite';
import { Flowbite } from 'src/app/flowbite.decorator';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
@Flowbite()
export class SidebarComponent implements OnInit {
  ngOnInit(): void {}
  @Output() reportListEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() dasboard: EventEmitter<void> = new EventEmitter<void>();
  @Output() userSettings: EventEmitter<void> = new EventEmitter<void>();
  @Output() searchDoctor: EventEmitter<void> = new EventEmitter<void>();
  @Output() logout: EventEmitter<void> = new EventEmitter<void>();

  showReportList() {
    this.reportListEmitter.emit();
  }

  showDasboard() {
    this.dasboard.emit();
  }

  showUserSettings() {
    this.userSettings.emit();
  }

  showSearchDoctor() {
    this.searchDoctor.emit();
  }
  emitLogout() {
    this.logout.emit();
  }
}
