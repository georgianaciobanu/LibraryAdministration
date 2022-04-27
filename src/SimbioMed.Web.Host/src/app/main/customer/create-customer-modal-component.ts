import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CustomerServiceProxy, CreateCustomerInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';


@Component({
    selector: 'createCustomerModal',
    templateUrl: './create-customer-modal-component.html'
})
export class CreateCustomerModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    customer: CreateCustomerInput = new CreateCustomerInput();
    cities: CityListDto[] = [];
    selectedCity: number[] = [];
    active: boolean = false;
    saving: boolean = false;
    changed: Date;
    citiesAfterParseClass: IMultiSelectOption[] = [];


    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy,
        private _cityService: CityServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.selectedCity = [];
        this.citiesAfterParseClass=[];
        this.active = true;
        this.customer = new CreateCustomerInput();
        this.modal.show();
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }
    }

    ngOnInit(): void {
        this.getCities();
    }
    
    SendDataonChange(event: any) {
        this.customer.dateOfBirth=event.target.value;
        }

    getCities(): void {

        this._cityService.getCity('').subscribe((result) => {
            this.cities = result.items;
        });

    }
  

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        if (this.selectedCity[0] == null) {
            this.saving = false;
            this.notify.info(this.l('City is required'));
            return;
        }
        this.spinnerService.show();
        this.customer.cityId = this.selectedCity[0];
        this._customerService.createCustomer(this.customer)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.customer);
            });

    }

    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect:true
    };

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
