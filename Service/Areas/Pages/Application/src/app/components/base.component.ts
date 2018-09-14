import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { OnDestroy } from '@angular/core';
import { ViewChild } from '@angular/core';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { OptionsService } from 'app/services/options.service';

@Component({
  selector: 'app-management-base',
  template: ''
})
export class BaseComponent implements OnDestroy {

  speedControl: any = null;
  destruction: Subject<any> = new Subject<any>();

  ngOnDestroy() {
    this.destruction.next(true);
    this.destruction.unsubscribe();
  }
}
