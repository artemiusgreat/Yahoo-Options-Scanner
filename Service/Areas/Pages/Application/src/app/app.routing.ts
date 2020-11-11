import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { ManagementComponent } from 'app/areas/management/management.component';
import { ManagementModule } from 'app/areas/management/management.module';

const routes: Routes = [
  {
    path: '',
    component: ManagementComponent,
    children: [{
      path: '',
      loadChildren: './areas/management/management.module#ManagementModule'
    }]
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ]
})

export class AppRoutingModule { }
