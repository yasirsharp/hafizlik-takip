import api from './axiosInstance';
import { IDataResult } from '../types/api.types';
import { UserForLoginDto, UserForRegisterDto, AccessToken } from '../types/auth.types';

export const authApi = {
  login: async (credentials: UserForLoginDto) => {
    const response = await api.post<IDataResult<AccessToken>>('/auth/login', credentials);
    return response.data;
  },
  
  register: async (data: UserForRegisterDto) => {
    const response = await api.post<IDataResult<AccessToken>>('/auth/register', data);
    return response.data;
  }
};
