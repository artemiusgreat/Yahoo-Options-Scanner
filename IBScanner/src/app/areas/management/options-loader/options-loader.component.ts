import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { OnDestroy } from '@angular/core';
import { ViewChild } from '@angular/core';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { OptionsService } from 'app/services/options.service';
import { BaseComponent } from 'app/components/base.component';
import { FormControl } from '@angular/forms';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';
import { environment } from 'environments/environment';
import { OptionsComponent } from 'app/areas/management/options/options.component';

@Component({
  selector: 'app-options-loader',
  templateUrl: './options-loader.component.html',
  styleUrls: ['./options-loader.component.scss']
})

export class OptionsLoaderComponent extends OptionsComponent {

  conditionSources: any = {

    Name: [
      { value: '', label: 'Property' },
      { value: 'Expiration', label: 'Expiration' },
      { value: 'Symbol', label: 'Symbol' }
    ],
    Operation: {
      Expiration: [
        { value: 'MonthRange', label: 'month range is' }
      ],
      Symbol: [
        { value: 'In', label: 'is one of' }
      ]
    },
    Value: {
      Expiration: {
        selector: 'date'
      },
      Symbol: {
        selector: 'multiselect',
        source: [
          { value: 'NDX', label: 'NDX Index' },
          { value: 'SPX', label: 'SPX Index' },
          { value: 'BKNG', label: 'Booking' },
          { value: 'AMZN', label: 'Amazon' }
        ]
      }
    }
  };

  schedule() {
    this.send(environment.options.schedule, { Action: 'schedule' });
  }
}
