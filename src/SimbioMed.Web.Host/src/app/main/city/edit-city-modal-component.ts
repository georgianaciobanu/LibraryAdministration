import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CityServiceProxy, CreateCityInput, EditCityInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { result } from 'lodash-es';

@Component({
    selector: 'editCityModal',
    templateUrl: './edit-city-modal-component.html'
})
export class EditCityModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    city: EditCityInput = new EditCityInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _cityService: CityServiceProxy
    ) {
        super(injector);
    }

    show(cityId): void {
        this.active = true;
        this._cityService.getCityForEdit(cityId).subscribe((result)=>{
          this.city=result;
          this.modal.show(); 
        });
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.spinnerService.show();
        this.saving = true;
        this._cityService.editCity(this.city)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.city);
            });

            this.saving = false;
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
