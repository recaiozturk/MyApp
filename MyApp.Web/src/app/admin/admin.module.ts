import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';

// PrimeNG Modules
import { SidebarModule } from 'primeng/sidebar';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { CardModule } from 'primeng/card';
import { PanelModule } from 'primeng/panel';
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';
import { SelectButtonModule } from 'primeng/selectbutton';

// Layout Components
import { AppTopbar } from './layout/components/app-topbar.component';
import { AppSidebar } from './layout/components/app-sidebar.component';
import { AppMenu } from './layout/components/app-menu.component';
import { AppMenuitem } from './layout/components/app-menuitem.component';
import { AppFooter } from './layout/components/app-footer.component';
import { AppConfigurator } from './layout/components/app-configurator.component';

@NgModule({
  declarations: [
    AdminComponent,
    AppTopbar,
    AppSidebar,
    AppMenu,
    AppMenuitem,
    AppFooter,
    AppConfigurator
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    AdminRoutingModule,
    // PrimeNG Modules
    SidebarModule,
    MenuModule,
    MenubarModule,
    ButtonModule,
    TableModule,
    ToolbarModule,
    CardModule,
    PanelModule,
    RippleModule,
    StyleClassModule,
    SelectButtonModule
  ],
  exports: [
    AppTopbar,
    AppSidebar,
    AppMenu,
    AppMenuitem,
    AppFooter
  ]
})
export class AdminModule { }
