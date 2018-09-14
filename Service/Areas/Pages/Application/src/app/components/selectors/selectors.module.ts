import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NguiAutoCompleteModule } from '@ngui/auto-complete';
import { SelectorsComponent } from 'app/components/selectors/selectors.component';
import { SelectorsCaseComponent } from 'app/components/selectors/case.component';
import { SelectorsControlComponent } from 'app/components/selectors/controls/control/control.component';
import { SelectorsAutoCompleteComponent } from 'app/components/selectors/controls/autocomplete/autocomplete.component';
import { SelectorsDateComponent } from 'app/components/selectors/controls/date/date.component';
import { SelectorsDateRangeComponent } from 'app/components/selectors/controls/date-range/date-range.component';
import { SelectorsInputComponent } from 'app/components/selectors/controls/input/input.component';
import { SelectorsMultiSelectComponent } from 'app/components/selectors/controls/multiselect/multiselect.component';
import { SelectorsSelectComponent } from 'app/components/selectors/controls/select/select.component';
import { SelectorsCheckboxComponent } from 'app/components/selectors/controls/checkbox/checkbox.component';

@NgModule({
  declarations: [
    SelectorsComponent,
    SelectorsCaseComponent,
    SelectorsControlComponent,
    SelectorsAutoCompleteComponent,
    SelectorsDateComponent,
    SelectorsDateRangeComponent,
    SelectorsInputComponent,
    SelectorsCheckboxComponent,
    SelectorsSelectComponent,
    SelectorsMultiSelectComponent
  ],
  exports: [
    SelectorsComponent,
    SelectorsControlComponent,
    SelectorsCaseComponent,
    SelectorsAutoCompleteComponent,
    SelectorsDateComponent,
    SelectorsDateRangeComponent,
    SelectorsInputComponent,
    SelectorsCheckboxComponent,
    SelectorsSelectComponent,
    SelectorsMultiSelectComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    NguiAutoCompleteModule
  ]
})

export class SelectorsModule { }
