export class WeatherData{
    CityName: string;
    Description: string;
    Humidity: number;
    IconUrl: string;
    Temperature: number;
    TemperatureRounded: number;
    Zip: string;

    public constructor(init?:Partial<WeatherData>){
        Object.assign(this, init);
    }
}