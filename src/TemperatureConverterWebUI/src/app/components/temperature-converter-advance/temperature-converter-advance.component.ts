import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { EMPTY, Observable, Subject } from 'rxjs';
import { catchError, debounceTime, filter, takeUntil, tap } from 'rxjs/operators';
import { TemperatureUnit } from 'src/app/enums/TemperatureUnit';
import { TemperatureConverterService } from 'src/app/services/temperature-converter.service';

@Component({
  selector: 'app-temperature-converter-advance',
  templateUrl: './temperature-converter-advance.component.html',
  styleUrls: ['./temperature-converter-advance.component.css']
})
export class TemperatureConverterAdvanceComponent implements OnDestroy {

  temperatureUnits = Object.keys(TemperatureUnit);

  fromValue: FormControl;
  fromUnit: FormControl;
  toUnit: FormControl;
  toValue$: Observable<number>;

  loading$: boolean = false;
  errorMsg: string = "";
  destroy$: Subject<boolean> = new Subject();


  constructor(private fb: FormBuilder, private temperatureService: TemperatureConverterService) { 
    this.fromValue = fb.control("", [ Validators.required ]);
    this.fromUnit = fb.control(this.temperatureUnits[0], [ Validators.required ]);
    this.toUnit = fb.control(this.temperatureUnits[1], [ Validators.required ])
    this.toValue$ = EMPTY;

    this.fromValue.valueChanges.pipe(
      takeUntil(this.destroy$),
      tap(() => this.toValue$ = EMPTY),
      filter(val => val),
      debounceTime(700),
      tap(() => this.convert())
    ).subscribe();

    this.fromUnit.valueChanges.pipe(
      takeUntil(this.destroy$),
      tap(() => this.convert())
    ).subscribe();

    this.toUnit.valueChanges.pipe(
      takeUntil(this.destroy$),
      tap(() => this.convert())
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  click(): void {
    this.fromValue.markAsTouched();
    this.convert();
  }

  reset(): void {
    this.fromValue.reset("");
    this.fromUnit.reset(this.temperatureUnits[0]);
    this.toUnit.reset(this.temperatureUnits[1]);
  }

  convert(): void {
    if (!this.fromValue.valid) {
      this.toValue$ = EMPTY;
      return;
    }

    this.loading$ = true;
    this.errorMsg = "";
    this.toValue$ = this.temperatureService.convert(this.fromValue.value,this.fromUnit.value,this.toUnit.value)
      .pipe(
        tap(() =>  this.loading$ = false),
        catchError(error => {
            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error error: ${error.error.message}`;
            } else {
                this.errorMsg = `Error: ${error.message}`;
            }
            this.loading$ = false;
            return EMPTY;
        })
      )
  }

}
