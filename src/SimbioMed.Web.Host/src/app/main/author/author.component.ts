import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AuthorServiceProxy, AuthorListDto, ListResultDtoOfAuthorListDto,CityServiceProxy,CityListDto } from '@shared/service-proxies/service-proxies';
import { min, remove as _remove } from 'lodash-es';
import { DateTime } from 'luxon';

@Component({
    templateUrl: './author.component.html',
    animations: [appModuleAnimation()]
})

export class AuthorComponent extends AppComponentBase {
    author: AuthorListDto[] = [];
    filter: string = '';
    flagSortAscDesc = true;
    cities: CityListDto[] = [];


    constructor(
        injector: Injector,
        private _authorService: AuthorServiceProxy,
        private _cityService: CityServiceProxy

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getAuthor();
    }
    
  
    deleteAuthor(author: AuthorListDto): void {
       
        this.message.confirm(
            this.l(author.firstName+' '+author.lastName), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._authorService.deleteAuthor(author.id).subscribe(() => {
                        let waitResponse = _remove(this.author, author);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(author.firstName+' '+author.lastName + ' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(author.firstName+' '+author.lastName + ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

        getAuthor(): void {
        this._authorService.getAuthor(this.filter).subscribe((result) => {
            this.author = result.items;
        });
    }

    getAge(birthday:DateTime,deathday :DateTime)
    {
        var birthdayString=birthday.toString();
        var deathdayString=deathday.toString();
        var today = new Date();
        var birthDate = new Date(birthdayString);
        var deathDate = new Date(deathdayString);
        var age = deathDate.getFullYear() - birthDate.getFullYear();
        var m = deathDate.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && deathDate.getDate() < birthDate.getDate())) 
        {
            age--;
        }
        return age;
    }

    sortTable(fieldName: string): void {
        switch (fieldName) {
            case "firstName":
                if (this.flagSortAscDesc) {
                    this.author = this.author.sort((a, b) => (a.firstName.toLowerCase() > b.firstName.toLowerCase()) ? 1 : -1)
                } else {
                    this.author = this.author.sort((a, b) => (a.firstName.toLowerCase() < b.firstName.toLowerCase()) ? 1 : -1)
                }
                break;
            case "lastName":
                if (this.flagSortAscDesc) {
                    this.author = this.author.sort((a, b) => (a.lastName.toLowerCase() > b.lastName.toLowerCase()) ? 1 : -1)
                } else {
                    this.author = this.author.sort((a, b) => (a.lastName.toLowerCase() < b.lastName.toLowerCase()) ? 1 : -1)
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
