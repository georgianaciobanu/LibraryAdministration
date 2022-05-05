import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { DiscountServiceProxy, CreateDiscountInput,EditDiscountInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'createOrEditDiscountModal',
    templateUrl: './create-or-edit-discount-modal-component.html'
})
export class CreateOrEditDiscountModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    discount: CreateDiscountInput = new CreateDiscountInput();
    discountEdit: EditDiscountInput = new EditDiscountInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _discountService: DiscountServiceProxy
    ) {
        super(injector);
    }

    show(discountId): void {
        this.active = true;
        this.discount = new CreateDiscountInput();
       if(discountId!=null){
        this._discountService.getDiscountForEdit(discountId).subscribe((result)=>{
            this.discount=result;
            this.discountEdit=result;
            this.modal.show(); 
          });
        }
        this.modal.show();
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        if(this.discountEdit.id!=null ){
            this.saveForEdit();
        }
        else{
            this.saveForCreate();
        }

    }
    saveForCreate(): void {
        this.saving = true;
        this._discountService.createDiscount(this.discount)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.discount);
            });
    }

    saveForEdit(): void {
        this.spinnerService.show();
        this.saving = true;
        this._discountService.editDiscount(this.discountEdit)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.discountEdit);
            });

            this.saving = false;
    }


    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
