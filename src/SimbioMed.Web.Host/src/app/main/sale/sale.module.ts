import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {SaleRoutingModule} from './sale-routing.module';
import {SaleComponent} from './sale.component';
import {CreateOrEditSaleModalComponent} from './create-or-edit-sale-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';





@NgModule({
     declarations: [SaleComponent,CreateOrEditSaleModalComponent],
    imports: [AppSharedModule, SaleRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule]
})
export class SaleModule {}
