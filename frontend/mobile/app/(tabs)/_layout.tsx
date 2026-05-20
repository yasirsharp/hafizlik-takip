import { Tabs } from 'expo-router';
import { View, Text } from 'react-native';

export default function TabsLayout() {
  return (
    <Tabs screenOptions={{ headerShown: true }}>
      <Tabs.Screen 
        name="index" 
        options={{ 
          title: 'Ana Sayfa',
          tabBarIcon: () => (
            <View>
              <Text>🏠</Text>
            </View>
          )
        }} 
      />
      {/* İleride eklenebilecek sekmeler */}
    </Tabs>
  );
}
