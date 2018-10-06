import { WeatherData } from "../weather-data";
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class WeatherDataService{
    private data: WeatherData[] = [];

    constructor(private http: HttpClient){}

    getData(location): Observable<HttpResponse<WeatherData>> {
        var apiEndpoint = 'http://localhost:51345/api/weather';
        var apiKey = 'McfNGT6AvJ7AVDLtg2zL7NM42jt5rhmf';

        var headers = {
            authorization: `apikey ${apiKey}`,
            'Content-Type': 'application/json'
        };
 
        var data = {
            location: location
        };

        return this.http.post<WeatherData>(apiEndpoint, JSON.stringify(data), {headers: headers, observe: 'response'});
    }
}