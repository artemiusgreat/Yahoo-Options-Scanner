import { Router, Request, Response, NextFunction } from 'express';
import { BaseController } from '../../base/controllers/base';
import { OptionModel } from '../models/option';
import { MongoModel } from '../../base/models/mongo';
import async from 'async';

class Controller extends BaseController {

  constructor() {
    super();
  }

  downloadContracts(query: Request, response: Response, done: NextFunction) {

    let pointer = this;
    let messages = (errors, items, count) => {
      return {
        Count: count || 0,
        Items: items instanceof Array ? items : (items ? [items] : []),
        Errors: errors instanceof Array ? errors : (errors ? [errors] : [])
      };
    };

    try {

      let data = query.body || {};
      let dates = data.Dates || [];
      let symbols = data.Symbols || [];
      let mongoModel = new MongoModel();
      let optionModel = new OptionModel();

      async.auto({

        open: (done) => {
          optionModel.openConnection(done);
        },

        contracts: ['open', (data, done) => {

          let params = [];

          dates.forEach((date) => {
            symbols.forEach((symbol) => {
              params.push(optionModel.client.contract.option(symbol, date, 0, ''));
            });
          });

          optionModel.getContracts(params, done);
        }],

        prices: ['open', 'contracts', (data, done) => {
          optionModel.getOptionDetails(data.contracts, done);
        }],

        save: ['open', 'contracts', 'prices', (data, done) => {
          mongoModel.saveOptions(data.prices, done);
        }],

        close: ['open', 'contracts', 'prices', 'save', (data, done) => {
          optionModel.closeConnection(done);
        }]

      }, (e, data) => {
        response.send(messages((e || {}).message, (data || {}).save, null));
      });

    } catch (e) {
      response.send(messages(e, null, null));
    }
  }

  scanContracts(query: Request, response: Response, done: NextFunction) {

    let pointer = this;
    let messages = (errors, items, count) => {
      return {
        Count: count || 0,
        Items: items instanceof Array ? items : (items ? [items] : []),
        Errors: errors instanceof Array ? errors : (errors ? [errors] : [])
      };
    };

    try {

      let selectors = query.query || {};
      let mongoModel = new MongoModel();

      async.auto({

        search: [(done) => {
          mongoModel.searchOptions(selectors, done);
        }]

      }, (e, data) => {

        let errors = (e || {}).message;
        let items = ((data || {}).search || {}).Items || [];
        let count = ((data || {}).search || {}).Count || 0;

        response.send(messages(errors, items, count));
      });

    } catch (e) {
      response.send(messages(e, null, null));
    }
  }
}

export const OptionController = new Controller();
