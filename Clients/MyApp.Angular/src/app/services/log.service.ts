import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LogsResponse } from '../models/log.model';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private apiUrl = `${environment.apiUrl}/logs`;

  constructor(private http: HttpClient) { }

  getLogs(pageNumber: number = 1, pageSize: number = 50): Observable<LogsResponse> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<LogsResponse>(this.apiUrl, { params });
  }

  getLogsByLevel(level: string, pageNumber: number = 1, pageSize: number = 50): Observable<any> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<any>(`${this.apiUrl}/level/${level}`, { params });
  }
}
