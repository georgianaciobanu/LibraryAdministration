import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {CustomerRoutingModule} from './customer-routing.module';
import {CustomerComponent} from './customer.component';
import {CreateCustomerModalComponent} from './create-customer-modal-component';
import {EditCustomerModalComponent} from './edit-customer-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';




@NgModule({
     declarations: [CustomerComponent, CreateCustomerModalComponent,EditCustomerModalComponent],
    imports: [AppSharedModule, CustomerRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule]
})
export class CustomerModule {}
