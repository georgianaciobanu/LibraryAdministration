import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PublisherServiceProxy, PublisherListDto, ListResultDtoOfPublisherListDto,CityServiceProxy,CityListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
@Component({
    templateUrl: './publisher.component.html',
    animations: [appModuleAnimation()]
})

export class PublisherComponent extends AppComponentBase {
    publisher: PublisherListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;
    cities: CityListDto[] = [];


    constructor(
        injector: Injector,
        private _publisherService: PublisherServiceProxy,
        private _cityService: CityServiceProxy

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getPublisher();
    }
    
    
    deletePublisher(publisher: PublisherListDto): void {
       
        this.message.confirm(
            this.l(publisher.name), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._publisherService.deletePublisher(publisher.id).subscribe(() => {
                        let waitResponse = _remove(this.publisher, publisher);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(publisher.name +' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(publisher.name + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

        getPublisher(): void {
        this._publisherService.getPublisher(this.filter).subscribe((result) => {
            this.publisher = result.items;
        });
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "name":
                if (this.flagSortAscDesc) {
                    this.publisher = this.publisher.sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase()) ? 1 : -1)
                } else {
                    this.publisher = this.publisher.sort((a, b) => (a.name.toLowerCase() < b.name.toLowerCase()) ? 1 : -1)
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
