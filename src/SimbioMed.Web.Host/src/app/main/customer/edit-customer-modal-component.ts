import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CustomerServiceProxy, EditCustomerInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { DatePipe } from '@angular/common';


@Component({
    selector: 'editCustomerModal',
    templateUrl: './edit-customer-modal-component.html'
})
export class EditCustomerModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;
    selectedCity: number[] = [];
    customer: EditCustomerInput = new EditCustomerInput();
    cities: CityListDto[] = [];
    active: boolean = false;
    saving: boolean = false;
    citiesAfterParseClass: IMultiSelectOption[] = [];


    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy,
        private _cityService: CityServiceProxy,
        public datepipe: DatePipe

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCities();
    }

    show(customerId): void {
        this.active = true;
        this.selectedCity=[];
        this.citiesAfterParseClass=[]; 
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }  
        this._customerService.getCustomerForEdit(customerId).subscribe((result) => {
            this.customer = result;
            this.selectedCity.push(this.customer.cityId);
            this.modal.show();
        });
    }

    getCities(): void {

        this._cityService.getCity('').subscribe((result) => {
            this.cities = result.items;
        });

    }
    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect: true
    };

    save(): void {
        this.spinnerService.show();
        this.saving = true;
        this.customer.cityId = this.selectedCity[0];
        this._customerService.editCustomer(this.customer)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.customer);
            });

        this.saving = false;
    }




    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
