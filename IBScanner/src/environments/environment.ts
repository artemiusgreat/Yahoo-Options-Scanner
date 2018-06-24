let data: any = {};

data.production = false;

data.options = {};
//data.options.scan = 'http://localhost:54981/service/options';
//data.options.download = 'http://localhost:54981/service/options/download';
data.options.scan = 'http://localhost:5000/service/options/scan';
data.options.download = 'http://localhost:5000/service/options/download';

export const environment = data;
