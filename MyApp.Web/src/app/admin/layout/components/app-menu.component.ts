import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
    selector: 'app-menu',
    template: `<ul class="layout-menu">
        <ng-container *ngFor="let item of model; let i = index">
            <li app-menuitem *ngIf="!item.separator" [item]="item" [index]="i" [root]="true"></li>
            <li *ngIf="item.separator" class="menu-separator"></li>
        </ng-container>
    </ul>`
})
export class AppMenu implements OnInit {
    model: MenuItem[] = [];

    ngOnInit() {
        this.model = [
            {
                label: 'Anasayfa',
                items: [
                    {
                        label: 'Dashboard',
                        icon: 'pi pi-fw pi-home',
                        routerLink: ['/admin'],
                        routerLinkActiveOptions: { paths: 'exact', queryParams: 'ignored', matrixParams: 'ignored', fragment: 'ignored' }
                    }
                ]
            },
            {
                label: 'Modüller',
                items: [
                    {
                        label: 'Loglar',
                        icon: 'pi pi-fw pi-list',
                        routerLink: ['/admin/logs']
                    },
                    {
                        label: 'Testler',
                        icon: 'pi pi-fw pi-check-circle',
                        routerLink: ['/admin/test']
                    }
                ]
            }
        ];
    }
}

