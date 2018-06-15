import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/components/base.component';

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: '/management', title: 'Dashboard', icon: 'design_app', class: '' },
  { path: '/management/options', title: 'Options', icon: 'design_bullet-list-67', class: '' },
  { path: '/management/options-loader', title: 'Options Loader', icon: 'design_bullet-list-67', class: '' },
  { path: '/management/stocks', title: 'Stocks', icon: 'education_atom', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent extends BaseComponent implements OnInit {

  menuItems: any[];

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }

  isMobileMenu() {
    return window.innerWidth < 991;
  };
}
