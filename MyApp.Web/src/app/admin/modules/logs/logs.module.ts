import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LogsRoutingModule } from './logs-routing.module';
import { LogsListComponent } from './components/logs-list/logs-list.component';

// PrimeNG Modules
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [
    LogsListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LogsRoutingModule,
    // PrimeNG Modules
    TableModule,
    ButtonModule,
    PaginatorModule,
    DropdownModule,
    DialogModule,
    TagModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule
  ]
})
export class LogsModule { }


