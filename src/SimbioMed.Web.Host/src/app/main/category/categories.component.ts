import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CategoryServiceProxy, CategoryListDto, ListResultDtoOfCategoryListDto } from '@shared/service-proxies/service-proxies';
import { remove as _remove } from 'lodash-es';

@Component({
    templateUrl: './categories.component.html',
    styleUrls: ['./categories.component.less'],
    animations: [appModuleAnimation()]
})
export class CategoriesComponent extends AppComponentBase implements OnInit {

    categories: CategoryListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;


    constructor(
        injector: Injector,
        private _categoryService: CategoryServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCategories();
    }
    
    deleteCategory(category: CategoryListDto): void {
       
        this.message.confirm(
            this.l(category.name), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._categoryService.deleteCategory(category.id).subscribe(() => {
                        let waitResponse = _remove(this.categories, category);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(category.name + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(category.name + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

    getCategories(): void {
        this._categoryService.getCategories(this.filter).subscribe((result) => {
            this.categories = result.items;
        });
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "name":
                if (this.flagSortAscDesc) {
                    this.categories = this.categories.sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase()) ? 1 : -1)
                } else {
                    this.categories = this.categories.sort((a, b) => (a.name.toLowerCase() < b.name.toLowerCase()) ? 1 : -1)
                }
                break;
            case "code":
                if (this.flagSortAscDesc) {
                    this.categories = this.categories.sort((a, b) => (a.code.toLowerCase() > b.code.toLowerCase()) ? 1 : -1)
                } else {
                    this.categories = this.categories.sort((a, b) => (a.code.toLowerCase() < b.code.toLowerCase()) ? 1 : -1)
                }
                break;
            default:
                console.log(
                    "Sorting field doesn't exist"
                )
        }
        this.flagSortAscDesc = !this.flagSortAscDesc

    }
    

}
