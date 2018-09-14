import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NgbTypeaheadSelectItemEvent } from '@ng-bootstrap/ng-bootstrap';
import { NgbModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { Subject, Observable } from 'rxjs/Rx';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';

@Component({
    selector: 'app-selectors-autocomplete',
    templateUrl: './autocomplete.component.html',
    styleUrls: ['./autocomplete.component.scss']
})

export class SelectorsAutoCompleteComponent extends SelectorsControlComponent {

    @ViewChild('selector') selector: ElementRef;

    /**
     * Return observable source
     */
    search = (update: Observable<string>) => {

        let pointer = this;

        // Merge is used to display all values on focus, without typing

        return update
            .debounceTime(300)
            .distinctUntilChanged()
            .switchMap(query => {

                query = (query || '').toLowerCase();

                // Display array of static items

                if (pointer.source instanceof Array) {

                    return Observable.of(pointer.source.filter(o => {
                        o = o || {};
                        o.label = o.label || '';
                        return o.label.toLowerCase().includes(query);
                    }));
                }

                // Display items from external callback

                if (pointer.source instanceof Function) {

                    return pointer.source(query).catch(err => {
                        console.log(err);
                        return [];
                    });;
                }

                // Load items from remote resource

                if (typeof pointer.source === 'string') {

                    return pointer.http
                        .get(pointer.source + query)
                        .map(res => res)
                        .catch(err => {
                            console.log(err);
                            return [];
                        });
                }

                return Observable.of(pointer.source);
            });
    }

    /**
     * Format selection, not used in filters
     */
    displaySelection = (option: any) => {
        return option;
    }

    /**
     * Callback for selection event
     * @param selection
     */
    selectItem(selection: NgbTypeaheadSelectItemEvent) {

        const pointer = this;

        selection.preventDefault();
        pointer.addValue(selection.item.value, selection.item.label);
        pointer.selector.nativeElement.value = selection.item.label;
    }

    /**
     * Display available options
     * @param event
     */
    onFocus = (event: any) => {

        const pointer = this;

        event.stopPropagation();

        setTimeout(() => {
            event.target.dispatchEvent(new Event('input'));
        }, 0);
    }
}
