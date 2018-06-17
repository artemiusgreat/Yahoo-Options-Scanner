import { BaseModel } from "./base";
import { MongoClient } from "mongodb";
import { environment } from "../../../environments/environment";

class Model extends BaseModel {

  constructor() {
    super();
  }

  openConnection(connection: string) {

    let pointer = this;

    return new Promise((success, error) => {

      try {

        MongoClient.connect(connection, (e, client) => {
          success(client);
        });

      } catch (e) {
        error(e);
      }
    });
  }

  closeConnection(client: MongoClient) {
    client.close();
  }

  saveOptions(contracts: any[]) {

    let pointer = this;

    return new Promise((success, error) => {

      try {

        this
          .openConnection(environment.mongo.connection)
          .catch(e => error(e))
          .then((client: any) => {

            let storage = client.db(environment.mongo.base.name);
            let collection = storage.collection(environment.mongo.base.collection.option);

            collection.insertMany(contracts, (e, response) => {

              this.closeConnection(client);

              if (e) {
                return error(e);
              }

              return success(response);
            });
          });

      } catch (e) {
        error(e);
      }
    });
  }
}

export const MongoModel = Model;