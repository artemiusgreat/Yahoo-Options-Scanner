import {
    Component,
    ViewEncapsulation,
    OnInit,
    OnDestroy,
    Input,
    Output,
    EventEmitter,
    ChangeDetectorRef,
    AfterViewInit
} from '@angular/core';

import { FormGroup } from '@angular/forms';
import { Observable, Subject } from 'rxjs/Rx';
import { NgbDropdown } from "@ng-bootstrap/ng-bootstrap";

@Component({
    selector: 'app-selectors',
    templateUrl: './selectors.component.html'
})

export class SelectorsComponent implements OnInit, OnDestroy, AfterViewInit {

    group: FormGroup;
    dropdowns: NgbDropdown[] = [];
    getKeys: Function = Object.keys;
    onReset: Subject<any> = new Subject();
    onInclude: Subject<any> = new Subject();
    onExclude: Subject<any> = new Subject();
    destruction: Subject<boolean> = new Subject<boolean>();

    constructor(private changeDetector: ChangeDetectorRef) {
        this.group = new FormGroup({});
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.destruction.next(true);
        this.destruction.unsubscribe();
    }

    ngAfterViewInit() {
    }

    /**
     * Reset form values
     */
    reset() {

        const pointer = this;

        pointer.group.reset();
        pointer.onReset.next(true);
    }
}
