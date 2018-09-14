import { Component, ViewChild } from '@angular/core';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { OptionsService } from 'app/services/options.service';
import { BaseComponent } from 'app/components/base.component';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.scss']
})

export class OptionsComponent extends BaseComponent {

  @ViewChild('optionsLoader') optionsLoader: SelectorsComponent;
  @ViewChild('optionsScanner') optionsScanner: SelectorsComponent;

  items: any = {
    rows: [],
    page: 1,
    count: 0,
    limit: 10
  };

  sources: any = {
    optionsCombos: [
      { value: '', label: 'Combinations' },
      { value: 'ShortPut', label: 'Short Put' },
      { value: 'ShortCall', label: 'Short Call' },
      { value: 'LongCondor', label: 'Long Condor' },
      { value: 'ShortCondor', label: 'Short Condor' },
      { value: 'LongStrangle', label: 'Long Strangle' },
      { value: 'ShortStrangle', label: 'Short Strangle' },
      { value: 'LongPutSpread', label: 'Long Put Spread' },
      { value: 'LongCallSpread', label: 'Long Call Spread' },
      { value: 'ShortPutSpread', label: 'Short Put Spread' },
      { value: 'ShortCallSpread', label: 'Short Call Spread' }
    ]
  };

  loaders: any = {
    optionsScanner: false
  };

  errors: any = {
    optionsScanner: []
  };

  constructor(
    public httpClient: HttpClient,
    public optionsService: OptionsService) {
    super();
  }

  onPageChange(index: number) {
    this.items.page = index;
    this.scan(null);
  }

  scan(event: any) {

    let values: any = this.optionsScanner.group.value;
    let params: any = {
      Page: this.items.page,
      Limit: this.items.limit,
      Stop: (values.stop || []).map(o => o.value)[0],
      Start: (values.start || []).map(o => o.value)[0],
      Cache: (values.cache || []).map(o => o.value)[0],
      Combo: (values.combo || []).map(o => o.value)[0],
      Symbol: (values.symbols || []).map(o => o.value)
    };

    for (let i in params) {
      if (!params[i]) {
        delete params[i];
      }
    }

    this.errors.optionsScanner = [];
    this.loaders.optionsScanner = true;
    this.optionsService.get(environment.options.scan, params).toPromise().then((response: any) => {
      this.items.rows = response.Items;
      this.items.count = response.Count;
      this.errors.optionsScanner = response.Errors;
      this.loaders.optionsScanner = false;
    }).catch(response => {
      this.errors.optionsScanner = response.Errors || ['Unknown error'];
      this.loaders.optionsScanner = false;
    });
  }
}
