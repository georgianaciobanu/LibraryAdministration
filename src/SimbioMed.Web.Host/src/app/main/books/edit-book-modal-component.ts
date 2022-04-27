import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { AuthorServiceProxy, BookCategoryListDto, CategoryListDto, CreateBookCategoryInput, CategoryServiceProxy, AuthorListDto, BookServiceProxy, CreateBookInput, EditBookInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { Router } from '@angular/router';


@Component({
    selector: 'editBookModal',
    templateUrl: './edit-book-modal-component.html'
})
export class EditBookModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal', { static: false }) modal: ModalDirective;
    @ViewChild('nameInput', { static: false }) nameInput: ElementRef;

    book: EditBookInput = new EditBookInput();
    bookCategory: BookCategoryListDto[] = [];
    bookCat: CreateBookCategoryInput = new CreateBookCategoryInput();
    selectedAuthor: number[] = [];
    authors: AuthorListDto[] = [];
    categories: CategoryListDto[] = [];
    selectedCategories: number[] = [];
    categoriesAfterParseClass: IMultiSelectOption[] = [];
    authorsAfterParseClass: IMultiSelectOption[] = [];
    active: boolean = false;
    saving: boolean = false;

    chooseone: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'fontawesome',
        buttonClasses: 'form-control',
        selectionLimit: 1,
        autoUnselect: true,
        closeOnSelect: true
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
        private _bookService: BookServiceProxy,
        private _authorService: AuthorServiceProxy,
        private _categoriesService: CategoryServiceProxy,
        private router: Router

    ) {
        super(injector);
    }

    show(bookId): void {
        this.active = true;
        this.selectedAuthor = [];
        this.authorsAfterParseClass = [];
        this.selectedCategories = [];
        this.categoriesAfterParseClass = [];
        for (let x = 0; x < this.authors.length; x++) {
            this.authorsAfterParseClass.push({ id: this.authors[x].id, name: this.authors[x].firstName + ' ' + this.authors[x].lastName })
        }
        for (let x = 0; x < this.categories.length; x++) {
            this.categoriesAfterParseClass.push({ id: this.categories[x].id, name: this.categories[x].name })
        }
        this.getBookCategories(bookId);


        this._bookService.getBookForEdit(bookId).subscribe((result) => {
            this.book = result;
            this.selectedAuthor.push(this.book.authorId);
            this.modal.show();


        });






    }


    getBookCategories(bookId): void {
        let dis = this;
        this._bookService.getBookCategory(bookId).subscribe((result) => {
            this.bookCategory = result.items;

            dis.bookCategory.forEach(element => {
                dis.selectedCategories.push(element.categoryId);

            });

        });
    }

    ngOnInit(): void {
        this.getAuthors();
        this.getCategories();

    }

    btnClick_goToAuthor = function () {
        this.router.navigateByUrl('/app/main/author');
    };

    btnClick_goToCategory = function () {
        this.router.navigateByUrl('/app/main/categories');
    };
    getAuthors(): void {

        this._authorService.getAuthor('').subscribe((result) => {
            this.authors = result.items;
        });

    }
    onShown(): void {
        //this.nameInput.nativeElement.focus();
    }

    getCategories(): void {

        this._categoriesService.getCategories('').subscribe((result) => {
            this.categories = result.items;
        });

    }


    save(): void {
        this.spinnerService.show();
        this.saving = true;
        this.book.authorId = this.selectedAuthor[0];
        this._bookService.editBook(this.book)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('Saved Successfully'));
                this.close();
                this.spinnerService.hide();
                this.updateCategoryList();
                this.modalSave.emit(this.book);

            });

        this.saving = false;

    }


    async updateCategoryList(): Promise<void> {
        let dis = this;
        this.bookCategory.forEach(function (categ) {
            dis._bookService.deleteBookCategory(categ.id).subscribe(() => {
            });

        });
        await new Promise(f => setTimeout(f, 2000));


        this.selectedCategories.forEach(function (categ) {
            dis.bookCat = new CreateBookCategoryInput();
            dis.bookCat.categoryId = categ;
            dis.bookCat.bookId = dis.book.id;

            dis._bookService.createBookCategory(dis.bookCat)
                .pipe(finalize(() => dis.saving = false))
                .subscribe(() => {
                    dis.modalSave.emit(dis.bookCategory);
                });



        });
        this.selectedCategories = [];

    }


    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
