import { Component, Injector,ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BookUnitServiceProxy, BookUnitListDto, ListResultDtoOfBookUnitListDto } from '@shared/service-proxies/service-proxies';
import {  remove as _remove } from 'lodash-es';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';


//import { FileUpload } from 'primeng/fileupload';
//import { finalize } from 'rxjs/operators';
import {saveAs} from 'file-saver';




@Component({
    templateUrl: './bookUnit.component.html',
    animations: [appModuleAnimation()]
})

export class BookUnitComponent extends AppComponentBase {
    bookUnit: BookUnitListDto[] = [];
    filter: string = '';
    //uploadUrl: string;
    fileName = '';
   // @ViewChild('ExcelFileUpload', { static: false }) excelFileUpload: FileUpload;
     



    constructor(
        injector: Injector,
        private _bookUnitService: BookUnitServiceProxy,
        private _httpClient: HttpClient,

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getBookUnit();   
    }

    
    // uploadExcel(data: { files: File }): void {
        // const formData: FormData = new FormData();
        // const file = data.files[0];
        // formData.append('file', file, file.name);

        // this._httpClient
        //     .post<any>(this.uploadUrl, formData)
        //     .pipe(finalize(() => this.excelFileUpload.clear()))
        //     .subscribe((response) => {
        //         if (response.success) {
        //             this.notify.success(this.l('ImportUsersProcessStart'));
        //         } else if (response.error != null) {
        //             this.notify.error(this.l('ImportUsersUploadFailed'));
        //         }
        //     });
   // }

    // onUploadExcelError(): void {
    //     this.notify.error(this.l('ImportUsersUploadFailed'));
    // }
  

    onFileSelected(event) {

        const file:File = event.target.files[0];

        if (file) {

            this.fileName = file.name;

            const formData = new FormData();

            formData.append("thumbnail", file);

            const upload$ = this._httpClient.post(AppConsts.remoteServiceBaseUrl + '/Users/ImportFromExcel', formData);

            upload$.subscribe();

            this.ngOnInit();
            window.location.reload()
        }
    }

    
    deleteBookUnit(bookUnit: BookUnitListDto): void {
       
        this.message.confirm(
            this.l(bookUnit.isbn+' '), "Do you want to delete?",
            isConfirmed => {
                if (isConfirmed) {
                    this._bookUnitService.deleteBookUnit(bookUnit.id).subscribe(() => {
                        let waitResponse = _remove(this.bookUnit, bookUnit);
                        if (waitResponse.length == 1) {
                            this.notify.info(this.l(bookUnit.isbn+' was successfuly deleted.'));
                        } else {
                            this.notify.error(this.l(bookUnit.isbn+ ' cannot be deleted.')); 
                        }
                    });
                }
            }
        );
        }

        getBookUnit(): void {
        this._bookUnitService.getBookUnit(this.filter).subscribe((result) => {
            this.bookUnit = result.items;
        });
    }

    
   

}
