# 📐 Backend Development Rules

> Bu belge, Hafızlık Takip backend projesinde tüm geliştiricilerin uyması gereken kuralları tanımlar.
> Her PR bu kurallara uygunluk açısından kontrol edilir.

---

## 1. Naming Conventions (İsimlendirme Kuralları)

### C# Genel Kuralları

| Öğe | Kural | Örnek |
|-----|-------|-------|
| **Namespace** | PascalCase | `HafizlikTakip.Business.Concrete` |
| **Class** | PascalCase | `StudentManager`, `EfStudentDal` |
| **Interface** | I + PascalCase | `IStudentService`, `IStudentDal` |
| **Public Method** | PascalCase | `GetAll()`, `Add()`, `GetById()` |
| **Private Method** | _camelCase (alt çizgi prefix) | `_validateStudent()` |
| **Public Property** | PascalCase | `FirstName`, `TenantId` |
| **Private Field** | _camelCase | `_studentDal`, `_tokenHelper` |
| **Parameter** | camelCase | `studentId`, `tenantId` |
| **Local Variable** | camelCase | `result`, `studentList` |
| **Constant** | PascalCase | `MaxPageCount`, `DefaultPageSize` |
| **Enum** | PascalCase (members dahil) | `MistakeType.Pronunciation` |
| **DTO Class** | PascalCase + Suffix | `StudentCreateDto`, `LessonDetailDto` |
| **Validator Class** | PascalCase + Validator | `StudentValidator`, `LessonValidator` |

### Dosya İsimlendirme

| Katman | Prefix/Suffix | Örnek |
|--------|---------------|-------|
| Entity | — | `Student.cs` |
| DTO | ...Dto | `StudentCreateDto.cs` |
| DAL Interface | I...Dal | `IStudentDal.cs` |
| DAL Concrete | Ef...Dal | `EfStudentDal.cs` |
| Service Interface | I...Service | `IStudentService.cs` |
| Service Concrete | ...Manager | `StudentManager.cs` |
| Validator | ...Validator | `StudentValidator.cs` |
| Controller | ...Controller | `StudentsController.cs` (çoğul) |

### JSON / API Response

| Öğe | Kural | Örnek |
|-----|-------|-------|
| JSON property | **camelCase** | `firstName`, `tenantId`, `currentPage` |
| API endpoint | **kebab-case** (lowercase) | `/api/duty-schedule`, `/api/teacher-students` |
| Query parameter | **camelCase** | `?studentId=5&pageSize=20` |

---

## 2. HTTP Method Politikası

> [!IMPORTANT]
> Bu projede **sadece GET ve POST** HTTP metodları kullanılır.
> PUT, PATCH, DELETE metodları **KULLANILMAZ**.

### Neden?
- Mobil cihazlarda bazı proxy/firewall'lar PUT/DELETE'i engelleyebilir
- Endpoint'lerin amacı URL path'ten açıkça anlaşılır
- Tutarlılık ve basitlik sağlar

### Endpoint Tasarım Kuralları

```
✅ Doğru Kullanım:

GET  /api/students              → Tüm öğrencileri getir
GET  /api/students?id=5         → Tek öğrenci getir
POST /api/students/add          → Yeni öğrenci ekle
POST /api/students/update       → Öğrenci güncelle
POST /api/students/delete       → Öğrenci sil (soft delete)

GET  /api/lessons?studentId=5   → Öğrencinin derslerini getir
POST /api/lessons/add           → Yeni ders kaydı

GET  /api/duty-schedule/week?startDate=2025-09-01  → Haftalık program
POST /api/duty-schedule/generate                    → Otomatik oluştur
POST /api/duty-schedule/approve                     → Onayla

❌ Yanlış Kullanım:

PUT    /api/students/5          → YASAK
PATCH  /api/students/5          → YASAK
DELETE /api/students/5          → YASAK
```

### Controller Method İsimlendirme

```csharp
// ✅ Doğru
[HttpGet("getall")]
public IActionResult GetAll() { ... }

[HttpGet("getbyid")]
public IActionResult GetById(int id) { ... }

[HttpPost("add")]
public IActionResult Add(StudentCreateDto dto) { ... }

[HttpPost("update")]
public IActionResult Update(StudentUpdateDto dto) { ... }

[HttpPost("delete")]
public IActionResult Delete(int id) { ... }
```

---

## 3. Multi-Tenant Kuralları

> [!CAUTION]
> Multi-tenant izolasyonu bu projenin en kritik güvenlik katmanıdır.
> Bir tenant'ın verisinin başka bir tenant tarafından görülmesi **KESİNLİKLE KABUL EDİLEMEZ**.

### Temel Prensipler

