import { AfterContentInit, Component, OnInit, ViewChild, ViewEncapsulation, Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbDateStruct, NgbDatepicker, NgbInputDatepicker, NgbPopover } from '@ng-bootstrap/ng-bootstrap';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
    selector: 'app-selectors-daterange',
    templateUrl: './date-range.component.html',
    styleUrls: ['./date-range.component.scss']
})

export class SelectorsDateRangeComponent extends SelectorsControlComponent {

    @ViewChild('popover') public popover: NgbPopover;
    @ViewChild('dateControl') calendar: NgbInputDatepicker;

    controlName: string;
    controlValue: NgbDateStruct;
    controlRange: number[] = [];

    controlOptions: any = {
        '': 'Date',
        'recent': 'Last 24 hours',
        'week': 'Last week',
        'month': 'Last month',
        'year': 'Last year',
        'custom': 'Custom range'
    };

    onDateChange(value: NgbDateStruct) {

        const pointer = this;

        if (value) {

            const date = pointer.modelToStamp(value);

            switch (pointer.controlRange.length) {

                case 2:
                    pointer.controlRange = [];
                    pointer.controlRange[0] = date;
                    break;

                case 1:
                    const v = pointer.controlRange[0];
                    pointer.controlRange[0] = Math.min(date, v);
                    pointer.controlRange[1] = Math.max(date, v);
                    break;

                case 0:
                    pointer.controlRange[0] = date;
                    break;
            }

            if (pointer.controlRange.length == 2) {

                pointer.addValue(
                    pointer.stampToString(pointer.controlRange[0]) + ' - ' + pointer.stampToString(pointer.controlRange[1]),
                    pointer.stampToString(pointer.controlRange[0]) + ' - ' + pointer.stampToString(pointer.controlRange[1])
                );

                pointer.popover.close();
            }
        }

        return false;
    }

    ngOnInit() {

        const pointer = this;

        pointer.controlName = '_' + pointer.name;
        pointer.group.addControl(pointer.controlName, new FormControl(pointer.controlValue, []));
    }

    /**
     * Show date picker and assign click event handler to update FB on change
     * @param event
     */
    showCalendar(event: any) {

        const pointer = this;

        pointer.controlRange = [];
        pointer.controlValue = null;
        pointer.popover.toggle();
    }

    /**
     * Convert datepicker format to timestamp
     * @param model
     */
    modelToStamp(model: NgbDateStruct) {
        return new Date(this.modelToString(model)).getTime();
    }

    /**
     * Convert time stamp to string
     * @param stamp
     */
    stampToString(stamp: number) {

        const pointer = this;
        const date = new Date(stamp);
        const value =
            date.getUTCFullYear() + '-' +
            pointer.getNumber(date.getUTCMonth() + 1) + '-' +
            pointer.getNumber(date.getUTCDate());

        return value;
    }

    /**
     * Convert datepicker format to string
     * @param model
     */
    modelToString(model: NgbDateStruct) {

        let pointer = this;

        return model ? model.year + '-' +
            pointer.getNumber(model.month) + '-' +
            pointer.getNumber(model.day) : null;
    }

    /**
     * Convert string date to datepicker format
     * @param value
     */
    plainToModel(value: any): NgbDateStruct {

        let pointer = this;
        let date = new Date(value);

        return {
            year: date.getUTCFullYear(),
            month: date.getUTCMonth() + 1,
            day: date.getUTCDate()
        };
    }

    /**
     * Add leading zero if needed
     * @param value
     */
    getNumber(value: number): number {
        return (value < 10 ? '0' + value : value) as number;
    }

    /**
     * Create date range for predefined values
     * @param control - HTML element
     */
    setControlRange(control) {

        let pointer = this;
        let date = new Date();
        let stamp = date.getTime();
        let day = 24 * 60 * 60 * 1000;

        pointer.controlRange = [];
        pointer.controlRange[1] = stamp;
        
        switch (control.value) {
            case 'recent': pointer.controlRange[0] = stamp - day; break;
            case 'week': pointer.controlRange[0] = stamp - day * 7; break;
            case 'month': pointer.controlRange[0] = stamp - day * 30; break;
            case 'year': pointer.controlRange[0] = stamp - day * 365; break;
        }

        if (pointer.controlRange[0] && pointer.controlRange[1]) {

            pointer.addValue(
                pointer.stampToString(pointer.controlRange[0]) + ' - ' + pointer.stampToString(pointer.controlRange[1]),
                pointer.stampToString(pointer.controlRange[0]) + ' - ' + pointer.stampToString(pointer.controlRange[1])
            );
        }
    }
}