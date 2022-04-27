import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CityServiceProxy, CreateCityInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'createCityModal',
    templateUrl: './create-city-modal-component.html'
})
export class CreateCityModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    city: CreateCityInput = new CreateCityInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _cityService: CityServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.city = new CreateCityInput();
        this.modal.show();
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._cityService.createCity(this.city)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.city);
            });
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
