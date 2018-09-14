import { Component, Input, OnDestroy, OnInit, ViewEncapsulation, ElementRef, ChangeDetectorRef, OnChanges, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subject, Observable } from 'rxjs';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { HttpClient } from '@angular/common/http';
import { BaseComponent } from 'app/components/base.component';
import { Output } from '@angular/core';

@Component({
  selector: 'app-selectors-control',
  template: '<div></div>'
})

export class SelectorsControlComponent extends BaseComponent implements OnInit, OnDestroy {

  @Output() onChange: EventEmitter<any> = new EventEmitter();

  @Input() name: string = '';
  @Input() label: string = '';
  @Input() values: any[] = [];
  @Input() instances: number = 100;

  @Input() set sourceParam(source: any) {
    this.setSource(source);
  }

  @Input()

  set containerParam(container: SelectorsComponent) {

    const pointer = this;

    // Add to Reactive Form every control that was added in UI

    if (container) {
      pointer.group = container.group;
      pointer.container = container;
      pointer.group.addControl(pointer.name, new FormControl(pointer.values, []));
    }
  }

  group: FormGroup;
  source: any = [];
  original: any = [];
  container: SelectorsComponent;
  getKeys: Function = Object.keys;

  constructor(
    public http?: HttpClient,
    public elements?: ElementRef,
    public changeDetector?: ChangeDetectorRef) {
    super();
  }

  ngOnInit() {
  }

  /**
   * Add value to the list 
   * @param value
   * @param label
   */
  addValue(value: any, label: any) {

    let params = {};
    let pointer = this;
    let selection = {
      value: value,
      label: label
    };

    pointer.values = pointer.values || [];

    if (pointer.instances > 1) {
      pointer.values.push(selection);
    } else {
      pointer.values = [selection];
    }

    params[pointer.name] = pointer.values;
    pointer.group.controls[pointer.name].setValue(pointer.values);
    pointer.container.onInclude.next(params);
    pointer.onChange.next(params);
  }

  /**
   * Remove value to the list 
   */
  removeValue() {

    let params = {};
    let pointer = this;

    pointer.values = [];
    params[pointer.name] = pointer.values;
    pointer.group.controls[pointer.name].setValue(pointer.values);
    pointer.container.onInclude.next(params);
    pointer.onChange.next(params);
  }

  /**
   * Define data source for control as a string URL, array of label / value objects, function, or Observable
   * @param source
   */
  setSource(source: any) {

    // Do not remove this method, it's overriden in multiselect component
    // It's needed to separate controls that load data on page load, or by some action, like typing

    this.setLocalSource(source);
  }

  /**
   * Define data source for control as a string URL, array of label / value objects
   * @param source
   */
  setLocalSource(source: any) {

    this.source = source;

    if (this.original && this.original.length == 0) {

      this.original = source;

      if (source instanceof Object) {
        this.original = Object.assign({}, source);
      }

      if (source instanceof Array) {
        this.original = source.map(o => Object.assign({}, o));
      }
    }
  }
}
