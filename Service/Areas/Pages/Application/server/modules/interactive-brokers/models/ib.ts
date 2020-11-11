import { BaseModel } from "../../base/models/base";
import * as IB from 'ib';

class Model extends BaseModel {

  id: number = 0;
  client: any = null;
  minMessageCode: number = 2100;
  maxMessageCode: number = 2137;

  constructor() {
    super();
  }

  getId(): number {
    return parseInt(Math.random().toString(10).substr(2, 9));
  }

  getError(e, data) {

    data = data || {};
    data.id = Math.max(data.id, 0) || 0;
    data.code = Math.max(data.code, 0) || 0;
    data.message = e;

    if (data.code >= this.minMessageCode || data.code <= this.maxMessageCode) {
      data.id = 0;
      data.code = 0;
    }

    return data;
  }

  openConnection(done: Function) {

    let pointer = this;

    try {

      pointer.client = new IB();
      pointer.client.connect();
      pointer.client.on('connected', (response) => {
        done(null, pointer.client);
      });

    } catch (e) {

      done(e, null);
    }
  }

  closeConnection(done: Function) {

    let pointer = this;

    try {

      pointer.client.disconnect();
      pointer.client.on('disconnected', (response) => {
        delete pointer.client;
        done(null, null);
      });

    } catch (e) {

      done(e, null);
    }
  }
}

export const IBModel = Model;