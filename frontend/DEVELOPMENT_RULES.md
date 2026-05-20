# 📐 Frontend Development Rules

> Bu belge, Hafızlık Takip frontend projelerinde (Mobil + Web) tüm geliştiricilerin uyması gereken kuralları tanımlar.

---

## 1. Proje Yapısı

```
frontend/
├── mobile/                    # React Native + Expo (iOS/Android)
│   ├── src/
│   │   ├── api/              # Axios instance, interceptors, endpoint tanımları
│   │   ├── components/       # Paylaşılan UI bileşenleri
│   │   ├── screens/          # Ekranlar (her rol için alt klasör)
│   │   ├── app/              # Expo Router dosya tabanlı navigasyon (Route'lar)
│   │   ├── hooks/            # Custom hook'lar
│   │   ├── store/            # Zustand State (AuthStore, TenantStore)
│   │   ├── i18n/             # Çoklu dil dosyaları (tr, ar, en)
│   │   ├── types/            # TypeScript tip tanımları (backend DTO'larla eş)
│   │   ├── utils/            # Yardımcı fonksiyonlar
│   │   ├── constants/        # Sabitler
│   │   └── theme/            # Renk, font, spacing tanımları
│   ├── assets/               # Görseller, fontlar
│   ├── app.json
│   └── package.json
│
└── web/                       # React + Vite + Ant Design (Admin Panel)
    ├── src/
    │   ├── api/
    │   ├── components/
    │   ├── pages/
    │   ├── hooks/
    │   ├── context/
    │   ├── types/
    │   ├── utils/
    │   └── theme/
    ├── index.html
    └── package.json
```

---

## 2. Naming Conventions (İsimlendirme Kuralları)

### TypeScript / JavaScript

| Öğe | Kural | Örnek |
|-----|-------|-------|
| **Dosya (component)** | PascalCase | `StudentCard.tsx`, `LoginScreen.tsx` |
| **Dosya (hook)** | camelCase + use prefix | `useAuth.ts`, `useStudents.ts` |
| **Dosya (util/helper)** | camelCase | `formatDate.ts`, `tokenStorage.ts` |
| **Dosya (type/interface)** | PascalCase | `StudentTypes.ts`, `ApiResponse.ts` |
| **Component** | PascalCase | `<StudentCard />`, `<LessonForm />` |
| **Function** | camelCase | `handleSubmit()`, `fetchStudents()` |
| **Variable** | camelCase | `studentList`, `isLoading` |
| **Constant** | UPPER_SNAKE_CASE | `API_BASE_URL`, `MAX_RETRY_COUNT` |
| **Interface** | I + PascalCase | `IStudent`, `ILessonCreateDto` |
| **Type** | PascalCase | `StudentResponse`, `ApiError` |
| **Enum** | PascalCase | `MistakeType.Pronunciation` |
| **CSS class** | kebab-case | `student-card`, `lesson-form-header` |
| **Event handler** | handle + Event | `handlePress`, `handleSubmit` |
| **Boolean** | is/has/can prefix | `isLoading`, `hasError`, `canEdit` |

### Klasör İsimlendirme

- Tüm klasörler **kebab-case** (küçük harf + tire): `duty-schedule/`, `teacher-students/`
- Component klasörleri **PascalCase** exception: `StudentCard/` (component + style birlikte)

---

## 3. API İletişim Kuralları

### HTTP Method Politikası

> [!IMPORTANT]
> Backend ile aynı kural: **Sadece GET ve POST** kullanılır.

```typescript
// ✅ Doğru
api.get('/students');
api.get('/students', { params: { id: 5 } });
api.post('/students/add', studentData);
api.post('/students/update', studentData);
api.post('/students/delete', { id: 5 });

// ❌ Yanlış
api.put('/students/5', data);
api.patch('/students/5', data);
api.delete('/students/5');
```

### Axios Instance Yapılandırması

```typescript
// api/axiosInstance.ts
const api = axios.create({
  baseURL: API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
});

// JWT Interceptor — her istekte token eklenir
api.interceptors.request.use((config) => {
  const token = getAccessToken();
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// Response Interceptor — 401'de logout
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) { logout(); }
    return Promise.reject(error);
  }
);
```

### API Response Type

Backend'den gelen tüm response'lar bu formattadır:

```typescript
// types/ApiResponse.ts
interface IResult {
  success: boolean;
  message: string | null;
}

interface IDataResult<T> extends IResult {
  data: T;
}
```

### API Fonksiyon Örneği

```typescript
// api/studentApi.ts
export const studentApi = {
  getAll: () => api.get<IDataResult<IStudent[]>>('/students/getall'),
  getById: (id: number) => api.get<IDataResult<IStudent>>('/students/getbyid', { params: { id } }),
  add: (data: IStudentCreateDto) => api.post<IResult>('/students/add', data),
  update: (data: IStudentUpdateDto) => api.post<IResult>('/students/update', data),
  delete: (id: number) => api.post<IResult>('/students/delete', { id }),
};
```

---

## 4. Multi-Tenant Kuralları

> [!CAUTION]
> Frontend'de **asla** TenantId manuel olarak gönderilmez.
> TenantId JWT token içinde taşınır ve backend tarafından çözümlenir.

