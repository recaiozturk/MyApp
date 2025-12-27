import { Component, OnInit } from '@angular/core';
import { Log, LogsResponse } from '../../../../../models/log.model';
import { LogService } from '../../../../../services/log.service';

@Component({
  selector: 'app-logs-list',
  templateUrl: './logs-list.component.html',
  styleUrls: ['./logs-list.component.scss']
})
export class LogsListComponent implements OnInit {
  logs: Log[] = [];
  selectedLog: Log | null = null;
  dialogVisible: boolean = false;
  
  totalRecords: number = 0;
  first: number = 0;
  rows: number = 50;
  loading: boolean = false;

  selectedLevel: string = 'All';
  levels: any[] = [
    { label: 'Tümü', value: 'All' },
    { label: 'Error', value: 'Error' },
    { label: 'Warning', value: 'Warning' },
    { label: 'Information', value: 'Information' },
    { label: 'Debug', value: 'Debug' }
  ];

  constructor(private logService: LogService) {}

  ngOnInit(): void {
    this.loadLogs();
  }

  loadLogs(): void {
    this.loading = true;
    const pageNumber = (this.first / this.rows) + 1;

    if (this.selectedLevel === 'All') {
      this.logService.getLogs(pageNumber, this.rows).subscribe({
        next: (response: LogsResponse) => {
          this.logs = response.data;
          this.totalRecords = response.totalCount;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading logs:', error);
          this.loading = false;
        }
      });
    } else {
      this.logService.getLogsByLevel(this.selectedLevel, pageNumber, this.rows).subscribe({
        next: (response: any) => {
          this.logs = response.data;
          this.totalRecords = response.totalCount || response.data.length;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading logs by level:', error);
          this.loading = false;
        }
      });
    }
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.rows = event.rows;
    this.loadLogs();
  }

  onLevelChange(): void {
    this.first = 0;
    this.loadLogs();
  }

  viewLog(log: Log): void {
    this.selectedLog = log;
    this.dialogVisible = true;
  }

  getSeverity(level: string): "success" | "secondary" | "info" | "warn" | "danger" | "contrast" | undefined {
    switch (level.toLowerCase()) {
      case 'error':
        return 'danger';
      case 'warning':
        return 'warn';
      case 'information':
        return 'info';
      case 'debug':
        return undefined;
      default:
        return undefined;
    }
  }

  formatDate(dateString: string | undefined): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return new Intl.DateTimeFormat('tr-TR', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    }).format(date);
  }
}

