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
                        data: { permission: 'Pages.Tenant.Dashboard' },
                    },
                    {
                        path: 'books',
                        loadChildren: () => import('./books/books.module').then(m => m.BooksModule),
                        data: { permission: 'Pages.Tenant.Book' }
                    },   
                    {
                        path: 'categories',
                        loadChildren: () => import('./category/categories.module').then(m => m.CategoriesModule)
                    }, 
                    {
                        path: 'city',
                        loadChildren: () => import('./city/city.module').then(m => m.CityModule)
                    },
                    {
                        path: 'store',
                        loadChildren: () => import('./store/store.module').then(m => m.StoreModule)
                    },
                    {
                        path: 'author',
                        loadChildren: () => import('./author/author.module').then(m => m.AuthorModule)
                    },
                    {
                        path: 'publisher',
                        loadChildren: () => import('./publisher/publisher.module').then(m => m.PublisherModule)
                    },
                    {
                        path: 'customer',
                        loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule)
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