### Kurallar

1. **TenantId'yi localStorage/state'te tutma** → Token'dan otomatik gelir
2. **URL'de tenantId kullanma** → `/api/students` yeterli, `/api/tenants/5/students` YAPMA
3. **Tenant verisi gösterilirken** → Sadece backend'in döndüğü veriyi göster, filtreleme yapma
4. Login sonrası kullanıcının tenant bilgisi token decode edilerek context'e yazılır

---

## 5. State Management

### Mobil (React Native)

```
useAuthStore    → Kullanıcı, token, rol bilgisi yönetimi (Zustand)
useTenantStore  → Aktif tenant bilgisi yönetimi (Zustand)
useThemeStore   → Tema ve UI state (Zustand)
```

- **Global State:** Sadece Zustand kullanılır (Context API kullanılmaz).
- **API State:** Axios + custom fetch hook'ları ile yönetilir.
- **Routing/Navigation:** Expo Router (App Router) kullanılır.
- **Styling (UI):** NativeWind (Tailwind CSS) kullanılır, StyleSheet sınırlı alanlarda tercih edilir.

### Web (Admin Panel)

- Ant Design form state
- React Query (server state)
- Context API (auth, tenant)

---

## 6. Çoklu Dil (i18n)

### Desteklenen Diller

| Dil | Kod | Dosya |
|-----|-----|-------|
| Türkçe | `tr` | `i18n/tr.json` |
| Arapça | `ar` | `i18n/ar.json` |
| İngilizce | `en` | `i18n/en.json` |

### Kurallar

1. **Hardcoded string YASAK** → Tüm metinler `t('key')` ile çağrılır
2. Key formatı: `module.action.detail` → `students.add.success`
3. Arapça mod aktifken RTL layout otomatik uygulanır
4. Sayı/tarih formatları locale'e göre ayarlanır

---

## 7. Component Kuralları

### Dosya Yapısı

```
StudentCard/
├── StudentCard.tsx          # Component (NativeWind className'leri ile)
└── index.ts                 # export
```

### Kurallar

1. Her component **tek bir sorumluluk** taşır
2. 200 satırı geçen component bölünür
3. Business logic component'te OLMAZ → custom hook'a taşınır
4. Props interface'i component dosyasında tanımlanır
5. Default export kullanılmaz → named export

```typescript
// ✅ Doğru
export const StudentCard: React.FC<IStudentCardProps> = ({ student }) => { ... };

// ❌ Yanlış
export default function StudentCard() { ... }
```

---

## 8. Git Workflow

### Branch Stratejisi

```
main              → Production-ready
├── develop       → Aktif geliştirme
│   ├── feature/login-screen
│   ├── feature/student-list
│   ├── feature/duty-schedule-view
│   └── feature/notifications
└── hotfix/...
```

### Commit Mesaj Formatı

```
feat: öğrenci listesi ekranı eklendi
fix: login ekranında keyboard overlay düzeltildi
style: StudentCard spacing düzenlendi
refactor: useAuth hook'u optimize edildi
docs: API entegrasyon notları güncellendi
chore: bağımlılıklar güncellendi
```

---

## 9. Type Safety Kuralları

1. **`any` tipi YASAK** → Mutlaka doğru tip tanımla
2. Backend DTO'ları ile frontend type'ları **birebir eşleşmeli**
3. API response'ları her zaman generic type ile çağrılır
4. `null` ve `undefined` kontrolü yapılır (optional chaining `?.` kullan)

```typescript
// ✅ Doğru
const student: IStudent | null = response.data.data;

// ❌ Yanlış
const student: any = response.data;
```

---

## 10. Performans Kuralları

1. **Gereksiz re-render önle** → `React.memo`, `useMemo`, `useCallback`
2. **Büyük listelerde** → `FlatList` (mobil) veya `virtualized list` (web)
3. **Resimler** → Optimize edilmiş boyutlarda, lazy load
5. **Bundle size** → Kullanılmayan import'ları kaldır

---

## 11. Logging (Loglama) Kuralları

Frontend projelerinde (Mobil ve Web) **kesinlikle doğrudan `console.log` KULLANILMAZ**. Her zaman `src/utils/logger.ts` üzerinden loglama yapılmalıdır.

### Ortam Bazlı Loglama
- **Development (`__DEV__`):** Tüm loglar (`DEBUG`, `INFO`, `WARN`, `ERROR`) console'a basılır.
- **Test:** Genellikle sadece `WARN` ve `ERROR` seviyeleri aktiftir. Test console'unu temiz tutmak için kullanılır.
- **Production:** Sadece `ERROR` seviyesi aktiftir. Hatalar ekranda gösterilmez, Sentry veya Crashlytics gibi remote hata takip araçlarına iletilir.

### Kullanım Örnekleri
```typescript
import { logger } from '../utils/logger';

// ❌ Yanlış
console.log('User fetched', data);
console.error('API Error', err);

// ✅ Doğru
logger.debug('User list retrieved successfully', { count: data.length });
logger.info('User successfully logged in', { userId: user.id });
logger.warn('Token is about to expire');
logger.error('Failed to fetch students', err);
```
