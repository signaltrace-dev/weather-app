import { Component } from '@angular/core';
import { WeatherData } from './weather-data';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Weather App';
  weatherData = new Array<WeatherData>();

  searchWasExecuted(executed: Boolean){
    if(executed)    {
      this.weatherData = new Array<WeatherData>();
    }
  }

  weatherDataUpdated(data: WeatherData[]){
    this.weatherData = data;
  }
}
