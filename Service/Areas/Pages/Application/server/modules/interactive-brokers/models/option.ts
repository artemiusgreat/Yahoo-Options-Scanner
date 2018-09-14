import { BaseModel } from "../../base/models/base";
import { IBModel } from "./ib";
import { MongoModel } from "../../base/models/mongo";
import { environment } from "../../../environments/environment";

class Model extends IBModel {

  constructor() {
    super();
  }

  getContracts(selectors: any[], done: Function) {

    let pointer = this;
    let contracts: any = {};
    let queryId = pointer.getId();
    let counter = selectors.length;

    pointer.client.on('error', (e, data) => {
      if (pointer.getError(e, data).code) {
        done(data, null);
      }
    });

    pointer.client.on('contractDetails', (id, contract) => {
      contracts[contract.summary.conId] = contract;
    });

    pointer.client.on('contractDetailsEnd', (id) => {
      if (--counter <= 0) {
        done(null, contracts);
      }
    });

    selectors.forEach((selector) => {
      pointer.client.reqContractDetails(queryId, selector);
    });
  }

  getOptionDetails(contracts: any, done: Function) {

    let pointer = this;
    let counter = Object.keys(contracts).length;
    
    pointer.client.on('error', (e, data) => {
      if (pointer.getError(e, data).code) {
        done(null, data);
      }
    });

    pointer.client.on('tickSize', (id, property, size) => {
      switch (property) {
        case pointer.client.TICK_TYPE.ASK_SIZE: contracts[id].summary.quote.askSize = size; break;
        case pointer.client.TICK_TYPE.BID_SIZE: contracts[id].summary.quote.bidSize = size; break;
      }
    });

    pointer.client.on('tickPrice', (id, property, price, canAutoExecute) => {
      switch (property) {
        case pointer.client.TICK_TYPE.ASK: contracts[id].summary.quote.ask = price; break;
        case pointer.client.TICK_TYPE.BID: contracts[id].summary.quote.bid = price; break;
        case pointer.client.TICK_TYPE.CLOSE: contracts[id].summary.quote.close = price; break;
      }
    });

    pointer.client.on('tickSnapshotEnd', (id) => {
      if (--counter <= 0) {
        done(null, contracts);
      }
    });
      
    for (let id in contracts) {

      contracts[id].summary.quote = contracts[id].summary.quote || {
        askSize: 0,
        bidSize: 0,
        close: 0,
        ask: 0,
        bid: 0
      };

      pointer.client.reqMktData(contracts[id].summary.conId, 
        pointer.client.contract.option(
          contracts[id].summary.symbol, 
          contracts[id].summary.expiry, 
          contracts[id].summary.strike, 
          contracts[id].summary.right), '', true, false);
    }
  }
}

export const OptionModel = Model;