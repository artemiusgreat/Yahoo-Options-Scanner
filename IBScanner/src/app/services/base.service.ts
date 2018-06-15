import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class BaseService {

  constructor(public client: HttpClient) { }

  get(url: string, params: any) {

    let query = new URLSearchParams();

    for (let name in params) {
      query.set(name, params[name]);
    }

    return this
      .client
      .get<any>(url + '?' + query.toString())
      .catch(this.error)
      .map(this.map);
  }

  post(url: string, params: any) {

    return this
      .client
      .post(url, params)
      .catch(this.error)
      .map(this.map);
  }

  map(response: any) {
    return response || {};
  }

  error(error: any) {
    console.log(error);
    return Observable.of([]);
  }
}
