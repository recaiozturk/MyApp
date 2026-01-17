export interface LoginRequest {
  userName: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  expiration: string;
  userId: string;
  userName: string;
  email: string;
  firstName?: string;
  lastName?: string;
}

export interface User {
  userId: string;
  userName: string;
  email: string;
  firstName?: string;
  lastName?: string;
}
