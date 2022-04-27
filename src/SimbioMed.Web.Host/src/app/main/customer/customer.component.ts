import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CustomerServiceProxy, CustomerListDto, ListResultDtoOfCustomerListDto,CityServiceProxy,CityListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
import { DateTime } from 'luxon';


@Component({
    templateUrl: './customer.component.html',
    animations: [appModuleAnimation()]
})

export class CustomerComponent extends AppComponentBase {
    customer: CustomerListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;
    cities: CityListDto[] = [];




    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy,
        private _cityService: CityServiceProxy,
        

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCustomer();  
        
      

    }

    
     getAge(birthday:DateTime)
{
    var dateString=birthday.toString();
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
    {
        age--;
    }
    return age;
}
    
    
    deleteCustomer(customer: CustomerListDto): void {
       
        this.message.confirm(
            this.l(customer.firstName+' '+customer.lastName), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._customerService.deleteCustomer(customer.id).subscribe(() => {
                        let waitResponse = _remove(this.customer, customer);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(customer.firstName+' '+customer.lastName + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(customer.firstName+' '+customer.lastName + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }
    
        getCustomer(): void {
        this._customerService.getCustomer(this.filter).subscribe((result) => {
            this.customer = result.items;
        });

    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "firstName":
                if (this.flagSortAscDesc) {
                    this.customer = this.customer.sort((a, b) => (a.firstName.toLowerCase() > b.firstName.toLowerCase()) ? 1 : -1)
                } else {
                    this.customer = this.customer.sort((a, b) => (a.firstName.toLowerCase() < b.firstName.toLowerCase()) ? 1 : -1)
                }
                break;
            case "lastName":
                if (this.flagSortAscDesc) {
                    this.customer = this.customer.sort((a, b) => (a.lastName.toLowerCase() > b.lastName.toLowerCase()) ? 1 : -1)
                } else {
                    this.customer = this.customer.sort((a, b) => (a.lastName.toLowerCase() < b.lastName.toLowerCase()) ? 1 : -1)
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
