import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { PublisherServiceProxy, CreatePublisherInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgxImageCompressService } from "ngx-image-compress";


@Component({
    selector: 'createPublisherModal',
    templateUrl: './create-publisher-modal-component.html'
})
export class CreatePublisherModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    publisher: CreatePublisherInput = new CreatePublisherInput();
    cities: CityListDto[] = [];
    selectedCity: number[] = [];
    active: boolean = false;
    saving: boolean = false;
    citiesAfterParseClass: IMultiSelectOption[] = [];
    imgResultBeforeCompression: string = "";
    imgResultAfterCompression: string = "";
    saveFlag: boolean = false;
    url: any;


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
        private _publisherService: PublisherServiceProxy,
        private _cityService: CityServiceProxy,
        private imageCompress: NgxImageCompressService

    ) {
        super(injector);
    }

    show(): void {
        this.selectedCity = [];
        this.citiesAfterParseClass=[];
        this.active = true;
        this.publisher = new CreatePublisherInput();
        this.modal.show();
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }
    }

    ngOnInit(): void {
        this.getCities();
    }

    compressFile() {
        this.publisher.photoId = '00000000-0000-0000-0000-000000000000';
        this.saveFlag = true;
        this.imageCompress.uploadFile().then(
            ({ image, orientation }) => {

                this.imgResultBeforeCompression = image;
                this.url = image
                this.saveFlag = false;


                this.imageCompress
                    .compressFile(image, orientation, 50, 50)
                    .then(
                        (compressedImage) => {
                            this.imgResultAfterCompression = compressedImage;
                            this.saveFlag = false;

                        }
                    );
            }
        );
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
        if (this.saveFlag)
        return;
        this.saving = true;
        if (this.selectedCity[0] == null) {
            this.saving = false;
            this.notify.info(this.l('City is required'));
            return;
        }
        if (this.url) {
            this.publisher.photoFileBytes = this.url;
        }

        this.spinnerService.show();
        this.publisher.cityId = this.selectedCity[0];
        this._publisherService.createPublisher(this.publisher)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.publisher); //save image to db
            });
    }

    close(): void {
        this.modal.hide();
        this.active = false;
        this.url = null;

    }
}
