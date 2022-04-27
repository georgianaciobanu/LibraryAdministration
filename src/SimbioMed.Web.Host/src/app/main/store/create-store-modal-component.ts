import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { StoreServiceProxy, CreateStoreInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';


@Component({
    selector: 'createStoreModal',
    templateUrl: './create-store-modal-component.html'})
export class CreateStoreModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    store: CreateStoreInput = new CreateStoreInput();
    cities: CityListDto[] = [];
    selectedCity: number[] = [];
    active: boolean = false;
    saving: boolean = false;
    citiesAfterParseClass: IMultiSelectOption[] = [];

    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect: true,
        displayAllSelectedText: true

    };

    constructor(
        injector: Injector,
        private _storeService: StoreServiceProxy,
        private _cityService: CityServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.selectedCity=[];
        this.citiesAfterParseClass=[];
        this.active = true;
        this.store = new CreateStoreInput(); 
        this.modal.show();
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }
    }

    ngOnInit(): void {
        this.getCities();
    }

    getCities() {
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
        this.store.cityId = this.selectedCity[0];
        this._storeService.createStore(this.store)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.store);
            });
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
