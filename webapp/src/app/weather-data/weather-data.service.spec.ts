import { WeatherDataService } from "./weather-data.service";
import { TestBed, inject, getTestBed } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";

let injector: TestBed;
let service: WeatherDataService;
let httpMock: HttpTestingController;

const dummyData = {
    CityName: 'Some Place', 
    Description: 'Fairly muggy', 
    Humidity: 56, 
    Temperature: 80.45, 
    TemperatureRounded: 80, 
    Zip: ''
};

describe('WeatherDataService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [WeatherDataService],
            imports: [
                HttpClientTestingModule
            ]
        });
        injector = getTestBed();
        service = injector.get(WeatherDataService);
        httpMock = injector.get(HttpTestingController);
    });
    
    afterEach(() => {
        httpMock.verify();
    });

    it('should return a WeatherData object', () => {
        const weatherService = getTestBed().get(WeatherDataService);
        weatherService.getData('some location').subscribe(data => {
            expect(data.body.CityName.length).toBeGreaterThan(0);
        });

        const req = httpMock.expectOne(service.API_ENDPOINT);
        expect(req.request.method).toBe('POST');

        req.flush(dummyData);
        httpMock.verify();
    });
});