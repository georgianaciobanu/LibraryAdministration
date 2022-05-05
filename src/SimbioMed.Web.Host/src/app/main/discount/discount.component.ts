import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { DiscountServiceProxy, DiscountListDto, ListResultDtoOfDiscountListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
@Component({
    templateUrl: './discount.component.html',
    animations: [appModuleAnimation()]
})

export class DiscountComponent extends AppComponentBase {
    discount: DiscountListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;


    constructor(
        injector: Injector,
        private _discountService: DiscountServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getDiscount();
    }
    
    
    deleteDiscount(discount: DiscountListDto): void {
       
        this.message.confirm(
            this.l(discount.description), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._discountService.deleteDiscount(discount.id).subscribe(() => {
                        let waitResponse = _remove(this.discount, discount);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(discount.description + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(discount.description + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

    getDiscount(): void {
        this._discountService.getDiscounts(this.filter).subscribe((result) => {
            this.discount = result.items;
        });
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "description":
                if (this.flagSortAscDesc) {
                    this.discount = this.discount.sort((a, b) => (a.description.toLowerCase() > b.description.toLowerCase()) ? 1 : -1)
                } else {
                    this.discount = this.discount.sort((a, b) => (a.description.toLowerCase() < b.description.toLowerCase()) ? 1 : -1)
                }
                break;  
                case "value":
                    if (this.flagSortAscDesc) {
                        this.discount = this.discount.sort((a, b) => (a.value > b.value ) ? 1 : -1)
                    } else {
                        this.discount = this.discount.sort((a, b) => (a.value < b.value) ? 1 : -1)
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
