import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { BooksRoutingModule } from './books-routing.module';
import { BooksComponent } from './books.component';
import { CreateBookModalComponent } from './create-book-modal-component';
import { EditBookModalComponent } from './edit-book-modal-component'; 
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';


@NgModule({
    declarations: [BooksComponent, CreateBookModalComponent, EditBookModalComponent],
    imports: [AppSharedModule, BooksRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class BooksModule { }
