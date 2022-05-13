import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'dashboard',
                        loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                        data: { permission: 'Pages.Tenant.Dashboard'},
                    },
                    {
                        path: 'books',
                        loadChildren: () => import('./books/books.module').then(m => m.BooksModule),
                        data: { permission: 'Pages_Tenant_Book_EditBook' }
                    },   
                    {
                        path: 'categories',
                        loadChildren: () => import('./category/categories.module').then(m => m.CategoriesModule),
                        data: { permission: 'Pages_Tenant_Categories_EditCategories' }
                    }, 
                    {
                        path: 'city',
                        loadChildren: () => import('./city/city.module').then(m => m.CityModule),
                        data: { permission: 'Pages_Tenant_City_EditCity' }

                    },
                    {
                        path: 'store',
                        loadChildren: () => import('./store/store.module').then(m => m.StoreModule),
                        data: { permission: 'Pages_Tenant_Store_EditStore' }

                    },
                    {
                        path: 'author',
                        loadChildren: () => import('./author/author.module').then(m => m.AuthorModule),
                        data: { permission: 'Pages_Tenant_Author_EditAuthor' }

                    },
                    {
                        path: 'publisher',
                        loadChildren: () => import('./publisher/publisher.module').then(m => m.PublisherModule),
                        data: { permission: 'Pages_Tenant_Publisher_EditPublisher' }

                    },
                    {
                        path: 'customer',
                        loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule),
                        data: { permission: 'Pages_Tenant_Customer_EditCustomer' }

                    },
                    {
                        path: 'bookUnit',
                        loadChildren: () => import('./bookUnit/bookUnit.module').then(m => m.BookUnitModule),
                        data: { permission: 'Pages.Tenant.BookUnit' }

                    },
                    {
                        path: 'discount',
                        loadChildren: () => import('./discount/discount.module').then(m => m.DiscountModule),
                        data: { permission: 'Pages_Tenant_Discount_EditDiscount' }

                    },
                    {
                        path: 'sale',
                        loadChildren: () => import('./sale/sale.module').then(m => m.SaleModule),
                        data: { permission: 'Pages.Tenant.Sale' }

                    },
                                         
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
