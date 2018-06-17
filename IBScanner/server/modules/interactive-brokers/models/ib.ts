import { BaseModel } from "../../base/models/base";
import * as IB from 'ib';

class Model extends BaseModel {

  id: number = 0;
  client: any = null;

  constructor() {

    super();

    this.client = new IB();
    
    this.client.on('error', (error) => {
    });
    
    this.client.on('result', (event, params) => {
    });

    this.client.once('nextValidId', (id) => {
      this.id = id;
    });
  }

  getId(): number {
    return parseInt(Math.random().toString(10).substr(2, 9));
  }
}

export const IBModel = Model;