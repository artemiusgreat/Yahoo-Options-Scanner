import { NgModule } from '@angular/core';
import { SelectorsModule } from 'app/components/selectors/selectors.module';
import { PaginationModule } from 'app/components/pagination/pagination.module';

@NgModule({
  exports: [
    SelectorsModule,
    PaginationModule
  ]
})

export class ComponentsModule { }
