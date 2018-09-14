import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/components/base.component';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})

export class FooterComponent extends BaseComponent {

  test: Date = new Date();
}
