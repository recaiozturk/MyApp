import { Component, ElementRef } from '@angular/core';

@Component({
    selector: 'app-sidebar',
    template: `<div class="layout-sidebar">
        <app-menu></app-menu>
    </div>`
})
export class AppSidebar {
    constructor(public el: ElementRef) {}
}

