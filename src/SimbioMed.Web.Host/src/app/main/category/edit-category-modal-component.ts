import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CategoryServiceProxy, CreateCategoryInput, EditCategoryInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { result } from 'lodash-es';

@Component({
    selector: 'editCategoryModal',
    templateUrl: './edit-category-modal-component.html'
})
export class EditCategoryModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    category: EditCategoryInput = new EditCategoryInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _categoriesService: CategoryServiceProxy
    ) {
        super(injector);
    }

    show(catId): void {
        this.active = true;
        this._categoriesService.getCategoryForEdit(catId).subscribe((result)=>{
          this.category=result;
          this.modal.show(); 
        });
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.spinnerService.show();
        this.saving = true;
        this._categoriesService.editCategory(this.category)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.spinnerService.hide();
                this.modalSave.emit(this.category);
            });

            this.saving = false;
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
