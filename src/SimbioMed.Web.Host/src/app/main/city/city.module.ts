import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {CityRoutingModule} from './city-routing.module';
import {CityComponent} from './city.component';
import {CreateCityModalComponent} from './create-city-modal-component';
import {EditCityModalComponent} from './edit-city-modal-component';



@NgModule({
    declarations: [CityComponent, CreateCityModalComponent,EditCityModalComponent],
    imports: [AppSharedModule, CityRoutingModule]
})
export class CityModule {}
