import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {StoreRoutingModule} from './store-routing.module';
import {StoreComponent} from './store.component';
import {CreateStoreModalComponent} from './create-store-modal-component';
import {EditStoreModalComponent} from './edit-store-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';



@NgModule({
     declarations: [StoreComponent, CreateStoreModalComponent,EditStoreModalComponent],
    imports: [AppSharedModule, StoreRoutingModule, MultiselectDropdownModule]
})
export class StoreModule {}
