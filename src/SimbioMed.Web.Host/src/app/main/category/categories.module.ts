import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {CategoriesRoutingModule} from './categories-routing.module';
import {CategoriesComponent} from './categories.component';
import {CreateCategoryModalComponent} from './create-category-modal-component';
import {EditCategoryModalComponent} from './edit-category-modal-component';


@NgModule({
    declarations: [CategoriesComponent, CreateCategoryModalComponent,EditCategoryModalComponent],
    imports: [AppSharedModule, CategoriesRoutingModule]
})
export class CategoriesModule {}
