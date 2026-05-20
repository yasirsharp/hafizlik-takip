export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  status: boolean;
  tenantId: number;
  role: string;
}

export interface AccessToken {
  token: string;
  expiration: string;
}

export interface UserForLoginDto {
  email: string;
  password?: string;
}

export interface UserForRegisterDto {
  email: string;
  password?: string;
  firstName: string;
  lastName: string;
}
