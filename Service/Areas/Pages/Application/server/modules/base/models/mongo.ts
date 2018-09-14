import { BaseModel } from "./base";
import { MongoClient } from "mongodb";
import { environment } from "../../../environments/environment";
import async from 'async';

class Model extends BaseModel {

  client: any = null;

  query: any = {
    Page: 0,
    Limit: 0,
    Count: 0,
    Order: null,
    Direction: 1,
    Items: []
  };

  constructor() {
    super();
  }

  openConnection(connection: string, done: Function) {

    let pointer = this;

    if (pointer.client && pointer.client.isConnected()) {
      done(null, pointer.client);
      return;
    }

    MongoClient.connect(connection, (e, client) => {
      pointer.client = client;
      done(e, pointer.client);
    });
  }

  closeConnection(client: MongoClient) {
    client.close();
  }

  saveOptions(contracts: any, done: Function) {

    let pointer = this;

    async.auto({

      open: (done) => {
        pointer.openConnection(environment.mongo.connection, done);
      },

      save: ['open', (data, done) => {

        try {

          let client = data.open;
          let storage = client.db(environment.mongo.base.name);
          let collection = storage.collection('options');

          for (let id in contracts) {
            collection.update({ _id: id }, contracts[id].summary, { upsert: true });
          }

          done(null, Object.keys(contracts));

        } catch (e) {
          done(e, null);
        }

      }]

    }, (e, data) => {
      done(e, data.save);
    });
  }

  searchOptions(query: any, done: Function) {

    let pointer = this;

    query = Object.assign({}, pointer.query, query);

    async.auto({

      open: (done) => {
        pointer.openConnection(environment.mongo.connection, done);
      },

      search: ['open', (data, done) => {

        try {

          let client = data.open;
          let storage = client.db(environment.mongo.base.name);
          let collection = storage.collection('options');
          let limit = +query.Limit || 0;
          let page = +query.Page || 0;
          let items = collection.find();

          items.skip(page * limit).limit(limit).toArray((e, docs) => {
            items.count().then((count) => {

              let response = Object.assign({}, pointer.query);

              response.Count = count;
              response.Items = docs;

              done(e, response);
            });
          });

        } catch (e) {
          done(e, null);
        }

      }]

    }, (e, data) => {
      done(e, data.search);
    });
  }
}

export const MongoModel = Model;