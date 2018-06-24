import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { OptionsService } from 'app/services/options.service';
import { BaseComponent } from 'app/components/base.component';
import { FormControl } from '@angular/forms';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';
import { environment } from 'environments/environment';
import { ServerDataSource } from 'ng2-smart-table';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.scss']
})

export class OptionsComponent extends BaseComponent implements OnInit {

  @ViewChild('loader') loader: SelectorsComponent;
  @ViewChild('optionsScanner') optionsScanner: SelectorsComponent;

  sources: any = {
    symbols: [
        { value: 'NDX', label: 'NDX Index' },
        { value: 'SPX', label: 'SPX Index' },
        { value: 'BKNG', label: 'Booking' },
        { value: 'AMZN', label: 'Amazon' }
    ],
    optionsScanner: ServerDataSource
  };

  loaders: any = {
    download: false,
    optionsScanner: false
  };

  errors: any = {
    download: [],
    optionsScanner: []
  };

  settings = {
    columns: {
      localSymbol: {
        title: 'Name'
      },
      right: {
        title: 'Right'
      },
      strike: {
        title: 'Strike'
      },
      expiry: {
        title: 'Expiration Date'
      }
    }
  };

  constructor(
    public httpClient: HttpClient,
    public optionsService: OptionsService) {
    super();
  }

  ngOnInit() {
  }

  download(event: any) {

    let values = this.loader.group.value;
    let end = ((values.end || [])[0] || {}).value;
    let start = ((values.start || [])[0] || {}).value;

    let params = {
      Dates: [],
      Symbols: (values.symbols || []).map(o => o.value)
    };

    this.errors.download = [];

    if (!start || !end || !params.Symbols.length) {
      this.errors.download.push('Parameters not defined');
      return false;
    }

    let endDate = new Date(end);
    let startDate = new Date(start);
    let endYear = endDate.getFullYear();
    let startYear = startDate.getFullYear();
    let endMonth = endDate.getMonth();
    let startMonth = startDate.getMonth();
    let pad = o => o < 10 ? '0' + o : o;

    for (let k = startYear; k < endYear + 1; k++) {
      for (let n = 0; n < 12; n++) {
        if (!(k == startYear && n < startMonth || k == endYear && n > endMonth)) {
          params.Dates.push(k + '' + pad(n + 1));
        }
      }
    }

    this.loaders.download = true;

    this.optionsService.post(environment.options.download, params).toPromise().then(response => {
      response = response || {};
      this.errors.download = response.Errors || ['Connection error'];
      this.loaders.download = false;
    }).catch(e => {
      this.loaders.download = false;
    });

    return true;
  }

  scan(event: any) {

    let query = new URLSearchParams();
    let values: any = this.optionsScanner.group.value;

    query.set('End', ((values.end || [])[0] || {}).value);
    query.set('Start', ((values.start || [])[0] || {}).value);

    let configuration = new ServerSourceConf({
      endPoint: environment.options.scan,
      pagerLimitKey: 'Limit',
      pagerPageKey: 'Page',
      sortFieldKey: 'Order',
      sortDirKey: 'Direction',
      totalKey: 'Count',
      dataKey: 'Items'
    });

    this.sources.optionsScanner = new ServerDataSource(this.httpClient, configuration);
  }
}
