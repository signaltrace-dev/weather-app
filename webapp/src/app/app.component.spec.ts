import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { LocationFormComponent } from './location-form/location-form.component';
import { WeatherDataComponent } from './weather-data/weather-data.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from "@angular/common/http/testing";



describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        LocationFormComponent,
        WeatherDataComponent
      ],
      imports:[
        FormsModule,
        HttpClientTestingModule,
        ReactiveFormsModule
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
});
