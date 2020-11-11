import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class BaseService {

  response: any = {
    Count: 0,
    Items: [],
    Errors: []
  };

  constructor(public client: HttpClient) {
  }

  get(url: string, params: any) {

    let query = new URLSearchParams();

    for (let name in params) {
      query.set(name, params[name]);
    }

    return this
      .client
      .get<any>(url + '?' + query.toString())
      .catch(this.error.bind(this))
      .map(this.map.bind(this));
  }

  post(url: string, params: any) {

    return this
      .client
      .post(url, params)
      .catch(this.error.bind(this))
      .map(this.map.bind(this));
  }

  map(response: any) {
    return response || this.response;
  }

  error(error: any) {
    console.log(error);
    return Observable.of(this.response);
  }
}
