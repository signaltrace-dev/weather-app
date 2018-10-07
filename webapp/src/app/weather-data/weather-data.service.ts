import { WeatherData } from "../weather-data";
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class WeatherDataService{
    private data: WeatherData[] = [];
    readonly API_ENDPOINT = 'http://localhost:51345/api/weather';
    readonly API_KEY = 'McfNGT6AvJ7AVDLtg2zL7NM42jt5rhmf';

    constructor(private http: HttpClient){}

    getData(location): Observable<HttpResponse<WeatherData>> {

        var headers = {
            authorization: `apikey ${this.API_KEY}`,
            'Content-Type': 'application/json'
        };
 
        var data = {
            location: location
        };

        return this.http.post<WeatherData>(this.API_ENDPOINT, JSON.stringify(data), {headers: headers, observe: 'response'});
    }
}