import React, { useState } from 'react';
import { 
  View, 
  Text, 
  TextInput, 
  TouchableOpacity, 
  KeyboardAvoidingView, 
  Platform,
  ActivityIndicator,
  Alert
} from 'react-native';
import { BlurView } from 'expo-blur';
import { useRouter } from 'expo-router';
import { useAuthStore } from '../../src/store/useAuthStore';
import { authApi } from '../../src/api/authApi';
import { logger } from '../../src/utils/logger';

export default function LoginScreen() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  
  const { login } = useAuthStore();
  const router = useRouter();

  const handleLogin = async () => {
    if (!email || !password) {
      Alert.alert('Hata', 'Lütfen e-posta ve şifrenizi giriniz.');
      return;
    }

    setLoading(true);
    try {
      const response = await authApi.login({ email, password });

      if (!response.success || !response.data?.token) {
        Alert.alert('Giriş Başarısız', response.message ?? 'Bilinmeyen bir hata oluştu.');
        return;
      }

      login(response.data.token, {
          id: 0,
          email,
          firstName: '',
          lastName: '',
          status: true,
          tenantId: 0,
          role: 'ogrenci'
      });
      logger.info('Login successful', { email });
      router.replace('/(tabs)');
    } catch (error: any) {
      logger.error('Login failed', error);
      Alert.alert('Giriş Başarısız', error.response?.data?.message || 'Bir hata oluştu. Lütfen bilgilerinizi kontrol edin.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <KeyboardAvoidingView 
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      className="flex-1 bg-indigo-900 justify-center items-center"
    >
      {/* Background Decorators */}
      <View className="absolute w-72 h-72 bg-purple-500 rounded-full blur-3xl opacity-50 -top-20 -left-20" />
      <View className="absolute w-96 h-96 bg-indigo-500 rounded-full blur-3xl opacity-40 -bottom-20 -right-20" />

      <BlurView intensity={80} tint="light" className="w-[85%] rounded-3xl p-8 overflow-hidden border border-white/20">
        <Text className="text-3xl font-bold text-center text-white mb-2">Hafızlık Takip</Text>
        <Text className="text-white/80 text-center mb-8">Sisteme giriş yapın</Text>

        <View className="space-y-4">
          <View>
            <Text className="text-white mb-2 font-medium">E-posta</Text>
            <TextInput
              value={email}
              onChangeText={setEmail}
              placeholder="ornek@mail.com"
              placeholderTextColor="rgba(255, 255, 255, 0.5)"
              keyboardType="email-address"
              autoCapitalize="none"
              className="bg-white/10 border border-white/20 text-white rounded-xl px-4 py-3"
            />
          </View>

          <View className="mb-6">
            <Text className="text-white mb-2 font-medium">Şifre</Text>
            <TextInput
              value={password}
              onChangeText={setPassword}
              placeholder="••••••••"
              placeholderTextColor="rgba(255, 255, 255, 0.5)"
              secureTextEntry
              className="bg-white/10 border border-white/20 text-white rounded-xl px-4 py-3"
            />
          </View>

          <TouchableOpacity 
            onPress={handleLogin}
            disabled={loading}
            className={`bg-indigo-600 rounded-xl py-4 items-center ${loading ? 'opacity-70' : ''}`}
          >
            {loading ? (
              <ActivityIndicator color="white" />
            ) : (
              <Text className="text-white font-bold text-lg">Giriş Yap</Text>
            )}
          </TouchableOpacity>
        </View>
      </BlurView>
    </KeyboardAvoidingView>
  );
}
