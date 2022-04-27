import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { StoreServiceProxy, StoreListDto, ListResultDtoOfStoreListDto,CityServiceProxy,CityListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
@Component({
    templateUrl: './store.component.html',
    animations: [appModuleAnimation()]
})

export class StoreComponent extends AppComponentBase {
    store: StoreListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;
    cities: CityListDto[] = [];


    constructor(
        injector: Injector,
        private _storeService: StoreServiceProxy,
        private _cityService: CityServiceProxy

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getStore();        
    }
    
    
    deleteStore(store: StoreListDto): void {
       
        this.message.confirm(
            this.l(store.storeName), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._storeService.deleteStore(store.id).subscribe(() => {
                        let waitResponse = _remove(this.store, store);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(store.storeName + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(store.address + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

    getStore(): void {
        this._storeService.getStore(this.filter).subscribe((result) => {
            this.store = result.items;
            
        });
    }


    getCities(): void {
        this._cityService.getCity(this.filter).subscribe((result) => {
            this.cities = result.items;
            
        });
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "storeName":
                if (this.flagSortAscDesc) {
                    this.store = this.store.sort((a, b) => (a.storeName.toLowerCase() > b.storeName.toLowerCase()) ? 1 : -1)
                } else {
                    this.store = this.store.sort((a, b) => (a.storeName.toLowerCase() < b.storeName.toLowerCase()) ? 1 : -1)
                }
                break;
            case "address":
                if (this.flagSortAscDesc) {
                    this.store = this.store.sort((a, b) => (a.address.toLowerCase() > b.address.toLowerCase()) ? 1 : -1)
                } else {
                    this.store = this.store.sort((a, b) => (a.address.toLowerCase() < b.address.toLowerCase()) ? 1 : -1)
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
