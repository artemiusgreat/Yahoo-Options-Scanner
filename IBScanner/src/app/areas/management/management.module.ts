import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { DashboardComponent } from 'app/areas/management/dashboard/dashboard.component';
import { UserProfileComponent } from 'app/areas/management/user-profile/user-profile.component';
import { TableListComponent } from 'app/areas/management/table-list/table-list.component';
import { OptionsComponent } from 'app/areas/management/options/options.component';
import { StocksComponent } from 'app/areas/management/stocks/stocks.component';
import { TypographyComponent } from 'app/areas/management/typography/typography.component';
import { IconsComponent } from 'app/areas/management/icons/icons.component';
import { NotificationsComponent } from 'app/areas/management/notifications/notifications.component';
import { ManagementRoutes } from 'app/areas/management/management.routing';
import { ComponentsModule } from 'app/components/components.module';
import { ManagementComponentsModule } from 'app/areas/management/components/components.module';
import { OptionsLoaderComponent } from 'app/areas/management/options-loader/options-loader.component';

@NgModule({
  imports: [
    ManagementComponentsModule,
    ComponentsModule,
    CommonModule,
    RouterModule.forChild(ManagementRoutes),
    FormsModule,
    ChartsModule,
    NgbModule,
    ToastrModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    OptionsComponent,
    OptionsLoaderComponent,
    StocksComponent,
    TypographyComponent,
    IconsComponent,
    NotificationsComponent
  ],
  providers: [
  ]
})

export class ManagementModule {}
