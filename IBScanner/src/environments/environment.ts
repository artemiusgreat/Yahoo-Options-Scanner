let data: any = {};

data.production = false;

data.options = {};
data.options.chain = 'http://localhost:54981/service/options';
data.options.schedule = 'http://localhost:54981/service/options/schedule';

export const environment = data;
