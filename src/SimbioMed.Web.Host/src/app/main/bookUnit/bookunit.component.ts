import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BookUnitServiceProxy, BookUnitListDto, ListResultDtoOfBookUnitListDto } from '@shared/service-proxies/service-proxies';
import {  remove as _remove } from 'lodash-es';

@Component({
    templateUrl: './bookUnit.component.html',
    animations: [appModuleAnimation()]
})

export class BookUnitComponent extends AppComponentBase {
    bookUnit: BookUnitListDto[] = [];
    filter: string = '';


    constructor(
        injector: Injector,
        private _bookUnitService: BookUnitServiceProxy,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getBookUnit();   
    }
    
  
    deleteBookUnit(bookUnit: BookUnitListDto): void {
       
        this.message.confirm(
            this.l(bookUnit.isbn+' '), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._bookUnitService.deleteBookUnit(bookUnit.id).subscribe(() => {
                        let waitResponse = _remove(this.bookUnit, bookUnit);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(bookUnit.isbn+' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(bookUnit.isbn+ ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

        getBookUnit(): void {
        this._bookUnitService.getBookUnit(this.filter).subscribe((result) => {
            this.bookUnit = result.items;
        });
    }

    
   

}
