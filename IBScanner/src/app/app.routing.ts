import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { ManagementComponent } from 'app/areas/management/management.component';

const routes: Routes = [
  {
    path: 'management',
    component: ManagementComponent,
    children: [
      {
        path: '',
        loadChildren: './areas/management/management.module#ManagementModule'
      }]
  },
  {
    path: '**',
    redirectTo: 'management'
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
