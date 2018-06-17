import { BaseModel } from "../../base/models/base";
import { IBModel } from "./ib";
import { MongoModel } from "../../base/models/mongo";
import { environment } from "../../../environments/environment";

class Model extends IBModel {

  constructor() {
    super();
  }

  getRemoteContracts(symbol: string, expiration: string) {

    let pointer = this;
    let queryId = pointer.getId();

    return new Promise((success, error) => {

      try {

        let contracts = [];

        pointer.client.connect();

        pointer.client.on('error', (e, data) => {
          if (data.id == queryId) {
            error(e);
          }
        });

        pointer.client.once('contractDetails', (id, contract) => {
          if (id == queryId) {
            contracts.push(contract);
          }
        });

        pointer.client.once('contractDetailsEnd', (id) => {
          if (id == queryId) {
            success(contracts);
            pointer.client.disconnect();
          }
        });

        pointer.client.reqContractDetails(queryId, pointer.client.contract.option(symbol, expiration));

      } catch (e) {
        error(e);
      }
    });
  }
}

export const OptionModel = Model;