import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { StoreServiceProxy, EditStoreInput,CityServiceProxy,CityListDto} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { result } from 'lodash-es';

@Component({
    selector: 'editStoreModal',
    templateUrl: './edit-store-modal-component.html'
})
export class EditStoreModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    selectedCity:number[]=[];
    store: EditStoreInput = new EditStoreInput();
    cities: CityListDto[] = [];
    active: boolean = false;
    saving: boolean = false;
    citiesAfterParseClass: IMultiSelectOption[] = [];


    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect:true,
        displayAllSelectedText: true

    };

    constructor(
        injector: Injector,
        private _storeService: StoreServiceProxy,
        private _cityService: CityServiceProxy

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCities();
    }

    show(storeId): void {
        this.active = true;
        this.selectedCity=[];
        this.citiesAfterParseClass=[]; 
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }  
        this._storeService.getStoreForEdit(storeId).subscribe((result)=>{
          this.store=result;
          this.selectedCity.push(this.store.cityId);
          this.modal.show();              
        });
     
    }

    getCities(): void {
      
        this._cityService.getCity('').subscribe((result) => {
            this.cities = result.items;
        });
    
   }
    onShown(): void {
        this.nameInput.nativeElement.focus();    }

    save(): void {
        this.spinnerService.show();
        this.saving = true;
        this.store.cityId=this.selectedCity[0];
        this._storeService.editStore(this.store)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.store);
            });

            this.saving = false;
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
