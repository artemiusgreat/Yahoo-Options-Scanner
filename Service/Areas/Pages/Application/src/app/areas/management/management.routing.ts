import { Routes } from '@angular/router';
import { DashboardComponent } from 'app/areas/management/dashboard/dashboard.component';
import { OptionsComponent } from 'app/areas/management/options/options.component';
import { UserProfileComponent } from 'app/areas/management/user-profile/user-profile.component';

export const ManagementRoutes: Routes = [
  { path: '', component: OptionsComponent },
  { path: 'user-profile', component: UserProfileComponent }
];
