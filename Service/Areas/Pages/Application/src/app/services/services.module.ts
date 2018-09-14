import { NgModule } from '@angular/core';
import { OptionsService } from "app/services/options.service";
import { BaseService } from 'app/services/base.service';

@NgModule({
    declarations: [],
    imports: [],
    bootstrap: [],
    exports: [],
    providers: [
      BaseService,
      OptionsService
    ]
})

export class ServicesModule { }