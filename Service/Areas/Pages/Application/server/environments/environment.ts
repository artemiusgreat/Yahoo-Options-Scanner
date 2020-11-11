let data: any = {};

data.production = false;

data.mongo = {};
data.mongo.base = {};
data.mongo.base.collection = {};
data.mongo.connection = 'mongodb://localhost:6000';
data.mongo.base.name = 'interactive-brokers';

export const environment = data;
