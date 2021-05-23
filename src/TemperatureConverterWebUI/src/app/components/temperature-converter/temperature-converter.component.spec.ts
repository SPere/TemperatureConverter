import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { EMPTY, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { TemperatureUnit } from 'src/app/enums/TemperatureUnit';
import { TemperatureConverterService } from 'src/app/services/temperature-converter.service';

import { TemperatureConverterComponent } from './temperature-converter.component';

describe('TemperatureConverterComponent', () => {
  let component: TemperatureConverterComponent;
  let fixture: ComponentFixture<TemperatureConverterComponent>;
  let temperatureConverterServiceSpy: jasmine.SpyObj<TemperatureConverterService>;

  beforeEach(async () => {
    temperatureConverterServiceSpy = jasmine.createSpyObj('TemperatureConverterService', ['convert']);

    await TestBed.configureTestingModule({
      declarations: [ TemperatureConverterComponent ],
      providers: [
        { provide: TemperatureConverterService, useValue: temperatureConverterServiceSpy  }
      ],
      imports: [
        BrowserModule, FormsModule, ReactiveFormsModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TemperatureConverterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  
  describe('constructor', () => {
    it('should initialize the fields', () => {
      // assert
      expect(component.fromValue.value).toBe('');
      expect(component.fromUnit.value).toBe(TemperatureUnit.Celsius);
      expect(component.toUnit.value).toBe(TemperatureUnit.Fahrenheit);
      expect(component.toValue$).toBe(EMPTY);
    })

    it('should empty toValue$ when fromValue changes', fakeAsync(() => {
      // arrange
      component.toValue$ = of(23.22);

      // act
      component.fromValue.setValue(1111);
      fixture.detectChanges();
      tick(10);

      // assert
      expect(component.toValue$).toBe(EMPTY);
    }))

    it('should empty toValue$ when fromUnit changes', fakeAsync(() => {
      // arrange
      component.toValue$ = of(23.22);

      // act
      component.fromUnit.setValue(1);
      fixture.detectChanges();
      tick(10);

      // assert
      expect(component.toValue$).toBe(EMPTY);
    }))

    it('should empty toValue$ when toUnit changes', fakeAsync(() => {
      // arrange
      component.toValue$ = of(23.22);

      // act
      component.toUnit.setValue(1);
      fixture.detectChanges();
      tick(10);

      // assert
      expect(component.toValue$).toBe(EMPTY);
    }))
  });

  describe('click', () => {
    it('should mark from value as touched', () => {
      // act
      component.click();

      // assert
      expect(component.fromValue.touched).toBeTruthy()
    });
  });

    describe('reset', () => {
      it('should reset the from value', () => {
        // arrange
        component.fromValue.setValue(23.23);

        // act
        component.reset();

        // assert
        expect(component.fromValue.value).toBe("");
      });

      it('should reset the from unit', () => {
        // arrange
        component.fromUnit.setValue(TemperatureUnit.Kelvin);

        // act
        component.reset();

        // assert
        expect(component.fromUnit.value).toBe(TemperatureUnit.Celsius);
      });

      it('should reset the to unit', () => {
        // arrange
        component.fromUnit.setValue(TemperatureUnit.Kelvin);

        // act
        component.reset();

        // assert
        expect(component.toUnit.value).toBe(TemperatureUnit.Fahrenheit);
      });
    })

    describe('convert', () => {
      it('should empty toValue$ when fromValue is empty', () => {
        // arrange
        component.toValue$ = of(23.22);
        component.fromValue.setValue("");

        // act
        component.convert();

        // assert
        expect(component.toValue$).toBe(EMPTY);
      })

      it('should set toValue$ to service response when all fields are valid', fakeAsync(() => {
        // arrange
        let returnResult = 17.2222;
        temperatureConverterServiceSpy.convert.and.returnValue(of(returnResult).pipe(delay(5)))
        component.fromValue.setValue(23.24);
        component.fromUnit.setValue(TemperatureUnit.Fahrenheit);
        component.toUnit.setValue(TemperatureUnit.Kelvin);
        component.toValue$ = of(23.22);
        let toValue = 0;

        // act
        component.convert();
        fixture.detectChanges();
        component.toValue$.subscribe(x => toValue = x);
        tick(10);

        // assert
        expect(toValue).toBe(returnResult);
        expect(component.loading$).toBeFalsy();
      }))

      it('should set errorMessage when all fields are valid and response fails', fakeAsync(() => {
        // arrange
        temperatureConverterServiceSpy.convert.and.returnValue(of(17.22).pipe(delay(5), map(x => { throw new Error('some network error') })))
        component.fromValue.setValue(23.24);
        component.fromUnit.setValue(TemperatureUnit.Fahrenheit);
        component.toUnit.setValue(TemperatureUnit.Kelvin);
        component.toValue$ = of(23.22);
        let toValue = 0;
        
        // act
        component.convert();
        fixture.detectChanges();
        component.toValue$.subscribe(x => toValue = x);
        tick(10);

        // assert
        expect(toValue).toBe(0);
        expect(component.errorMsg).toBe('Error: some network error')
        expect(component.loading$).toBeFalsy();
      }))
    });
});
