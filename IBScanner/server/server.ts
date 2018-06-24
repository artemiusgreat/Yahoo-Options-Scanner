import * as Express from 'express';
import * as BodyParser from 'body-parser';

import { Router, Request, Response, NextFunction } from 'express';
import { OptionController } from './modules/interactive-brokers/controllers/option';

class Module {

  private express;

  constructor() {

    let router = Router();

    router.get('/options/scan', OptionController.scanContracts);
    router.post('/options/download', OptionController.downloadContracts);

    this.express = Express();

    this.express.use((query, response, done) => {
      response.setHeader('Access-Control-Allow-Origin', '*');
      response.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
      response.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
      response.setHeader('Access-Control-Allow-Credentials', true);
      done();
    });

    this.express.use(BodyParser.json());
    this.express.use(BodyParser.urlencoded({ extended: false }));
    this.express.use('/service', router);

    this.express.listen(5000, (e) => {
      console.log(e || '...');
    });
  }
}

export const Server = new Module();