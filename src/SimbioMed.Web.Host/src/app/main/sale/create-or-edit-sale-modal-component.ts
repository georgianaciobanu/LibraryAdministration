import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import {CreateDiscountSaleInput,  DiscountListDto, DiscountSaleListDto, DiscountServiceProxy, CreateSaleDetailInput, SaleDetailListDto,SaleServiceProxy,BookUnitServiceProxy, BookUnitListDto, StoreListDto, CreateSaleInput, SaleListDto,CustomerServiceProxy,StoreServiceProxy,CustomerListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { Router } from '@angular/router';
import { id } from '@swimlane/ngx-charts';
import { ThrowStmt } from '@angular/compiler';


@Component({
    selector: 'createOrEditSaleModal',
    templateUrl: './create-or-edit-sale-modal-component.html'
})
export class CreateOrEditSaleModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    sale: CreateSaleInput = new CreateSaleInput();
    customers: CustomerListDto[] = [];
    saleDetail: CreateSaleDetailInput = new CreateSaleDetailInput();
    saleDetailView: SaleDetailListDto[]=[];
    newList:CreateSaleDetailInput[]=[];
    discountSale: CreateDiscountSaleInput = new CreateDiscountSaleInput();
    discountSaleView: DiscountSaleListDto[]=[];
    stores: StoreListDto[] = [];
    bookUnit: BookUnitListDto[] = [];
    discounts: DiscountListDto[] = [];
    selectedCustomer: number[] = [];
    selectedStore: number[] = [];
    selectedBook: number[] = [];
    selectedDiscount: number[] = [];
    qtty: any;
    active: boolean = false;
    saving: boolean = false;
    insertedSaleId:number;
    customersAfterParseClass: IMultiSelectOption[] = [];
    storesAfterParseClass: IMultiSelectOption[] = [];
    booksAfterParseClass: IMultiSelectOption[] = [];
    discountsAfterParseClass: IMultiSelectOption[] = [];
    selectedBooks:BookUnitListDto[] = [];

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
        autoUnselect: true,
        closeOnSelect: true,
        displayAllSelectedText: false

    };
    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy,
        private _saleService: SaleServiceProxy,
        private _storeService: StoreServiceProxy,
        private _bookUnitService: BookUnitServiceProxy,
        private _discountsService: DiscountServiceProxy,
        private router: Router

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCustomers();
        this.getStores();
        this.getBooks();
        this.getDiscounts();

    }

    show(saleId): void {
         this.selectedCustomer=[];
         this.selectedBook=[];
         this.selectedDiscount=[];
         this.selectedStore=[];
         this.customersAfterParseClass=[];
         this.storesAfterParseClass=[];
         this.booksAfterParseClass=[];
         this.discountsAfterParseClass=[];
         this.active = true;
         this.sale = new CreateSaleInput();
         this.saleDetail=new CreateSaleDetailInput();
         this.modal.show();
         for (let x = 0; x < this.customers.length; x++) {
             this.customersAfterParseClass.push({ id: this.customers[x].id, name: this.customers[x].firstName+' '+this.customers[x].lastName})
         }
         for (let x = 0; x < this.stores.length; x++) {
            this.storesAfterParseClass.push({ id: this.stores[x].id, name: this.stores[x].storeName + ' ' +this.stores[x].address + ' '+this.stores[x].city.cityName })
        }
        for (let x = 0; x < this.bookUnit.length; x++) {
             this.booksAfterParseClass.push({ id: this.bookUnit[x].id, name: this.bookUnit[x].isbn+ ' '+this.bookUnit[x].book.title +' de '+this.bookUnit[x].book.author.firstName+' '+this.bookUnit[x].book.author.lastName+ ' '+this.bookUnit[x].publisher.name })
        }
        for (let x = 0; x < this.discounts.length; x++) {
            this.discountsAfterParseClass.push({ id: this.discounts[x].id, name: this.discounts[x].description+ ' '+this.discounts[x].value })
       }

     
         if(saleId!=null){
            this._saleService.getSaleForEdit(saleId).subscribe((result) => {
                this.sale = result;
                this.selectedCustomer.push(this.sale.customerId);
                this.selectedStore.push(this.sale.storeId);
                this.modal.show();
            });

            this.getSaleDetails(saleId);
            this.getDiscountSale(saleId);
            

         }
       
     }
 
     getSaleDetails(saleId): void {
        let dis = this;
        this._saleService.getSaleDetail(saleId).subscribe((result) => {
            this.saleDetailView = result;

            dis.saleDetailView.forEach(element => {
                dis.selectedBook.push(element.bookUnitId);

            });

        });
    }



    getDiscountSale(saleId): void {
        let dis = this;
        this._saleService.getDiscountSale(saleId).subscribe((result) => {
            this.discountSaleView = result;

            dis.discountSaleView.forEach(element => {
                dis.selectedDiscount.push(element.discountId);

            });

        });
    }


     getCustomers(): void {

        this._customerService.getCustomer('').subscribe((result) => {
            this.customers = result.items;
        });

    }
   
    getDiscounts(): void {

        this._discountsService.getDiscounts('').subscribe((result) => {
            this.discounts = result.items;
        });

    }

    
    getBooks(): void {

        this._bookUnitService.getBookUnit('').subscribe((result) => {
            this.bookUnit = result.items;
        });

    }

    getStores(): void {

        this._storeService.getStore('').subscribe((result) => {
            this.stores = result.items;
        });

    }

    onChangeService(e): void {
        this.selectedBooks=this.bookUnit.filter(val => this.selectedBook.includes(val.id));
        console.log(this.qtty);

    }

   
    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

   
   

