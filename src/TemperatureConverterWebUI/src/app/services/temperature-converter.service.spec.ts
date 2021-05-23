import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TemperatureConverterService } from './temperature-converter.service';
import { TemperatureUnit } from '../enums/TemperatureUnit';

describe('TemperatureConverterService', () => {
  let service: TemperatureConverterService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(TemperatureConverterService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('convert', () => {
    it('should return an Observable<number>', () => {
      // arrange
      const returnValue = 111111;
      const fromValue= 123;
      const fromUnit = TemperatureUnit.Celsius;
      const toUnit = TemperatureUnit.Fahrenheit;
  
      // act
      service.convert(fromValue, fromUnit, toUnit).subscribe(result => {
        expect(result).toBe(returnValue);
      });
  
      // assert
      const req = httpMock.expectOne(`http://localhost:55555/api/TemperatureConverter/${fromValue}/${fromUnit}/${toUnit}`);
      expect(req.request.method).toBe("GET");
      req.flush(returnValue);
    });
  });
});
