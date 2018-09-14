import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef, AfterContentInit, Input, HostListener, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { NgbDropdown } from "@ng-bootstrap/ng-bootstrap";
import { Subject } from "rxjs/Rx";
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
  selector: 'app-selectors-multiselect',
  templateUrl: './multiselect.component.html',
  styleUrls: ['./multiselect.component.scss']
})

export class SelectorsMultiSelectComponent extends SelectorsControlComponent implements OnInit, AfterViewInit {

  @ViewChild('menu') menu: NgbDropdown;
  @ViewChild('selector') selector: ElementRef;
  @ViewChild('list') list: ElementRef;

  @Input() all: any = true;
  @Input() conditions: string[] = [null, 'or', 'not'];

  conditional: string = null;

  @HostListener('document:click', ['$event'])
  onClick(event) {
    if (this.reference.nativeElement.contains(event.target) == false) {
      this.menu.close();
    }
  }

  constructor(public reference: ElementRef) {
    super();
  }

  ngAfterViewInit() {
  }

  ngOnInit() {

    let pointer = this;

    pointer.menu['id'] = pointer.name;
    pointer.container.dropdowns.push(pointer.menu);

    // Watch for new filters added from outside of current control

    pointer.container.onReset.takeUntil(pointer.destruction).subscribe(content => {
      pointer.source = pointer.original;
    });
  }

  /**
   * Pass selected value to reactive form
   * @param item
   */
  selectOne(item: any) {

    let pointer = this;

    pointer.values = [];
    pointer.source.forEach(o => {

      if (o.value == item.value) {
        pointer.conditional = null;
        o.conditional = pointer.getNextState(o.conditional);
      }

      if (o.conditional) {
        pointer.values.push(o);
      }
    });

    let params = {};

    params[pointer.name] = pointer.values;
    pointer.group.controls[pointer.name].setValue(pointer.values);
    pointer.container.onInclude.next(params);
    pointer.onChange.next(params);
  }

  /**
   * Pass selected value to reactive form
   * @param conditional - global state for all checkboxes
   */
  selectAll(conditional?: string) {

    let pointer = this;

    pointer.values = [];
    pointer.conditional = conditional || pointer.getNextState(pointer.conditional);
    pointer.source.forEach(o => {

      o.conditional = pointer.conditional;

      if (o.conditional) {
        pointer.values.push(o);
      }
    });

    let params = {};

    params[pointer.name] = pointer.values;
    pointer.group.controls[pointer.name].setValue(pointer.values);
    pointer.container.onInclude.next(params);
    pointer.onChange.next(params);
  }

  /**
   * Define cycle of states
   * @param conditional - null, or, not
   */
  getNextState(conditional: string) {

    // If condition is undefined, next one is "or"

    if (!conditional) {

      conditional = 'or';

      if (this.conditions.includes(conditional)) {
        return conditional;
      }
    }

    // If condition is "or", but if "or" is not allowed, next one is "not"

    if (conditional == 'or') {

      conditional = 'not';

      if (this.conditions.includes(conditional)) {
        return conditional;
      }
    }

    return null;
  }

  setSource(source: any) {

    // If options are dynamic and need to be requested via HTTP first

    if (source instanceof Function) {
      source('').toPromise().then(items => this.setLocalSource(items));
      return;
    }

    // Otherwise, show static options

    this.setLocalSource(source);
  }
}
