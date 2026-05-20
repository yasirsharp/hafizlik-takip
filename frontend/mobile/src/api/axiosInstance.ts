import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

// Localhost Backend API URL
// Note: Use your machine's local IP address instead of localhost if running on physical device
export const API_BASE_URL = 'http://localhost:5190/api'; 

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});

api.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('@auth_token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response && error.response.status === 401) {
      await AsyncStorage.removeItem('@auth_token');
      // TODO: Handle navigation to Login
    }
    return Promise.reject(error);
  }
);

export default api;
