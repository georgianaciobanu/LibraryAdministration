import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CityServiceProxy, CityListDto, ListResultDtoOfCityListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
@Component({
    templateUrl: './city.component.html',
    animations: [appModuleAnimation()]
})

export class CityComponent extends AppComponentBase {
    city: CityListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;


    constructor(
        injector: Injector,
        private _cityService: CityServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCity();
    }
    
    
    deleteCity(city: CityListDto): void {
       
        this.message.confirm(
            this.l(city.cityName), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._cityService.deleteCity(city.id).subscribe(() => {
                        let waitResponse = _remove(this.city, city);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(city.cityName + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(city.cityName + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

    getCity(): void {
        this._cityService.getCity(this.filter).subscribe((result) => {
            this.city = result.items;
        });
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "cityName":
                if (this.flagSortAscDesc) {
                    this.city = this.city.sort((a, b) => (a.cityName.toLowerCase() > b.cityName.toLowerCase()) ? 1 : -1)
                } else {
                    this.city = this.city.sort((a, b) => (a.cityName.toLowerCase() < b.cityName.toLowerCase()) ? 1 : -1)
                }
                break;
            case "country":
                if (this.flagSortAscDesc) {
                    this.city = this.city.sort((a, b) => (a.country.toLowerCase() > b.country.toLowerCase()) ? 1 : -1)
                } else {
                    this.city = this.city.sort((a, b) => (a.country.toLowerCase() < b.country.toLowerCase()) ? 1 : -1)
                }
                break;
            default:
                console.log(
                    "Sorting field doesn't exist"
                )
        }
        this.flagSortAscDesc = !this.flagSortAscDesc

    }
    

}
