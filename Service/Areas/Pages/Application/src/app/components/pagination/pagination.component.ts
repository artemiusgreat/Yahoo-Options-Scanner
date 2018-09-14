import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class PaginationComponent {

    @Input() itemsPage: number = 1;
    @Input() itemsCount: number = 0;
    @Input() itemsLimit: number = 20;

    @Output() onChangeEvent: EventEmitter<number> = new EventEmitter();

    /**
     * Count pages based on total number of records
     */
    getPageCount() {

      const pointer = this;
      const count = pointer.itemsCount || 0;
      const limit = pointer.itemsLimit || 1;

      return Math.ceil(count / limit);
    }

    /**
     * Change page by click on a button
     * @param event
     * @param action
     */
    onClick(event: any, action: string) {

        const pointer = this;
        const minPage = 1;
        const maxPage = pointer.getPageCount();
        const page: any = pointer.itemsPage;

        event.preventDefault();

        switch (action) {
            case 'start': pointer.itemsPage = minPage; break;
            case 'previous': pointer.itemsPage = Math.max(parseInt(page) - 1, minPage); break;
            case 'next': pointer.itemsPage = Math.min(parseInt(page) + 1, maxPage); break;
            case 'end': pointer.itemsPage = maxPage; break;
        }

        pointer.onChangeEvent.emit(pointer.itemsPage);
        return false;
    }

    /**
     * Change page by entering exact page number
     * @param event
     */
    onKey(event: any) {

        event.preventDefault();

        const pointer = this;
        const minPage = 1;
        const maxPage = pointer.getPageCount();

        let page = parseInt(event.target.value);

        if (isNaN(page)) {
            return false;
        }

        if (page < minPage) {
            page = minPage;
        }

        if (page > maxPage) {
            page = maxPage;
        }

        pointer.itemsPage = page;
        pointer.onChangeEvent.emit(pointer.itemsPage);
        return false;
    }

    constructor() {}
}
