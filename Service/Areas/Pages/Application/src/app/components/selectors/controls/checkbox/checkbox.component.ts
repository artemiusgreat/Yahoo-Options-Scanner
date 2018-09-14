import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef, AfterContentInit, Input } from '@angular/core';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
  selector: 'app-selectors-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss']
})

export class SelectorsCheckboxComponent extends SelectorsControlComponent {

  /**
   * Event handler that update real control value on while user is typing something
   * @param event
   */
  detectChange(event: any) {

    const pointer = this;

    if (event.target.checked) {
      pointer.addValue(event.target.checked ? 1 : 0, pointer.label);
      return;
    }

    pointer.removeValue();
  }
}
