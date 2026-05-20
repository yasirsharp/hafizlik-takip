import { View, Text, TouchableOpacity } from 'react-native';
import { useAuthStore } from '../../src/store/useAuthStore';

export default function HomeScreen() {
  const { user, logout } = useAuthStore();

  return (
    <View className="flex-1 items-center justify-center bg-gray-50">
      <Text className="text-2xl font-bold mb-4">Hoş Geldiniz!</Text>
      <Text className="text-lg mb-8">{user?.firstName} {user?.lastName}</Text>
      
      <TouchableOpacity 
        onPress={logout}
        className="bg-red-500 py-3 px-6 rounded-lg"
      >
        <Text className="text-white font-semibold">Çıkış Yap</Text>
      </TouchableOpacity>
    </View>
  );
}
