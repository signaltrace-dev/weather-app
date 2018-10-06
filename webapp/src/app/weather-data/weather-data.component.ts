import { Component, OnInit, Input } from '@angular/core';
import { WeatherData } from '../weather-data';

@Component({
  selector: 'app-weather-data',
  templateUrl: './weather-data.component.html',
  styleUrls: ['./weather-data.component.css']
})
export class WeatherDataComponent implements OnInit {
  @Input() data: WeatherData[];
  constructor() { }

  ngOnInit() {
  }

}
