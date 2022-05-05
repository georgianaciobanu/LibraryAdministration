import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SaleServiceProxy, SaleListDto, ListResultDtoOfSaleListDto } from '@shared/service-proxies/service-proxies';
import {  remove as _remove } from 'lodash-es';

@Component({
    templateUrl: './sale.component.html',
    animations: [appModuleAnimation()]
})

export class SaleComponent extends AppComponentBase {
    sale: SaleListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;



    constructor(
        injector: Injector,
        private _saleService: SaleServiceProxy,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getSale();   
    }
    
  
    deleteSale(sale: SaleListDto): void {
       
        this.message.confirm(
            this.l(sale.saleNumber+' '), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._saleService.deleteSale(sale.id).subscribe(() => {
                        let waitResponse = _remove(this.sale, sale);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(sale.saleNumber+' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(sale.saleNumber+ ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

        getSale(): void {
        this._saleService.getSale(this.filter).subscribe((result) => {
            this.sale = result.items;
        });
    }

    
    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "totalAmount":
                if (this.flagSortAscDesc) {
                    this.sale = this.sale.sort((a, b) => (a.totalAmount > b.totalAmount) ? 1 : -1)
                } else {
                    this.sale = this.sale.sort((a, b) => (a.totalAmount < b.totalAmount) ? 1 : -1)
                }
                break;
            case "totalQuantity":
                if (this.flagSortAscDesc) {
                    this.sale = this.sale.sort((a, b) => (a.totalQtty > b.totalQtty) ? 1 : -1)
                } else {
                    this.sale = this.sale.sort((a, b)  => (a.totalQtty < b.totalQtty) ? 1 : -1)
                }
                break;
            case "store":
                if (this.flagSortAscDesc) {
                    this.sale = this.sale.sort((a, b) => (a.store.storeName.toLowerCase() > b.store.storeName.toLowerCase()) ? 1 : -1)
                } else {
                    this.sale = this.sale.sort((a, b) => (a.store.storeName.toLowerCase() < b.store.storeName.toLowerCase()) ? 1 : -1)
                }
                break;

            case "saleDate":
                if (this.flagSortAscDesc) {
                    this.sale = this.sale.sort((a, b) => (a.saleDate > b.saleDate) ? 1 : -1)
                } else {
                    this.sale = this.sale.sort((a, b)  => (a.saleDate < b.saleDate) ? 1 : -1)
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
