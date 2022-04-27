import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { AuthorServiceProxy, EditAuthorInput, CityServiceProxy, CityListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgxImageCompressService } from "ngx-image-compress";


@Component({
    selector: 'editAuthorModal',
    templateUrl: './edit-author-modal-component.html'
})
export class EditAuthorModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;
    selectedCity: number[] = [];
    author: EditAuthorInput = new EditAuthorInput();
    cities: CityListDto[] = [];
    active: boolean = false;
    saving: boolean = false;
    citiesAfterParseClass: IMultiSelectOption[] = [];
    url: any;
    imgResultBeforeCompression: string = "";
    imgResultAfterCompression: string = "";
    saveFlag: boolean = false;

    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect: true
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

    
    show(authorId): void {
        this.active = true;
        this.selectedCity = [];
        this.citiesAfterParseClass = [];
        for (let x = 0; x < this.cities.length; x++) {
            this.citiesAfterParseClass.push({ id: this.cities[x].id, name: this.cities[x].cityName })
        }
        this._authorService.getAuthorForEdit(authorId).subscribe((result) => {
            this.imgResultAfterCompression = this.author.photoFileBytes;
            this.author = result;
            this.selectedCity.push(this.author.cityId);
            this.modal.show();
        });
    }


    getCities(): void {

        this._cityService.getCity('').subscribe((result) => {
            this.cities = result.items;
        });

    }
    compressFile() {
        this.author.photoId = '00000000-0000-0000-0000-000000000000';
        this.saveFlag = true; 
        this.imageCompress.uploadFile().then(
            ({ image, orientation }) => {
                this.imgResultBeforeCompression = image;
                this.url = image; 
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


    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    updateImageUrl(img: string) { 
        this.url = img 

    }

    save(): void {
        if (this.saveFlag)
            return;
        this.spinnerService.show();
        this.saving = true;
        this.author.cityId = this.selectedCity[0];
        if (this.url) {
            this.author.photoFileBytes = this.url;
        }
        this._authorService.editAuthor(this.author)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.author);
                this.imgResultAfterCompression = null

            });

        this.saving = false;
    }


    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
