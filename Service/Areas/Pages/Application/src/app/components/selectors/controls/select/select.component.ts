import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
    selector: 'app-selectors-select',
    templateUrl: './select.component.html',
    styleUrls: ['./select.component.html']
})

export class SelectorsSelectComponent extends SelectorsControlComponent {

    /**
     * Pass selected value to reactive form
     * @param event
     */
    setSelection(event: any) {

        const pointer = this;
        const selection = (pointer.source || []).filter(o => o.value == event.target.value)[0];
        
        if (selection) {
            pointer.addValue(selection.value, selection.label);
        }
    }
}
