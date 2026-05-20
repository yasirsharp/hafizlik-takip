import { create } from 'zustand';
import { createJSONStorage, persist } from 'zustand/middleware';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '../types/auth.types';

interface AuthState {
  user: User | null;
  token: string | null;
  tenantId: number | null;
  isLoading: boolean;
  
  // Actions
  login: (token: string, user: User, tenantId?: number) => void;
  logout: () => void;
  setTenant: (tenantId: number) => void;
  setLoading: (isLoading: boolean) => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      token: null,
      tenantId: null,
      isLoading: false,

      login: (token, user, tenantId) => 
        set({ token, user, tenantId: tenantId ?? user.tenantId ?? null, isLoading: false }),
        
      logout: () => 
        set({ user: null, token: null, tenantId: null }),
        
      setTenant: (tenantId) => 
        set({ tenantId }),
        
      setLoading: (isLoading) => 
        set({ isLoading }),
    }),
    {
      name: 'auth-storage',
      storage: createJSONStorage(() => AsyncStorage),
    }
  )
);
