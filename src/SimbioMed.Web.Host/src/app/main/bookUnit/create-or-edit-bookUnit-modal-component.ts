import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { DiscountServiceProxy,CreateDiscountBookInput,DiscountListDto, DiscountBookListDto,BookUnitServiceProxy,StoreListDto,PublisherListDto, EditBookUnitInput, CreateBookUnitInput, BookUnitListDto,BookListDto,BookServiceProxy,StoreServiceProxy,PublisherServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { NgxImageCompressService } from "ngx-image-compress";
import { Router } from '@angular/router';
import { id } from '@swimlane/ngx-charts';


@Component({
    selector: 'createOrEditBookUnitModal',
    templateUrl: './create-or-edit-bookUnit-modal-component.html'
})
export class CreateOrEditBookUnitModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    bookUnit: CreateBookUnitInput = new CreateBookUnitInput();
    books: BookListDto[] = [];
    publishers: PublisherListDto[] = [];
    stores: StoreListDto[] = [];
    discountBook: CreateDiscountBookInput = new CreateDiscountBookInput();
    discountBookView: DiscountBookListDto[]=[];
    bookUnitEdit: EditBookUnitInput = new EditBookUnitInput();
    selectedBook: number[] = [];
    discounts: DiscountListDto[] = [];
    selectedPublisher: number[] = [];
    selectedStore: number[] = [];
    selectedDiscount: number[] = [];
    active: boolean = false;
    saving: boolean = false;
    url: any;
    imgResultBeforeCompression: string = "";
    imgResultAfterCompression: string = "";
    booksAfterParseClass: IMultiSelectOption[] = [];
    publishersAfterParseClass: IMultiSelectOption[] = [];
    storesAfterParseClass: IMultiSelectOption[] = [];
    discountsAfterParseClass: IMultiSelectOption[] = [];
    saveFlag: boolean = false;
    insertedBookUnitId:number;



    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect: true,
        displayAllSelectedText: true

    };

    choosemany: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 5,
        autoUnselect: true,
        closeOnSelect: true,
        displayAllSelectedText: true

    };
    constructor(
        injector: Injector,
        private _bookUnitService: BookUnitServiceProxy,
        private _bookService: BookServiceProxy,
        private _publisherService: PublisherServiceProxy,
        private _storeService: StoreServiceProxy,
        private imageCompress: NgxImageCompressService,
        private _discountsService: DiscountServiceProxy,
        private router: Router

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getBooks();
        this.getStores();
        this.getPublishers();
        this.getDiscounts();

    }

    show(bookUnitId): void {
        this.imgResultAfterCompression=null;
         this.selectedBook=[];
         this.selectedPublisher=[];
         this.selectedDiscount=[];
         this.selectedStore=[];
         this.booksAfterParseClass=[];
         this.storesAfterParseClass=[];
         this.publishersAfterParseClass=[];
         this.discountsAfterParseClass=[];
         this.active = true;
         this.bookUnit = new CreateBookUnitInput();
         this.modal.show();
         for (let x = 0; x < this.books.length; x++) {
             this.booksAfterParseClass.push({ id: this.books[x].id, name: this.books[x].title+' de '+this.books[x].author.firstName+' '+this.books[x].author.lastName  })
         }
         for (let x = 0; x < this.stores.length; x++) {
            this.storesAfterParseClass.push({ id: this.stores[x].id, name: this.stores[x].storeName + ' ' +this.stores[x].address + ' '+this.stores[x].city.cityName })
        }
        for (let x = 0; x < this.publishers.length; x++) {
            this.publishersAfterParseClass.push({ id: this.publishers[x].id, name: this.publishers[x].name })
        }
        for (let x = 0; x < this.discounts.length; x++) {
            this.discountsAfterParseClass.push({ id: this.discounts[x].id, name: this.discounts[x].description+ ' '+this.discounts[x].value })
       }
     

         if(bookUnitId!=null){
            this.selectedDiscount=[];
            this.selectedStore=[];
            this.getDiscountBook(bookUnitId);
            this._bookUnitService.getBookUnitForEdit(bookUnitId).subscribe((result) => {
                this.imgResultAfterCompression = this.bookUnitEdit.photoFileBytes;
                this.imgResultAfterCompression = this.bookUnit.photoFileBytes;
                this.bookUnit = result;
                this.bookUnitEdit = result;
                this.selectedBook.push(this.bookUnitEdit.bookId);
                this.selectedPublisher.push(this.bookUnitEdit.publisherId);
                this.selectedStore.push(this.bookUnitEdit.storeId);
               // this.selectedBook.push(this.bookUnit.bookId);
               // this.selectedPublisher.push(this.bookUnit.publisherId);
               // this.selectedStore.push(this.bookUnit.storeId);
                this.modal.show();
            });


         }
       
     }
     getDiscountBook(bookUnitId): void {
        let dis = this;
        this._bookUnitService.getDiscountBook(bookUnitId).subscribe((result) => {
            this.discountBookView = result;

            dis.discountBookView.forEach(element => {
                dis.selectedDiscount.push(element.discountId);

            });

        });
    }

    getDiscounts(): void {

        this._discountsService.getDiscounts('').subscribe((result) => {
            this.discounts = result.items;
        });

    }

    btnClick_goToDiscount= function () {
        this.router.navigateByUrl('/app/main/discount');
    };
     
     compressFile() {
         this.bookUnit.photoId = '00000000-0000-0000-0000-000000000000';
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

     getBooks(): void {

        this._bookService.getBooks('').subscribe((result) => {
            this.books = result.items;
        });

    }

    getStores(): void {

        this._storeService.getStore('').subscribe((result) => {
            this.stores = result.items;
        });

    }

    getPublishers(): void {

        this._publisherService.getPublisher('').subscribe((result) => {
            this.publishers = result.items;
        });

    }

   
    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        if(this.bookUnitEdit.id!=null ){
            this.saveForEdit();
        }
        else{
            this.saveForCreate();
        }

    }
    btnClick_goToPublisher= function () {
        this.router.navigateByUrl('/app/main/publisher');
};

