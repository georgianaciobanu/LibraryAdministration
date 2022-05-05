import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {DiscountRoutingModule} from './discount-routing.module';
import {DiscountComponent} from './discount.component';
import {CreateOrEditDiscountModalComponent} from './create-or-edit-discount-modal-component';



@NgModule({
    declarations: [DiscountComponent, CreateOrEditDiscountModalComponent],
    imports: [AppSharedModule, DiscountRoutingModule]
})
export class DiscountModule {}
