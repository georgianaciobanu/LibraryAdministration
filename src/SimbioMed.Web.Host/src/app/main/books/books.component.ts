import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BookServiceProxy, BookCategoryListDto, BookListDto, ListResultDtoOfBookListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';
import { waitForAsync } from '@angular/core/testing';

@Component({
    templateUrl: './books.component.html',
    styleUrls: ['./books.component.less'],
    animations: [appModuleAnimation()]
})
export class BooksComponent extends AppComponentBase implements OnInit {

    books: BookListDto[] = [];
    categories: BookCategoryListDto[]=[];
    filter: string = '';
    defaultRows = 15;
    flagSortAscDesc = true

    constructor(
        injector: Injector,
        private _bookService: BookServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
          this.getBooks();
          
      //   this.getBooksCategories();
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

    getBooks():void {
        this._bookService.getBooks(this.filter).subscribe((result) => {
            this.books = result.items;   

        });
       
       
  
   
}

 async getBooksCategories(): Promise<void>{
    await new Promise(f => setTimeout(f, 2000));

    this.books.forEach(element => {
        this._bookService.getBookCategory(element.id.toString()).subscribe(async (result)=>{
            if(result!=null && result.items!=null){
                element.categories=result.items; 
                this.categories=result.items;
            }
          
    });
});
}
    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "title":
                if (this.flagSortAscDesc) {
                    this.books = this.books.sort((a, b) => (a.title.toLowerCase() > b.title.toLowerCase()) ? 1 : -1)
                } else {
                    this.books = this.books.sort((a, b) => (a.title.toLowerCase() < b.title.toLowerCase()) ? 1 : -1)
                }
                break;
            // case "author":
            //     if (this.flagSortAscDesc) {
            //         this.books = this.books.sort((a, b) => (a.author.toLowerCase() > b.author.toLowerCase()) ? 1 : -1)
            //     } else {
            //         this.books = this.books.sort((a, b) => (a.author.toLowerCase() < b.author.toLowerCase()) ? 1 : -1)
            //     }
            //     break;
            // case "publishingHouse":
            //     if (this.flagSortAscDesc) {
            //         this.books = this.books.sort((a, b) => (a.publishingHouse.toLowerCase() > b.publishingHouse.toLowerCase()) ? 1 : -1)
            //     } else {
            //         this.books = this.books.sort((a, b) => (a.publishingHouse.toLowerCase() < b.publishingHouse.toLowerCase()) ? 1 : -1)
            //     }
            //     break;
            // case "pagesNumber":
            //     if (this.flagSortAscDesc) {
            //         this.books = this.books.sort((a, b) => (a.pagesNumber > b.pagesNumber) ? 1 : -1)
            //     } else {
            //         this.books = this.books.sort((a, b) => (a.pagesNumber < b.pagesNumber) ? 1 : -1)
            //     }
            //     break;
            default:
                console.log(
                    "Sorting field doesn't exist"
                )
        }
        this.flagSortAscDesc = !this.flagSortAscDesc

    }
}
