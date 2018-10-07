import { Component } from '@angular/core';
import { WeatherData } from './weather-data';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  hottest = null;
  title = 'Weather App';
  weatherData = new Array<WeatherData>();

  searchWasExecuted(executed: Boolean){
    if(executed)    {
      this.weatherData = new Array<WeatherData>();
      this.hottest = null;
    }
  }

  weatherDataUpdated(data: WeatherData[]){
    this.weatherData = data;

    let hottestTemp = 0;

    for(let weather of data){
      if(weather.TemperatureRounded > hottestTemp){
        hottestTemp = weather.TemperatureRounded;
        this.hottest = weather.CityName;
      }
      else if(weather.TemperatureRounded == hottestTemp){
        this.hottest = 'Both';
      }
    }
  }
}
