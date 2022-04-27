import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { BookServiceProxy, CreateBookCategoryInput, BookCategoryListDto, CategoryListDto,CategoryServiceProxy, CreateBookInput,AuthorListDto,AuthorServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { Router } from '@angular/router';


@Component({
    selector: 'createBookModal',
    templateUrl: './create-book-modal-component.html'
})
export class CreateBookModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    book: CreateBookInput = new CreateBookInput();
    bookCategory: CreateBookCategoryInput = new CreateBookCategoryInput();
    authors: AuthorListDto[] = [];
    categories: CategoryListDto[] = [];
    selectedCategories: number[] = [];
    selectedAuthor: number[] = [];
    insertedBookId:number;
    authorsAfterParseClass: IMultiSelectOption[] = [];
    categoriesAfterParseClass: IMultiSelectOption[] = [];
    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _bookService: BookServiceProxy,
        private _authorsService: AuthorServiceProxy,
        private _categoriesService: CategoryServiceProxy,
        private router : Router

    ) {
        super(injector);
    }

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
    show(): void {
        this.selectedAuthor=[];
        this.authorsAfterParseClass=[];
        this.selectedCategories=[];
        this.categoriesAfterParseClass=[];
        this.active = true;
        this.book = new CreateBookInput();
        this.bookCategory=new CreateBookCategoryInput();
        this.modal.show();
        for (let x = 0; x < this.authors.length; x++) {
            this.authorsAfterParseClass.push({ id: this.authors[x].id, name: this.authors[x].firstName+ ' '+this.authors[x].lastName })
        }
        for (let x = 0; x < this.categories.length; x++) {
            this.categoriesAfterParseClass.push({ id: this.categories[x].id, name: this.categories[x].name})
        }
    }
    btnClick_goToAuthor= function () {
        this.router.navigateByUrl('/app/main/author');
};

btnClick_goToCategory= function () {
    this.router.navigateByUrl('/app/main/categories');
};

    onShown(): void {
      // this.nameInput.nativeElement.focus();
    }

    ngOnInit(): void {
        this.getAuthors();
        this.getCategories();
    }

    getAuthors(): void {

        this._authorsService.getAuthor('').subscribe((result) => {
            this.authors = result.items;
        });

    }

    getCategories(): void {

        this._categoriesService.getCategories('').subscribe((result) => {
            this.categories = result.items;
        });

    }

    save(): void {
        this.saving = true;
        if (this.selectedAuthor[0] == null) {
            this.saving = false;
            this.notify.info(this.l('Author is required'));
            return;
        }
       let hs=this;
        this.book.authorId = this.selectedAuthor[0];
        this._bookService.createBook(this.book)
            .pipe(finalize(() => this.saving = false))
            .subscribe(result=> {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.book);
                hs.insertedBookId=result;
                this.addNewCategory();
            });

           
            
    }

    addNewCategory(): void{
        let dis=this;
        this.selectedCategories.forEach(function(categ){
            dis.bookCategory=new CreateBookCategoryInput();
            dis.bookCategory.categoryId=categ;
            dis.bookCategory.bookId=dis.insertedBookId;
            dis._bookService.createBookCategory(dis.bookCategory) 
            .pipe(finalize(() => dis.saving = false))
            .subscribe(() => {
                dis.modalSave.emit(dis.bookCategory);
            });



        } );

    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
