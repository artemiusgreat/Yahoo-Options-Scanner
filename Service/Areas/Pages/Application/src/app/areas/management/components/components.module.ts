import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SidebarComponent } from 'app/areas/management/components/sidebar/sidebar.component';
import { NavbarComponent } from 'app/areas/management/components/navbar/navbar.component';
import { FooterComponent } from 'app/areas/management/components/footer/footer.component';

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    NgbModule
  ],
  declarations: [
    SidebarComponent,
    NavbarComponent,
    FooterComponent
  ],
  exports: [
    SidebarComponent,
    NavbarComponent,
    FooterComponent
  ]
})

export class ManagementComponentsModule {}
