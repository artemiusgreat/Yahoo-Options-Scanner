import { AfterContentInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbDateStruct, NgbDatepicker, NgbInputDatepicker } from '@ng-bootstrap/ng-bootstrap';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';
import { HostListener } from '@angular/core';
import { ElementRef } from '@angular/core';

@Component({
  selector: 'app-selectors-date',
  templateUrl: './date.component.html',
  styleUrls: ['./date.component.scss']
})

export class SelectorsDateComponent extends SelectorsControlComponent {

  @ViewChild('dateControl') calendar: NgbInputDatepicker;

  public controlName: string;
  public controlValue: NgbDateStruct;

  @HostListener('document:click', ['$event'])
  onClick(event) {
    if (this.reference.nativeElement.contains(event.target) == false) {
      this.calendar.close();
    }
  }

  constructor(public reference: ElementRef) {
    super();
  }

  onShowCalendar(event: any) {
    this.calendar.open();
  }

  onSelectDate(event: any) {

    let data = o => o < 10 ? '0' + o : o;
    let date = data(event.year) + '-' + data(event.month) + '-' + data(event.day);

    this.addValue(date, date);
  }
}