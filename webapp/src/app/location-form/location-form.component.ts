import { Component, EventEmitter, OnInit, Injectable, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { WeatherData } from '../weather-data';
import { WeatherDataService } from '../weather-data/weather-data.service';

@Component({
  providers: [WeatherDataService],
  selector: 'app-location-form',
  templateUrl: './location-form.component.html',
  styleUrls: ['./location-form.component.css']
})

export class LocationFormComponent implements OnInit {
  @Output() weatherData = new EventEmitter<WeatherData[]>();
  @Output() searchExecuted = new EventEmitter<Boolean>();

  location = new FormControl('');
  errors = [];
  searching = false;
  
  constructor(private http: HttpClient, private weatherDataService: WeatherDataService) { }

  ngOnInit() {
  }

  private handleError(error: HttpErrorResponse){
    let message = error.error && error.error.Message ? error.error.Message : 'Sorry, it looks like there was a problem contacting the weather service!';
    switch(error.status){
      case 404:
        message = `We couldn't find anything matching "${this.location.value}". Try searching again.`;
      break;
    }
    this.errors.push(message);
  }

  searchLocation(){
    let data = [];
    this.errors = [];

    if(this.location.value){
      this.searchExecuted.emit(true);
      this.searching = true;

      this.weatherDataService.getData(this.location.value)
        .subscribe(resp =>{
          this.weatherDataService.getData('Gulf Breeze').subscribe(compareResp => {
            data = data.concat(resp.body);
            data = data.concat(compareResp.body);
            this.weatherData.emit(data);
            this.searching = false;
          },
          error => {
            this.searching = false;
            this.handleError(error);
          })
        },
        error => {
          this.searching = false;
          this.handleError(error);
        });
      }
      else{
        this.errors.push('You have to enter a location!');
      }
  }
}
