import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BookServiceProxy, BookCategoryListDto, BookListDto, ListResultDtoOfBookListDto, ListResultDtoOfDiscountListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
import { waitForAsync } from '@angular/core/testing';

@Component({
    templateUrl: './books.component.html',
    styleUrls: ['./books.component.less'],
    animations: [appModuleAnimation()]
})
export class BooksComponent extends AppComponentBase implements OnInit {

    books: BookListDto[] = [];
    categories: BookCategoryListDto[] = [];
    filter: string = '';
    defaultRows = 15;
    flagSortAscDesc = true
     listOfCategs = [];
     categ:string;

    constructor(
        injector: Injector,
        private _bookService: BookServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getBooks();
        // let queriedBooks = this.getBooks();
        // queriedBooks.then(function (resultedBooks) {  
        //     console.log("Final result:", resultedBooks)
        // })
        // Promise.all([queriedBooks]).then((values) => {
        //     console.log("Smr fam mea:", values);
        // });
    }

    deleteBook(book: BookListDto): void {
        this.message.confirm(
            this.l(book.title), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._bookService.deleteBook(book.id).subscribe(() => {
                        let waitResponse = _remove(this.books, book);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l('Book ' + book.title + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l('Book ' + book.title + ' cannot be deleted.'));
                        }
                    });
                }
            }
        );
    }


    getBooks(): void {
        
        this._bookService.getBooks(this.filter).subscribe((result) => {
            this.books = result.items;
            this.listOfCategs=[];
            this.books.forEach(element => {
                element.categories.forEach(elem => {
                    this.listOfCategs.push({ id: element.id, categories: elem.category.name })
                });


            });
        
            
        
        });
    }

    getListOfCategs(id):string{
        this.categ="";
        this.categ= this.listOfCategs.filter(val => val.id == id).map(o => o.categories).join(',');
        console.log(this.categ);
        return this.categ;

    }

    // async getBooks(): Promise<void> {

    // let jj = this._bookService.getBookCategory("36").toPromise().then(function (result) {
    //     console.log("dsadsa:", result)
    // })
    // console.log("kkk:", jj)
    // let myLocalBooks = await this._bookService
    //     .getBooks(this.filter)
    //     .toPromise()
    //     .then( async function (receivedBooks) {
    //         let myLocalCategories = []
    //         for (let index = 0; index < receivedBooks.items.length; index++) {
    //             myLocalCategories.push(receivedBooks.items[index].categories)
    //   let bookCateg=  await _this._bookService.getBookCategory(""+receivedBooks.items[index].id).toPromise() ;
    //     let auxCategory =  _this._bookService
    //         .getBookCategory("" + receivedBooks.items[index].id).toPromise().then(function(res) {
    //             console.log("Result jajaja:", res)
    //         })
    //     if (auxCategory != null) {
    //         myLocalCategories.push(auxCategory)
    //     }
    //                 Promise.all(myLocalCategories).then((values) => {
    //                     console.log("Smr fam mea:", values);
    //                 });
    //            }
    //         })

    // }

    // async getBooksCategories(): Promise<void> {
    //     let myLocalCategories;
    //     for (let index = 0; index < this.books.length; index++) {
    //         let auxCategory = await this._bookService
    //             .getBookCategory(this.books[index].id.toString()).toPromise()
    //         myLocalCategories.push(auxCategory)
    //         console.log("CATEGORYYY:", auxCategory)
    //     }
    //     return myLocalCategories;
    // }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "title":
                if (this.flagSortAscDesc) {
                    this.books = this.books.sort((a, b) => (a.title.toLowerCase() > b.title.toLowerCase()) ? 1 : -1)
                } else {
                    this.books = this.books.sort((a, b) => (a.title.toLowerCase() < b.title.toLowerCase()) ? 1 : -1)
                }
                break;
            case "author":
                if (this.flagSortAscDesc) {
                    this.books = this.books.sort((a, b) => (a.author.lastName.toLowerCase() > b.author.lastName.toLowerCase()) ? 1 : -1)
                } else {
                    this.books = this.books.sort((a, b) => (a.author.lastName.toLowerCase() < b.author.lastName.toLowerCase()) ? 1 : -1)
                }
                break;
            default:
                console.log(
                    "Sorting field doesn't exist"
                )
        }
        this.flagSortAscDesc = !this.flagSortAscDesc

    }
}
