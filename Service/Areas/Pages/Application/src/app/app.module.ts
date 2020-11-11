import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ManagementComponent } from 'app/areas/management/management.component';
import { ComponentsModule } from 'app/components/components.module';
import { AppRoutingModule } from 'app/app.routing';
import { AppComponent } from 'app/app.component';
import { ServicesModule } from 'app/services/services.module';
import { ManagementComponentsModule } from 'app/areas/management/components/components.module';

@NgModule({
  imports: [
    ManagementComponentsModule,
    ComponentsModule,
    ServicesModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    NgbModule.forRoot()
  ],
  declarations: [
    AppComponent,
    ManagementComponent
  ],
  bootstrap: [
    AppComponent
  ],
  providers: [{
    provide: APP_BASE_HREF,
    useValue: '/'
  }]
})

export class AppModule { }
