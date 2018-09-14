import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef, AfterContentInit, Input } from '@angular/core';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
  selector: 'app-selectors-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})

export class SelectorsInputComponent extends SelectorsControlComponent {

    @Input() sensitive: number = 0;

    /**
     * Event handler that update real control value on while user is typing something
     * @param event
     */
    detectChange(event: any) {

        const pointer = this;

        // Sensitive means to react on all keyup events, not "enter" only
        
        if (pointer.sensitive) {
            pointer.addValue(event.target.value, event.target.value);
        }
    }

    /**
     * Event handler that update real control value only when user press "Enter"
     * @param event
     */
    detectEnterChange(event: any) {

        const pointer = this;

        pointer.addValue(event.target.value, event.target.value);

        if (pointer.sensitive == 0) {
            event.target.value = '';
        }
    }
}
