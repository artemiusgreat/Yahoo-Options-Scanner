import { Component, OnInit } from '@angular/core';
import { OptionsService } from 'app/services/options.service';
import { BaseComponent } from 'app/components/base.component';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.scss']
})

export class StocksComponent extends BaseComponent implements OnInit {

  orders: any = [];
  records: any = [];
  conditions: any = [];
  keys: Function = Object.keys;

  constructor(public optionsService: OptionsService) {
    super();
  }

  ngOnInit() {
  }
}