btnClick_goToStore= function () {
    this.router.navigateByUrl('/app/main/store');
};

btnClick_goToTitle= function () {
    this.router.navigateByUrl('/app/main/books');
};


    saveForCreate(): void {
        if (this.saveFlag)
        return;
        this.saving = true;
        if (this.selectedBook[0] == null) {
            this.saving = false;
            this.notify.info(this.l('Title is required'));
            return;
        }
        if (this.selectedPublisher[0] == null) {
            this.saving = false;
            this.notify.info(this.l('Publisher is required'));
            return;
        }
        if (this.selectedStore[0] == null) {
            this.saving = false;
            this.notify.info(this.l('Store is required'));
            return;
        }
        if (this.url) {

            this.bookUnit.photoFileBytes = this.url;
        }
        let hs=this;   
        this.spinnerService.show();
        this.bookUnit.bookId = this.selectedBook[0];
        this.bookUnit.storeId = this.selectedStore[0];
        this.bookUnit.publisherId = this.selectedPublisher[0];

        this._bookUnitService.createBookUnit(this.bookUnit)
            .pipe(finalize(() => this.saving = false))
            .subscribe(result => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.bookUnit);
                hs.insertedBookUnitId=result;
                this.addNewDiscount();

            });

    }

    saveForEdit(): void {
        let hs=this;   
        if (this.saveFlag)
            return;
        this.spinnerService.show();
        this.saving = true;
        this.bookUnitEdit.bookId = this.selectedBook[0];
        this.bookUnitEdit.storeId = this.selectedStore[0];
        this.bookUnitEdit.publisherId = this.selectedPublisher[0];
        if (this.url) {
            this.bookUnitEdit.photoFileBytes = this.url;
        }
        this._bookUnitService.editBookUnit(this.bookUnitEdit)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                hs.insertedBookUnitId=this.bookUnitEdit.id;
                this.updateDiscountList();
                this.modalSave.emit(this.bookUnitEdit);
                this.imgResultAfterCompression = null
               
            });

        this.saving = false;
    }

    async updateDiscountList(): Promise<void> {
        let dis = this;
        this.discountBookView.forEach(function (disc) {
            dis._bookUnitService.deleteDiscountBook(disc.id).subscribe(() => {
            });

        });

     this.addNewDiscount();
      


        this.selectedDiscount = [];

    }



    addNewDiscount(): void{
        let dis=this;
        dis.selectedDiscount.forEach(function(det){
            dis.discountBook=new CreateDiscountBookInput();
            dis.discountBook.discountId=det;
            dis.discountBook.bookUnitId=dis.insertedBookUnitId;
            dis._bookUnitService.createDiscountBook(dis.discountBook) 
            .pipe(finalize(() => dis.saving = false))
            .subscribe(() => {
                dis.modalSave.emit(dis.discountBook);
            });



        } );

    }


    close(): void {
        this.modal.hide();
        this.url = null;
        this.active = false;
    }
}