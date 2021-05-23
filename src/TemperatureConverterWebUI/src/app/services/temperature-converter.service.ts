import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { TemperatureUnit } from '../enums/TemperatureUnit';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TemperatureConverterService {

  constructor(private httpClient: HttpClient) { }

  convert(fromValue: number, fromUnit: TemperatureUnit, toUnit: TemperatureUnit): Observable<number> {
    return this.httpClient.get<number>(`${environment.temperatureConverterUrl}/${fromValue}/${fromUnit}/${toUnit}`);
  }
}