1. **Her entity** `TenantId` alanı içermelidir (Core entity'ler hariç: User, OperationClaim)
2. **EF Core Global Query Filter** ile tüm sorgular otomatik filtrelenir
3. `TenantId` **asla** client'tan alınmaz → JWT token'dan çözümlenir
4. Yeni kayıt eklerken `TenantId` otomatik set edilir (Interceptor/Middleware)
5. Seed data tenant-bağımsızdır (Sure, Cüz, Sayfa verileri)

### TenantId Akışı

```
Client Request
    → JWT Token (TenantId claim içerir)
    → TenantResolverMiddleware (HttpContext.Items["TenantId"] = ...)
    → DbContext Global Query Filter (.HasQueryFilter(x => x.TenantId == currentTenantId))
    → Veri sadece ilgili tenant'a ait döner
```

### Kontrol Listesi

- [ ] Yeni entity oluşturdun mu? → `TenantId` alanı ekle
- [ ] Yeni migration çalıştırdın mı? → `TenantId` index'i var mı kontrol et
- [ ] Raw SQL yazdın mı? → `WHERE TenantId = @tenantId` eklediğinden emin ol
- [ ] Test yazdın mı? → Farklı tenant'ların verilerinin karışmadığını doğrula

---

## 4. Veri Akışı (Data Flow)

### İstek Akışı (Request → Response)

```
📱 Client (React Native / Web)
    │
    ▼
🌐 WebAPI Controller
    │  - DTO alır (request body veya query param)
    │  - Sadece IActionResult döner
    │  - İş mantığı YAPMAZ
    │
    ▼
⚙️ Business Manager
    │  - [SecuredOperation] → Yetki kontrolü (AOP)
    │  - [ValidationAspect] → FluentValidation (AOP)
    │  - [CacheAspect] → Önbellekleme (AOP)
    │  - İş kurallarını uygular
    │  - IResult / IDataResult<T> döner
    │
    ▼
💾 DataAccess DAL
    │  - EF Core sorguları
    │  - Entity alır/döner
    │  - İş mantığı YAPMAZ
    │
    ▼
🗄️ PostgreSQL (TenantId ile filtrelenmiş)
```

### Response Format

Tüm API response'ları `IResult` veya `IDataResult<T>` formatında döner:

```json
// Başarılı (veri ile)
{
  "data": { ... },
  "success": true,
  "message": "Öğrenci başarıyla eklendi"
}

// Başarılı (veri olmadan)
{
  "success": true,
  "message": "İşlem başarılı"
}

// Hata
{
  "success": false,
  "message": "Bu öğrenci zaten kayıtlı"
}

// Liste
{
  "data": [ ... ],
  "success": true,
  "message": null
}
```

### DTO Kuralları

1. **Controller** sadece DTO alır ve döner, entity almaz
2. **Entity → DTO** dönüşümü AutoMapper veya manuel mapping ile yapılır
3. Her işlem için ayrı DTO: `StudentCreateDto`, `StudentUpdateDto`, `StudentDetailDto`
4. DTO'lar `Entities/Dtos/` klasöründe, `IDto` marker interface'i ile işaretlenir

---

## 5. Katman Bağımlılık Kuralları

```
Core          → Hiçbir projeye bağımlı değil
Entities      → Core'a bağımlı
DataAccess    → Core + Entities'e bağımlı
Business      → Core + Entities + DataAccess'e bağımlı
WebAPI        → Core + Entities + Business'a bağımlı (DataAccess'e DEĞİL!)
```

> [!WARNING]
> WebAPI katmanı **asla** DataAccess katmanına doğrudan referans vermez.
> Tüm veri erişimi Business katmanı üzerinden yapılır.
> (Autofac DI modülü WebAPI'de yüklenir ama servis çağrıları Business üzerinden geçer)

---

## 6. Git Workflow

### Branch Stratejisi

```
main              → Production-ready kod
├── develop       → Aktif geliştirme
│   ├── feature/auth          → Yetkilendirme özelliği
│   ├── feature/students      → Öğrenci modülü
│   ├── feature/duty-schedule → Nöbet programı
│   └── feature/notifications → Bildirimler
└── hotfix/...    → Acil düzeltmeler
```

### Commit Mesaj Formatı

```
feat: öğrenci CRUD işlemleri eklendi
fix: tenant filter'da null reference düzeltildi
refactor: StudentManager'da tekrarlayan kod temizlendi
docs: API endpoint dokümantasyonu güncellendi
test: StudentManager unit testleri eklendi
chore: NuGet paketleri güncellendi
```

### Kurallar

- Her commit **tek bir mantıksal değişiklik** içerir
- Her özellik tamamlandığında `develop` branch'e push edilir
- `main` branch'e sadece test edilmiş kod merge edilir
- Commit mesajları **Türkçe** yazılır

---

## 7. Genel Kodlama Kuralları

1. **Magic number/string kullanma** → Constants sınıfında tanımla
2. **Business logic controller'da yazılmaz** → Manager sınıflarında olur
3. **try-catch controller'da kullanılmaz** → ExceptionMiddleware yakalar
4. **Console.WriteLine kullanılmaz** → ILogger kullan
5. **var** kullanımı: Tip açıkça belliyse `var` kullan, belirsizse explicit tip yaz
6. **async/await**: Tüm DB işlemleri async olacak
7. **Nullable**: `#nullable enable` açık olacak, null kontrolü yapılacak
8. **Region**: `#region` kullanılmayacak, sınıflar küçük tutulacak
