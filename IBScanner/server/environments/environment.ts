let data: any = {};

data.production = false;

data.mongo = {};
data.mongo.base = {};
data.mongo.base.collection = {};
data.mongo.connection = 'mongodb://localhost:6000';
data.mongo.base.name = 'interactive-brokers';
data.mongo.base.collection.option = 'option';

export const environment = data;
