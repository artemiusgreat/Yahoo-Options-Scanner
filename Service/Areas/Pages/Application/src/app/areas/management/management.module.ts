import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { DashboardComponent } from 'app/areas/management/dashboard/dashboard.component';
import { UserProfileComponent } from 'app/areas/management/user-profile/user-profile.component';
import { OptionsComponent } from 'app/areas/management/options/options.component';
import { ManagementRoutes } from 'app/areas/management/management.routing';
import { ComponentsModule } from 'app/components/components.module';
import { ManagementComponentsModule } from 'app/areas/management/components/components.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { BaseComponent } from 'app/components/base.component';
import { MapsComponent } from 'app/areas/management/maps/maps.component';
import { HighchartsChartModule } from 'highcharts-angular';

@NgModule({
  imports: [
    HighchartsChartModule,
    ManagementComponentsModule,
    ComponentsModule,
    CommonModule,
    RouterModule.forChild(ManagementRoutes),
    FormsModule,
    ChartsModule,
    NgbModule,
    Ng2SmartTableModule,
    ToastrModule.forRoot()
  ],
  declarations: [
    BaseComponent,
    MapsComponent,
    DashboardComponent,
    UserProfileComponent,
    OptionsComponent
  ],
  providers: [
  ]
})

export class ManagementModule {}
