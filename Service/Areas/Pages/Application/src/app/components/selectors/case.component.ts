import { Component, Input, OnDestroy, OnInit, ViewEncapsulation, ElementRef, ChangeDetectorRef, EventEmitter, Output } from '@angular/core';
import { Http } from '@angular/http';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';

@Component({
  selector: 'app-selectors-case',
  templateUrl: './case.component.html'
})

export class SelectorsCaseComponent {

  @Output() onChange: EventEmitter<any> = new EventEmitter();
  @Input() instances: number = 100;
  @Input() sensitive: number = 0;
  @Input() selector: string = '';
  @Input() name: string = '';
  @Input() label: string = '';
  @Input() values: any = '';
  @Input() source: any = '';
  @Input() references: any = '';
  @Input() all: any = true;
  @Input() container: SelectorsComponent;
  @Input() conditions: string[] = [null, 'or', 'not'];

  onDataChange(event: any) {
    this.onChange.next(event);
  }
}
