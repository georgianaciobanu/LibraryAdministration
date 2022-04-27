import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AuthorRoutingModule} from './author-routing.module';
import {AuthorComponent} from './author.component';
import {CreateAuthorModalComponent} from './create-author-modal-component';
import {EditAuthorModalComponent} from './edit-author-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';





@NgModule({
     declarations: [AuthorComponent, CreateAuthorModalComponent,EditAuthorModalComponent],
    imports: [AppSharedModule, AuthorRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule]
})
export class AuthorModule {}
