import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { CategoryServiceProxy, CreateCategoryInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'createCategoryModal',
    templateUrl: './create-category-modal-component.html'
})
export class CreateCategoryModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    category: CreateCategoryInput = new CreateCategoryInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _categoriesService: CategoryServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.category = new CreateCategoryInput();
        this.modal.show();
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._categoriesService.createCategory(this.category)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.category);
            });
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
