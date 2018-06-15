import { Routes } from '@angular/router';
import { DashboardComponent } from 'app/areas/management/dashboard/dashboard.component';
import { OptionsComponent } from 'app/areas/management/options/options.component';
import { StocksComponent } from 'app/areas/management/stocks/stocks.component';
import { UserProfileComponent } from 'app/areas/management/user-profile/user-profile.component';
import { TableListComponent } from 'app/areas/management/table-list/table-list.component';
import { TypographyComponent } from 'app/areas/management/typography/typography.component';
import { IconsComponent } from 'app/areas/management/icons/icons.component';
import { NotificationsComponent } from 'app/areas/management/notifications/notifications.component';
import { OptionsLoaderComponent } from 'app/areas/management/options-loader/options-loader.component';

export const ManagementRoutes: Routes = [
  { path: 'options', component: OptionsComponent },
  { path: 'options-loader', component: OptionsLoaderComponent },
  { path: 'stocks', component: StocksComponent },
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'table-list', component: TableListComponent },
  { path: 'typography', component: TypographyComponent },
  { path: 'icons', component: IconsComponent },
  { path: 'notifications', component: NotificationsComponent },
  { path: '', component: DashboardComponent }
];
