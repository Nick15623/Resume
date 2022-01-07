import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Endpoint, EndpointType } from '../models/form.models';

@Injectable()
export default class ApiService {

  constructor(private http: HttpClient) { }

  async handleRequest(endpoint: Endpoint, model: any = null): Promise<any> {
    var returnValue = null;
    var shouldContinue: boolean = true;

    if (endpoint) {

      if (endpoint.Before) {
        model = endpoint.Before(model);
      }

      if (shouldContinue) {
        switch (endpoint.Type) {
          case EndpointType.GET:
            returnValue = await this.get(endpoint);
            break;
          case EndpointType.POST:
            returnValue = await this.post(endpoint, model);
            break;
          case EndpointType.PUT:
            returnValue = await this.put(endpoint, model);
            break;
          case EndpointType.DELETE:
            returnValue = await this.delete(endpoint);
            break;
        }
      }

      if (endpoint.After) {
        returnValue = endpoint.After(returnValue);
      }

    }

    if (!shouldContinue) { returnValue = null; }
    return returnValue;
  }

  private async get(endpoint: Endpoint): Promise<any> {
    return await this.http.get<Array<any>>(endpoint.Url).toPromise();
  }

  private async post(endpoint: Endpoint, model: any): Promise<any> {
    return await this.http.post<any>(this.buildUrl(endpoint, model), model).toPromise();
  }

  private async put(endpoint: Endpoint, model: any): Promise<any> {
    return await this.http.put<any>(this.buildUrl(endpoint, model), model).toPromise();
  }

  private async delete(endpoint: Endpoint): Promise<any> {
    return await this.http.delete<any>(endpoint.Url).toPromise();
  }

  private buildUrl(endpoint: Endpoint, model: any): string {
    var url = "";
    if (endpoint) {
      url = endpoint.Url;
      if (endpoint.QueryString && model) {
        var isFirst: boolean = true;
        endpoint.QueryString.forEach(x => {
          if (isFirst) {
            var value = "";
            if (x.CustomValue) { value = x.CustomValue(model[x.PropertyName]); }
            else { value = model[x.PropertyName]?.toString() ?? ""; }
            url = `${url}?${x.Param}=${value}`; 
            isFirst = false;
          } else {
            var value = "";
            if (x.CustomValue) { value = x.CustomValue(model[x.PropertyName]); }
            else { value = model[x.PropertyName]?.toString() ?? ""; }
            url = `${url}&${x.Param}=${value}`; 
          }
        });
      }
    }
    return url;
  }
}
