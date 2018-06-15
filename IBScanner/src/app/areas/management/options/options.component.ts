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

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.scss']
})

export class OptionsComponent extends BaseComponent implements OnInit {

  @ViewChild('selectors') selectors: SelectorsComponent;

  orderSources: any = {

    Name: [
      { value: '', label: 'Order' },
      { value: 'Expiration', label: 'Expiration' },
      { value: 'Strike', label: 'Strike' },
      { value: 'Combination', label: 'Combination' },
      { value: 'Symbol', label: 'Symbol' },
      { value: 'Ask', label: 'Ask' },
      { value: 'Bid', label: 'Bid' }
    ],
    Direction: [
      { value: '1', label: 'Ascending' },
      { value: '-1', label: 'Descending' }
    ]
  };

  conditionSources: any = {

    Name: [
      { value: '', label: 'Property' },
      { value: 'Expiration', label: 'Expiration' },
      { value: 'Strike', label: 'Strike' },
      { value: 'Combination', label: 'Combination' },
      { value: 'Symbol', label: 'Symbol' }
    ],
    Operation: {
      Expiration: [
        { value: 'Month', label: 'month is' }
      ],
      Strike: [
        { value: '<=', label: 'is less or equal to' },
        { value: '>=', label: 'is greater or equal to' },
        { value: '=', label: 'is equal to' }
      ],
      Combination: [
        { value: 'In', label: 'is one of' }
      ],
      Symbol: [
        { value: 'In', label: 'is one of' }
      ]
    },
    Value: {
      Expiration: {
        selector: 'date'
      },
      Strike: {
        selector: 'input'
      },
      Combination: {
        selector: 'multiselect',
        source: [
          { value: 'nakedPut', label: 'Naked Put' },
          { value: 'nakedCall', label: 'Naked Call' }
        ]
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

  loader: number = 0;
  orders: any = [];
  errors: any = [];
  items: any = [];
  conditions: any = [];
  keys: Function = Object.keys;

  constructor(public optionsService: OptionsService) {
    super();
  }

  ngOnInit() {
  }

  showItem(value: any) {

    if (value instanceof Array) {
      return value.map(o => o.label).join(' or ');
    }

    return value.label;
  }

  removeItem(items: any[], index: number) {
    items.splice(index, 1);
  }

  addCondition(name: string, operation: string, control: any) {

    let value = null;

    if (control instanceof Array) {

      let start = (this.selectors.group.get(control[0].name).value[0] || {}).value;
      let end = (this.selectors.group.get(control[1].name).value[0] || {}).value;

      if (start && end) {
        value = [{ label: start + ':' + end, value: start + ':' + end }];
      }

    } else {

      value = this.selectors.group.get(control.name).value;
    }
    
    if ((value || []).length) {

      this.conditions.push({
        name: this.conditionSources.Name.filter(o => o.value == name)[0],
        operation: this.conditionSources.Operation[name].filter(o => o.value == operation)[0],
        value: value
      });
    }
  }

  addOrder(name: string, direction: string) {

    let orderName = this.orderSources.Name.filter(o => o.value == name)[0];
    let orderDirection = this.orderSources.Direction.filter(o => o.value == direction)[0];

    if (orderName && (orderDirection || {}).value) {
      this.orders.push({
        name: orderName,
        value: orderDirection
      });
    }
  }

  scan() {
    this.send(environment.options.chain, { Action: 'scan' });
  }

  send(url: string, params?: any) {

    params = params || {};

    params.Action = params.Action || null;
    params.Orders = params.Orders || [];
    params.Selectors = params.Selectors || [];

    this.conditions.forEach((v, k) => {

      let param: any = {};

      param.Name = v.name.value;
      param.Operation = v.operation.value;
      param.Value = v.value.map(o => o.value);
      params.Selectors.push(param);
    });

    this.orders.forEach((v, k) => {

      let param: any = {};

      param.Name = v.name.value;
      param.Direction = v.value.value;
      params.Orders.push(param);
    });

    this.optionsService.post(url, params).toPromise().then(response => {
      response = response || {};
      this.items = response.Items || [];
      this.errors = response.Errors || ['No connection'];
    });
  }
}
