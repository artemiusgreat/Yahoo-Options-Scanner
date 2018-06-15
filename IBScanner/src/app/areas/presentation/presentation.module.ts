import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { ComponentsModule } from 'app/components/components.module';
import { AreasPresentationRoutes } from 'app/areas/presentation/presentation.routing';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AreasPresentationRoutes),
    FormsModule,
    ChartsModule,
    NgbModule,
    ComponentsModule,
    ToastrModule.forRoot()
  ],
  declarations: [
  ]
})

export class AreasPresentationModule {}
