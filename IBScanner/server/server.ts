import * as Express from 'express';
import * as BodyParser from 'body-parser';

import { Router, Request, Response, NextFunction } from 'express';
import { OptionController } from './modules/interactive-brokers/controllers/option';

class Module {  

  private express;

  constructor () {

    let router = Router();
    
    router.get('/', OptionController.getContracts);
    
    this.express = Express();
    this.express.use(BodyParser.json());
    this.express.use(BodyParser.urlencoded({ extended: false }));
    this.express.use('/', router);
    this.express.use(Express.static(__dirname + '/assets'));

    this.express.listen(5000, (e) => {  
      console.log(e || 'Server is ready');
    });
  }
}

export const Server = new Module();