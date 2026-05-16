# ☪️ Hafız Takip

> Kur'an kursu öğreticileri için hafızlık takibi, ders yönetimi,  
> veli bilgilendirme ve akşam etüdü koordinasyon sistemi.

---

## İçindekiler

1. [Proje Tanımı](#1-proje-tanımı)
2. [Çözülen Problem](#2-çözülen-problem)
3. [Hedef Kullanıcılar & Roller](#3-hedef-kullanıcılar--roller)
4. [Özellik Listesi](#4-özellik-listesi)
5. [Teknik Mimari](#5-teknik-mimari)
6. [Veri Modeli](#6-veri-modeli)
7. [Ekran Haritası](#7-ekran-haritası)
8. [Geliştirme Roadmap'i](#8-geliştirme-roadmapi)
9. [Kurulum](#9-kurulum)
10. [Geliştirme Notları](#10-geliştirme-notları)

---

## 1. Proje Tanımı

**Hafız Takip**, Kur'an kursu öğreticilerinin tüm günlük yönetim süreçlerini  
tek bir mobil uygulamada birleştiren, telefon öncelikli bir web uygulamasıdır.

Öğrencilerin hafızlık yolculuklarını sayfa bazında takip eder; günlük ders,  
tekrar ve ödev kayıtlarını dijitalleştirir. Velilere otomatik bildirim gönderir,  
akşam etüdü için yardımcı hocalara not iletir.

**Teknoloji:** React · Supabase · Tailwind CSS · PWA  
**Platform:** Mobil öncelikli web (telefon tarayıcısı + kurulabilir PWA)  
**Dil:** Türkçe

---

## 2. Çözülen Problem

Kur'an kursu öğreticileri her gün aşağıdaki süreçleri kağıt üzerinde  
ya da bellekten yönetmek zorunda kalmaktadır:

| Süreç | Mevcut Durum | Uygulama Sonrası |
|-------|-------------|-----------------|
| Yeni sayfa kaydı | Kağıda not | Anlık dijital kayıt |
| Tekrar takibi | Bellek / defter | Sabah–öğle–akşam ayrı oturum |
| Ödev kontrolü | Manuel liste | Durum rozetiyle filtreleme |
| Veli bilgilendirme | Manuel SMS/WA | Otomatik şablon + tek tıkla gönderim |
| Etüt koordinasyonu | Sözlü iletişim | Yardımcı hocaya dijital not |
| İlerleme takibi | Yok | Sayfa bazlı görsel harita |

---

## 3. Hedef Kullanıcılar & Roller

### Baş Hoca (Admin)
- Öğrenci ekler, düzenler, arşivler
- Günlük ders kaydeder
- Veli bildirimi gönderir
- Etüt notu bırakır
- Tüm raporlara erişir

### Yardımcı Hoca
- Kendisine bırakılan etüt notlarını görür
- Tamamlama durumunu günceller
- Öğrenci ders geçmişini okuyabilir (salt okunur)

### Veli *(İleride)*
- Yalnızca kendi çocuğunun raporlarını görür
- Bildirim onayı verir / iptal eder

---

## 4. Özellik Listesi

### 4.1 Öğrenci Yönetimi
- Öğrenci oluşturma: ad, yaş, veli adı, veli telefonu
- Mevcut sayfa / cüz / sure bilgisi
- Fotoğraf / avatar rengi
- Pasife alma (silme değil arşivleme)
- Arama ve filtreleme

### 4.2 Ders & Tekrar Takibi
- Günlük ders kaydı: yeni sayfa sayısı, tekrar sayısı
- Tekrar oturumu: sabah / öğle / akşam ayrı kayıt
- Ödev durumu: yapıldı / eksik / yapılmadı
- Performans değerlendirmesi: Mükemmel / Çok İyi / İyi / Orta / Zayıf
- Serbest not alanı (mahreç hatası, dikkat vb.)
- Ders geçmişi ve düzenleme

### 4.3 Hafızlık İlerleme Haritası
- 604 sayfa üzerinden görsel ilerleme çubuğu
- Cüz ve sure bazlı konum gösterimi
- Haftalık ilerleme trendi (grafik)
- Tahmini tamamlanma süresi hesabı

### 4.4 Veli Bilgilendirme
- Günlük otomatik rapor metni oluşturma
- WhatsApp ile tek tıkla gönderim
- Haftalık özet bildirimi
- Gönderim logu ve durum takibi

### 4.5 Akşam Etüdü Koordinasyonu
- Öğrenci bazlı etüt notu ekleme
- Bugünkü etüt listesi (yardımcı hoca görünümü)
- Tamamlandı işaretleme
- Geçmiş notlar arşivi

### 4.6 Raporlama & Analitik
- Günlük özet dashboard
- Haftalık / aylık ilerleme raporu
- Ödev tamamlama oranları
- Sınıf geneli karşılaştırma
- PDF rapor dışa aktarımı

### 4.7 Sistem & Kullanıcı Yönetimi
- Hoca girişi: telefon + OTP
- Çoklu öğretmen desteği
- Rol bazlı yetkilendirme
- Offline çalışma (PWA + IndexedDB kuyruk)
- Türkçe arayüz

---

## 5. Teknik Mimari

### Klasör Yapısı

```
hafiz-takip/
├── public/
│   ├── manifest.json          # PWA manifest
│   └── icons/
├── src/
│   ├── app/
│   │   ├── App.jsx
│   │   ├── Router.jsx
│   │   └── store/             # Zustand global state
│   │       ├── studentStore.js
│   │       ├── lessonStore.js
│   │       └── authStore.js
│   ├── components/            # Tekrar kullanılabilir UI
│   │   ├── ui/
│   │   │   ├── Avatar.jsx
│   │   │   ├── Badge.jsx
│   │   │   ├── Button.jsx
│   │   │   ├── Card.jsx
│   │   │   ├── Input.jsx
│   │   │   ├── Modal.jsx
│   │   │   └── ProgressBar.jsx
│   │   └── layout/
│   │       ├── BottomNav.jsx
│   │       ├── TopBar.jsx
│   │       └── PageWrapper.jsx
│   ├── screens/
│   │   ├── auth/
│   │   │   └── LoginScreen.jsx
│   │   ├── dashboard/
│   │   │   └── DashboardScreen.jsx
│   │   ├── students/
│   │   │   ├── StudentListScreen.jsx
│   │   │   ├── StudentDetailScreen.jsx
│   │   │   └── StudentFormScreen.jsx
│   │   ├── lessons/
│   │   │   ├── LessonListScreen.jsx
│   │   │   └── LessonFormScreen.jsx
│   │   ├── parents/
│   │   │   └── ParentNotifyScreen.jsx
│   │   ├── etut/
│   │   │   └── EtutScreen.jsx
│   │   └── reports/
│   │       └── ReportsScreen.jsx
│   ├── services/
│   │   ├── supabase.js        # Supabase client
│   │   ├── studentService.js
│   │   ├── lessonService.js
│   │   └── notificationService.js
│   ├── hooks/
│   │   ├── useStudents.js
│   │   ├── useLessons.js
│   │   ├── useAuth.js
│   │   └── useOfflineQueue.js
│   ├── utils/
│   │   ├── dateHelpers.js
│   │   ├── whatsappBuilder.js
│   │   └── surahList.js       # Sure isimleri (1-114)
│   └── styles/
│       └── index.css          # Tailwind + özel değişkenler
├── .env.local
├── vite.config.js
├── tailwind.config.js
└── package.json
```

### Teknoloji Kararları

| Katman | Teknoloji | Gerekçe |
|--------|-----------|---------|
| UI Framework | React 18 | Geniş ekosistem, hızlı prototipleme |
| Stil | Tailwind CSS | Mobil öncelikli utility-first |
| State | Zustand | Basit, boilerplate az |
| Backend | Supabase | Auth + DB + Realtime hepsi hazır |
| Veritabanı | PostgreSQL (Supabase) | İlişkisel veri, güçlü sorgular |
| Build | Vite | Hızlı HMR, küçük bundle |
| PWA | Vite PWA Plugin | Offline destek, kurulabilir |
| Deploy | Vercel | Ücretsiz, otomatik CI/CD |
| Bildirim | WhatsApp link → WA Business API | Kademeli geçiş |

---

## 6. Veri Modeli

```sql
-- ─────────────────────────────────────────
-- Öğretmenler
-- ─────────────────────────────────────────
CREATE TABLE teachers (
  id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  name        TEXT NOT NULL,
  phone       TEXT UNIQUE NOT NULL,
  role        TEXT NOT NULL DEFAULT 'teacher',
                -- 'admin' | 'teacher' | 'assistant'
  is_active   BOOLEAN DEFAULT true,
  created_at  TIMESTAMPTZ DEFAULT now()
);

-- ─────────────────────────────────────────
-- Öğrenciler
-- ─────────────────────────────────────────
CREATE TABLE students (
  id             UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  teacher_id     UUID NOT NULL REFERENCES teachers(id),
  name           TEXT NOT NULL,
  age            INT,
  parent_name    TEXT,
  parent_phone   TEXT,
  current_page   INT NOT NULL DEFAULT 1,
  current_juz    INT NOT NULL DEFAULT 1,
  current_surah  TEXT NOT NULL DEFAULT 'Fatiha',
  performance    TEXT DEFAULT 'İyi',
  avatar_color   TEXT DEFAULT '#1B5C3A',
  is_active      BOOLEAN DEFAULT true,
  created_at     TIMESTAMPTZ DEFAULT now()
);

-- ─────────────────────────────────────────
-- Ders Kayıtları
-- ─────────────────────────────────────────
CREATE TABLE lessons (
  id            UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  student_id    UUID NOT NULL REFERENCES students(id),
  teacher_id    UUID NOT NULL REFERENCES teachers(id),
  lesson_date   DATE NOT NULL,
  new_pages     INT NOT NULL DEFAULT 0,
  homework      TEXT NOT NULL DEFAULT 'yapıldı',
                  -- 'yapıldı' | 'eksik' | 'yapılmadı'
  performance   TEXT,
  note          TEXT,
  created_at    TIMESTAMPTZ DEFAULT now(),
  UNIQUE (student_id, lesson_date)
);

-- ─────────────────────────────────────────
-- Tekrar Oturumları
-- ─────────────────────────────────────────
CREATE TABLE repetition_sessions (
  id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  lesson_id    UUID NOT NULL REFERENCES lessons(id) ON DELETE CASCADE,
  student_id   UUID NOT NULL REFERENCES students(id),
  session_time TEXT NOT NULL,
                  -- 'sabah' | 'öğle' | 'akşam'
  page_start   INT NOT NULL,
  page_end     INT NOT NULL,
  rep_count    INT NOT NULL DEFAULT 1,
  quality      INT CHECK (quality BETWEEN 1 AND 5),
  note         TEXT,
  created_at   TIMESTAMPTZ DEFAULT now()
);

-- ─────────────────────────────────────────
-- Etüt Notları
-- ─────────────────────────────────────────
CREATE TABLE etut_notes (
  id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  student_id   UUID NOT NULL REFERENCES students(id),
  from_teacher UUID NOT NULL REFERENCES teachers(id),
  note_date    DATE NOT NULL,
  note         TEXT NOT NULL,
  is_done      BOOLEAN DEFAULT false,
  done_at      TIMESTAMPTZ,
  created_at   TIMESTAMPTZ DEFAULT now()
);

-- ─────────────────────────────────────────
-- Veli Bildirim Logu
-- ─────────────────────────────────────────
CREATE TABLE parent_notifications (
  id            UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  student_id    UUID NOT NULL REFERENCES students(id),
  teacher_id    UUID NOT NULL REFERENCES teachers(id),
  sent_at       TIMESTAMPTZ DEFAULT now(),
  message_type  TEXT DEFAULT 'daily',
                  -- 'daily' | 'weekly' | 'custom'
  channel       TEXT DEFAULT 'whatsapp',
  message_body  TEXT,
  status        TEXT DEFAULT 'sent'
);
```

### Row Level Security (RLS) Kuralları

```sql
-- Öğretmen yalnızca kendi öğrencilerini görebilir
CREATE POLICY "teacher_own_students" ON students
  FOR ALL USING (teacher_id = auth.uid());

-- Ders kayıtları: öğrencinin öğretmenine ait
CREATE POLICY "teacher_own_lessons" ON lessons
  FOR ALL USING (teacher_id = auth.uid());

-- Etüt notları: yardımcı hoca okuyabilir
CREATE POLICY "assistant_read_etut" ON etut_notes
  FOR SELECT USING (true);

CREATE POLICY "teacher_write_etut" ON etut_notes
  FOR INSERT WITH CHECK (from_teacher = auth.uid());
```

---

## 7. Ekran Haritası

```
App
├── /login                         Giriş Ekranı (OTP)
│
└── (Auth gerektiren)
    ├── /                          Dashboard (Ana Sayfa)
    │   ├── Günlük istatistikler
    │   ├── Öğrenci kart listesi
    │   └── Etüt uyarı bandı
    │
    ├── /students                  Öğrenci Listesi
    │   ├── Arama / filtre
    │   └── + Yeni Öğrenci butonu
    │
    ├── /students/:id              Öğrenci Detay
    │   ├── [Tab] Dersler
    │   │   ├── Bugün Ders Ekle
    │   │   └── Ders Geçmişi
    │   ├── [Tab] Tekrarlar
    │   │   ├── Sabah Oturumu
    │   │   ├── Öğle Oturumu
    │   │   └── Akşam Oturumu
    │   ├── [Tab] Etüt Notları
    │   └── [Tab] Veli
    │       ├── Veli Bilgileri
    │       ├── Rapor Önizleme
    │       └── WhatsApp Gönder
    │
    ├── /students/:id/edit         Öğrenci Düzenle
    │
    ├── /lessons                   Günlük Ders Özeti
    │   ├── Tamamlanan dersler
    │   └── Bekleyen dersler
    │
    ├── /parents                   Toplu Veli Bildirimi
    │   └── Tüm öğrenciler → WA linki
    │
    ├── /etut                      Akşam Etüdü
    │   ├── Bugünkü notlar
    │   └── Tamamlandı işaretleme
    │
    └── /reports                   Raporlar (İleri Aşama)
        ├── Haftalık özet
        └── Aylık ilerleme grafiği
```

---

## 8. Geliştirme Roadmap'i

---

### 🟦 AŞAMA 1 — Temel Altyapı

**Hedef:** Projeyi ayağa kaldır, temel navigasyon ve tasarım sistemini kur.

- [ ] Vite + React projesi oluştur
- [ ] Tailwind CSS kurulumu ve renk / font değişkenlerini tanımla
- [ ] Supabase projesi oluştur, `.env.local` bağlantı ayarları
- [ ] Temel klasör yapısını oluştur (`screens/`, `components/`, `services/`, `hooks/`, `utils/`)
- [ ] React Router kurulumu ve temel route'lar
- [ ] Zustand store iskeletleri (`authStore`, `studentStore`, `lessonStore`)
- [ ] Ortak UI bileşenleri: `Button`, `Card`, `Input`, `Badge`, `Avatar`, `ProgressBar`, `Modal`
- [ ] Layout bileşenleri: `BottomNav`, `TopBar`, `PageWrapper`
- [ ] Mobil viewport ayarları (meta, body scroll)
- [ ] Supabase veritabanı tablolarını oluştur
- [ ] RLS politikalarını uygula

---

### 🟦 AŞAMA 2 — Kimlik Doğrulama

**Hedef:** Öğretmen güvenli şekilde giriş yapabilsin.

- [ ] Supabase Auth — telefon numarası + OTP akışı
- [ ] `LoginScreen`: telefon giriş formu, OTP doğrulama adımı
- [ ] `useAuth` hook'u (oturum durumu, kullanıcı bilgisi, loading)
- [ ] `authStore`: oturum bilgisini global state'e al
- [ ] Korumalı route yapısı (oturum yoksa `/login`'e yönlendir)
- [ ] Oturum kalıcılığı (`localStorage` / Supabase session)
- [ ] Çıkış yapma
- [ ] Hata mesajları: yanlış OTP, süre doldu, telefon bulunamadı

---

### 🟦 AŞAMA 3 — Öğrenci Yönetimi

**Hedef:** Öğrenci eklenebilsin, listelenebilsin, detayı görüntülenebilsin.

- [ ] `studentService.js`: `getAll`, `getById`, `create`, `update`, `archive`
- [ ] `useStudents` hook'u
- [ ] `StudentListScreen`: liste + arama + aktif/pasif filtre
- [ ] `StudentFormScreen`: yeni öğrenci ekleme formu
  - [ ] Ad, yaş, veli adı, veli telefonu
  - [ ] Başlangıç sayfası, cüzü, suresi (`surahList.js` kullan)
  - [ ] Avatar rengi seçimi
  - [ ] Validasyon: zorunlu alanlar, telefon formatı
- [ ] `StudentDetailScreen`: profil kartı, hafızlık ilerleme çubuğu, sekmeler
- [ ] Öğrenci düzenleme (`StudentFormScreen` — edit modu)
- [ ] Pasife alma (silme değil, `is_active = false`)
- [ ] `surahList.js`: 114 sureyi sıralı dizi olarak tanımla

---

### 🟦 AŞAMA 4 — Ders & Tekrar Kaydı

**Hedef:** Günlük ders kaydedilebilsin, tekrar oturumları girilebilsin.

- [ ] `lessonService.js`: `getTodayLesson`, `createLesson`, `updateLesson`
- [ ] `repetitionService.js`: `addSession`, `getSessionsByLesson`
- [ ] `useLessons` hook'u
- [ ] `LessonFormScreen`: yeni ders girişi
  - [ ] Yeni sayfa sayısı
  - [ ] Ödev durumu (yapıldı / eksik / yapılmadı)
  - [ ] Performans değerlendirmesi
  - [ ] Serbest not alanı
- [ ] Tekrar oturumu formu (sabah / öğle / akşam)
  - [ ] Sayfa aralığı (başlangıç – bitiş)
  - [ ] Tekrar sayısı
  - [ ] Kalite notu (1–5 yıldız)
- [ ] Ders kaydedince öğrenci `current_page` güncelleme
- [ ] Ders geçmişi listesi (öğrenci detay — Dersler sekmesi)
- [ ] Bugün ders girilmişse form düzenleme modunda açılsın
- [ ] `LessonListScreen`: tüm öğrencilerin günlük ders özeti

---

### 🟦 AŞAMA 5 — Dashboard

**Hedef:** Ana sayfa günün durumunu tek bakışta göstersin.

- [ ] Günlük istatistik kartları
  - [ ] Toplam öğrenci
  - [ ] Bugün ders işlenen
  - [ ] Bekleyen öğrenci
  - [ ] Toplam yeni sayfa
- [ ] Öğrenci kart listesi (ilerleme çubuklu)
- [ ] Eksik ödev uyarı rozetleri
- [ ] Etüt notu olan öğrenci uyarı bandı
- [ ] Öğrenciye tıklayınca detay ekranına geçiş

---

### 🟦 AŞAMA 6 — Veli Bildirimi

**Hedef:** Velilere günlük rapor WhatsApp ile gönderilebilsin.

- [ ] `whatsappBuilder.js`: günlük ve haftalık rapor şablonları
- [ ] `StudentDetail` — Veli sekmesi
  - [ ] Veli bilgileri görünümü
  - [ ] Otomatik rapor metni önizlemesi
  - [ ] WhatsApp ile gönder butonu (deep-link)
- [ ] `ParentNotifyScreen`: tüm veliler, her biri için WA butonu
- [ ] Gönderildi durumu local state'te tutulsun
- [ ] `parent_notifications` tablosuna log yazma
- [ ] `notificationService.js`: log oluşturma fonksiyonu

---

### 🟦 AŞAMA 7 — Akşam Etüdü

**Hedef:** Yardımcı hocaya etüt notu bırakılabilsin ve yönetilebilsin.

- [ ] `etutService.js`: `addNote`, `getTodayNotes`, `markDone`
- [ ] `StudentDetail` — Etüt sekmesi
  - [ ] Not ekleme formu
  - [ ] Geçmiş notlar listesi (tamamlandı durumu)
- [ ] `EtutScreen`: bugünkü tüm etüt notları
  - [ ] Öğrenci bazlı gruplama
  - [ ] Tamamlandı / tamamlanmadı ayrımı
  - [ ] Tamamlandı işaretleme butonu
- [ ] Yardımcı hoca için rol bazlı görünüm kısıtı

---

### 🟦 AŞAMA 8 — Offline & PWA

**Hedef:** İnternet olmadan da ders kaydedilebilsin.

- [ ] `vite-plugin-pwa` kurulumu ve yapılandırması
- [ ] `manifest.json`: uygulama adı, ikonlar, tema rengi, standalone mod
- [ ] Service Worker: statik dosyalar ve API yanıtlarını önbelleğe al
- [ ] `useOfflineQueue` hook'u: çevrimdışı girişleri IndexedDB kuyruğuna ekle
- [ ] İnternet gelince kuyruktaki kayıtları Supabase'e yaz
- [ ] Bağlantı durumu göstergesi (online / offline banner)
- [ ] "Ana ekrana ekle" yönlendirme bildirimi

---

### 🟦 AŞAMA 9 — Raporlama

**Hedef:** Öğretmen ilerlemeyi görsel olarak takip edebilsin.

- [ ] Recharts entegrasyonu
- [ ] Haftalık yeni sayfa trendi (çizgi grafik)
- [ ] Aylık ödev tamamlama oranı (bar grafik)
- [ ] Öğrenci bazlı ilerleme özeti
- [ ] Sınıf geneli karşılaştırma tablosu
- [ ] En iyi 3 / en az gelişen 3 öğrenci kutusu
- [ ] PDF raporu dışa aktarma (`html2pdf.js`)

---

### 🟦 AŞAMA 10 — Üretim & Yayın

**Hedef:** Gerçek kullanıcılara açılan, kararlı bir sistem.

- [ ] Vercel'e deploy (GitHub otomatik CI/CD)
- [ ] Supabase üretim ortamı (geliştirme ortamından ayrı proje)
- [ ] Ortam değişkenlerini güvenli şekilde yönet
- [ ] Sentry hata izleme entegrasyonu
- [ ] Lazy loading ve code splitting optimizasyonu
- [ ] Lighthouse denetimi (PWA skoru, performans, erişilebilirlik)
- [ ] Kullanıcı kabul testi (2–3 öğretmenle pilot)
- [ ] Geri bildirim döngüsü ve düzeltme sprinti

---

## 9. Kurulum

```bash
# Projeyi oluştur
npm create vite@latest hafiz-takip -- --template react
cd hafiz-takip

# Bağımlılıkları yükle
npm install
npm install @supabase/supabase-js zustand react-router-dom
npm install recharts date-fns react-hot-toast
npm install -D tailwindcss postcss autoprefixer vite-plugin-pwa
npx tailwindcss init -p

# Ortam değişkenlerini tanımla
cp .env.example .env.local
```

**.env.local**
```
VITE_SUPABASE_URL=https://xxxx.supabase.co
VITE_SUPABASE_ANON_KEY=xxxx
```

```bash
# Geliştirme sunucusunu başlat
npm run dev
```

---

## 10. Geliştirme Notları

### Kodlama Standartları
- Tüm bileşenler fonksiyonel (hook tabanlı), class component yok
- Dosya adları: `PascalCase.jsx` (bileşenler), `camelCase.js` (utils / hooks / services)
- Her servis fonksiyonu `try/catch` ile sarılı, hata yukarı fırlatılır
- Supabase sorguları yalnızca `services/` klasöründe bulunur — bileşenler doğrudan sorgu yapmaz
- Zustand store'ları ince tutulur, iş mantığı `hooks/` katmanında kalır

### Tasarım Prensipleri
- Dominant renk: koyu yeşil `#1B5C3A`, vurgu: altın `#B8881A`
- Tüm dokunma hedefleri minimum `44×44 px`
- Yükleme durumları iskelet ekranla gösterilir (dönen spinner değil)
- Hata mesajları kullanıcı dostu Türkçe
- Boş durum ekranları yönlendirici mesajla desteklenir

### Bilinen Kısıtlamalar
- WhatsApp Business API ücretlidir; başlangıçta manuel deep-link yeterlidir
- Supabase ücretsiz planda aylık 50 OTP SMS limiti vardır
- PDF dışa aktarım büyük raporlarda sunucu taraflı çözüm gerektirebilir

---

*Proje başlangıç tarihi: Mayıs 2026*  
*Geliştirme modeli: Claude Opus 4.6 ile AI destekli iteratif geliştirme*
