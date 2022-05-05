import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {BookUnitRoutingModule} from './bookUnit-routing.module';
import {BookUnitComponent} from './bookUnit.component';
import {CreateOrEditBookUnitModalComponent} from './create-or-edit-bookUnit-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';





@NgModule({
     declarations: [BookUnitComponent,CreateOrEditBookUnitModalComponent],
    imports: [AppSharedModule, BookUnitRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule]
})
export class BookUnitModule {}
