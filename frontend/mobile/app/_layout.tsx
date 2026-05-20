import { useEffect } from 'react';
import { Slot, useRouter, useSegments } from 'expo-router';
import { useAuthStore } from '../src/store/useAuthStore';
import { View, ActivityIndicator } from 'react-native';

import '../global.css'; // NativeWind v4 için gerekli css dosyası referansı varsayımı, gerekmiyorsa kaldırılabilir

export default function RootLayout() {
  const { token, isLoading } = useAuthStore();
  const segments = useSegments();
  const router = useRouter();

  useEffect(() => {
    // Navigasyon için mount olup olmadığından emin olduktan sonra:
    const inAuthGroup = segments[0] === '(auth)';

    if (!token && !inAuthGroup) {
      // Token yok, auth dışı bir yerdeyse (örneğin root ya da tabs) -> login'e at
      router.replace('/(auth)/login');
    } else if (token && inAuthGroup) {
      // Token var, ama auth sayfasındaysa -> ana sayfaya at
      router.replace('/(tabs)');
    }
  }, [token, segments, router]);

  if (isLoading) {
    return (
      <View className="flex-1 items-center justify-center bg-gray-50">
        <ActivityIndicator size="large" color="#4F46E5" />
      </View>
    );
  }

  return <Slot />;
}
