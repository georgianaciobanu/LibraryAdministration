import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { AuthorServiceProxy, CreateAuthorInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgxImageCompressService } from "ngx-image-compress";

@Component({
    selector: 'createAuthorModal',
    templateUrl: './create-author-modal-component.html',
    styleUrls: ['./create-author-modal-component.less']
})
export class CreateAuthorModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    author: CreateAuthorInput = new CreateAuthorInput();
    cities: CityListDto[] = [];
    selectedCity: number[] = [];
    active: boolean = false;
    saving: boolean = false;
    url: any;
    imgResultBeforeCompression: string = "";
    imgResultAfterCompression: string = "";
    citiesAfterParseClass: IMultiSelectOption[] = [];
    saveFlag: boolean = false;



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
        private _authorService: AuthorServiceProxy,
        private _cityService: CityServiceProxy,
        private imageCompress: NgxImageCompressService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getCities();
    }


    show(): void {
       this.imgResultAfterCompression=null;
        this.selectedCity=[];
        this.citiesAfterParseClass=[];
        this.active = true;
        this.author = new CreateAuthorInput();
        this.modal.show();
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }
      
    }

    compressFile() {
        this.author.photoId = '00000000-0000-0000-0000-000000000000';
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

    changeSelectedType(event: any) {
        console.log(event); //object, depends on ngValue
        console.log(this.author.gender); //object, depends on ngValue
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

            this.author.photoFileBytes = this.url;
        }
        this.spinnerService.show();
        this.author.cityId = this.selectedCity[0];
        this._authorService.createAuthor(this.author)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.author);
            });
    }

    close(): void {
        this.modal.hide();
        this.url = null;
        this.active = false;
    }
}
