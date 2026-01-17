export interface Log {
  id: number;
  application: string;
  logged: string;
  level: string;
  message: string;
  logger?: string;
  callsite?: string;
  exception?: string;
  properties?: string;
}

export interface LogsResponse {
  data: Log[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}
