import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {PublisherRoutingModule} from './publisher-routing.module';
import {PublisherComponent} from './publisher.component';
import {CreatePublisherModalComponent} from './create-publisher-modal-component';
import {EditPublisherModalComponent} from './edit-publisher-modal-component';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule} from '@angular/material/core'
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';




@NgModule({
     declarations: [PublisherComponent, CreatePublisherModalComponent,EditPublisherModalComponent],
    imports: [AppSharedModule, PublisherRoutingModule, MultiselectDropdownModule,MatDatepickerModule,
        MatNativeDateModule, FormsModule,
        MatInputModule]
})
export class PublisherModule {}