btnClick_goToStore= function () {
    this.router.navigateByUrl('/app/main/store');
};

btnClick_goToDiscount= function () {
    this.router.navigateByUrl('/app/main/discount');
};

btnClick_goToCustomer= function () {
    this.router.navigateByUrl('/app/main/customer');
};


    save(): void {
        this.saving = true;      
        if (this.selectedStore[0] == null) {
            this.saving = false;
            this.notify.info(this.l('Store is required'));
            return;
        }
        let hs=this;   
        this.spinnerService.show();
        this.sale.customerId = this.selectedCustomer[0];
        this.sale.storeId = this.selectedStore[0];
        this.addNewDetail();
        this._saleService.createSale(this.sale)
            .pipe(finalize(() => this.saving = false))
            .subscribe(result => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.sale);               
                hs.insertedSaleId=result;
                this.addNewDiscount();


            });

    }

    addNewDetail(): void{
        let dis=this;
       
        dis.selectedBook.forEach(async function(det){
            let newDetail=new CreateSaleDetailInput();
            newDetail.bookUnitId=det;
            newDetail.saleId=dis.insertedSaleId;
            newDetail.qtty=Number((<HTMLInputElement>document.getElementById("item-"+det)).value);
            dis.newList.push(newDetail);
            // dis._saleService.createSaleDetail(dis.saleDetail) 
            // .pipe(finalize(() => dis.saving = false))
            // .subscribe(() => {
            //     dis.modalSave.emit(dis.saleDetail);
            // });



        } );
    this.sale.details=this.newList;
    }

    addNewDiscount(): void{
        let dis=this;
        dis.selectedDiscount.forEach(function(det){
            dis.discountSale=new CreateDiscountSaleInput();
            dis.discountSale.discountId=det;
            dis.discountSale.saleId=dis.insertedSaleId;
            dis._saleService.createDiscountSale(dis.discountSale) 
            .pipe(finalize(() => dis.saving = false))
            .subscribe(() => {
                dis.modalSave.emit(dis.discountSale);
            });



        } );

    }

    close(): void {
        this.modal.hide();
        this.selectedBooks=[];
        this.active = false;
    }
}