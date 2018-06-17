import { Router, Request, Response, NextFunction } from 'express'
import { BaseController } from '../../base/controllers/base';
import { OptionModel } from '../models/option';
import { MongoModel } from '../../base/models/mongo';

class Controller extends BaseController {

  constructor() {
    super();
  }

  getContracts(query: Request, response: Response, done: NextFunction) {

    try {

      let mongoModel = new MongoModel();
      let optionModel = new OptionModel();

      optionModel
        .getRemoteContracts('AAPL', '201807')
        .catch(e => console.log(e))
        .then((contracts: any[]) => {
          console.log('### ', contracts.length)
          //mongoModel.saveOptions(contracts);
        });

      response.send('WORKS');

    } catch (e) {
      done(e);
    }
  }
}

export const OptionController = new Controller();
