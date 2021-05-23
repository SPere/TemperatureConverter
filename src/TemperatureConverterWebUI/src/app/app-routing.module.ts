import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TemperatureConverterAdvanceComponent } from './components/temperature-converter-advance/temperature-converter-advance.component';
import { TemperatureConverterComponent } from './components/temperature-converter/temperature-converter.component';

const routes: Routes = [
  { path:'simple', component: TemperatureConverterComponent },
  { path:'advance', component: TemperatureConverterAdvanceComponent},
  { path:'', pathMatch: 'full', redirectTo: '/simple' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
