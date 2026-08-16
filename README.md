# ✈️ Jadoo Travel - Modern Seyahat Acentesi & Yönetim Sistemi

Jadoo Travel, seyahat rotalarının, kategorilerinin, kullanıcı yorumlarının ve rezervasyon taleplerinin yönetildiği, **MongoDB** tabanlı ve **Gemini Yapay Zeka (AI)** entegrasyonuna sahip modern bir seyahat acentesi yönetim paneli ve web uygulamasıdır.

---

## 🚀 Öne Çıkan Özellikler

### 1. 📊 Canlı Veri Destekli Dinamik Dashboard
*   **Haftalık Rezervasyon & Katılımcı Analizi (Bar Chart):** Gün bazında yeni eklenen rezervasyon taleplerini ve katılımcı (misafir) sayılarını dinamik olarak gruplayıp analiz eder.
*   **Rota Dağılımları (Donut Chart):** Alınan rezervasyonların hangi şehirlere yapıldığını oransal olarak gösterir.
*   **Toplam Ciro & Son 7 Günlük Trend (Sparkline Area):** Rezervasyonlardan elde edilen gelirleri ve günlük kazanç trendini görselleştirir.
*   **Son Talepler & Aktif Turlar:** Veritabanındaki en güncel 5 rezervasyon talebini ve aktif 4 tura ait detayları (fiyat, resim, kapasite) dinamik olarak listeler.

### 2. 🔐 MongoDB Destekli ASP.NET Core Identity
*   **Özel Veritabanı Deposu (Mongo Stores):** Entity Framework Core (SQL) bağımlılığı olmadan, `AspNetCore.Identity.MongoDbCore` entegrasyonu ile yerleşik Identity mekanizması doğrudan MongoDB'ye bağlanmıştır.
*   **Orijinal Giriş/Kayıt Arayüzleri:** Spike-Admin şablonunun orijinal arayüz tasarımlarına sadık kalınarak hazırlanan şık Giriş Yap ve Kayıt Ol ekranları.
*   **Güvenli Alanlar (`[Authorize]`):** Admin paneline ait tüm kritik işlemler yetkilendirme katmanı ile korunur.
*   **Dinamik Profil Yönetimi:** Giriş yapan kullanıcının Ad, Soyad ve E-posta bilgileri panel üstündeki profil menüsüne yansıtılır ve çıkış yapma (Logout) aksiyonu güvenli bir şekilde yönetilir.

### 3. 🪄 AI Rota Önerileri (Gezi Rehberi)
*   **Gemini 3.5 Flash-Lite:** Adminlerin girdikleri Şehir/Ülke bilgisine göre mutlaka görülmesi gereken en popüler 10 mekanı ve kısa Türkçe açıklamalarını anlık olarak üretir.
*   **localStorage Entegrasyonu:** Adminler kendi kişisel API anahtarlarını tarayıcılarında güvenli bir şekilde saklayabilir, sonraki girişlerde anahtarlar otomatik doldurulur.

### 4. 🔔 Canlı Bildirim Merkezi (Navbar Bell)
*   Sisteme yeni bir rezervasyon düştüğünde sağ üstteki zil butonu üzerinde kırmızı bildirim işareti belirir. Açılır menüde en son gelen 3 rezervasyon talebinin detayı listelenir ve tıklandığında rezervasyonlar sayfasına hızlıca gidilir.

### 5. 🌐 Yapay Zeka Destekli Çoklu Dil Altyapısı (Batch Translation)
*   Sözlük anahtarlarını sayfaların ilk render anında toplayıp (300ms gecikmeli kuyrukta biriktirerek) tek bir API çağrısıyla Gemini üzerinden topluca Türkçe, İngilizce, İspanyolca ve Fransızca dillerine çeviren yüksek performanslı toplu çeviri mekanizması kurulmuştur.

---

## 📸 Ekran Görüntüleri

### Yönetim Paneli (Dashboard)
![Dashboard](JadooTravel/wwwroot/image/dashboard.png)

### AI Gezi Rehberi Sayfası
![AI Gezi Rehberi](JadooTravel/wwwroot/image/ai_recommend.png)

### Giriş ve Kayıt Ekranları
![Giriş Ekranı](JadooTravel/wwwroot/image/login.png)
![Kayıt Ekranı](JadooTravel/wwwroot/image/register.png)

---

## 🛠️ Teknolojiler

*   **Framework:** ASP.NET Core 10.0 MVC (C#)
*   **Veritabanı:** MongoDB (NoSQL)
*   **Yapısal Tasarım:** Repository Pattern, DTO (Data Transfer Object) ve AutoMapper
*   **Güvenlik:** ASP.NET Core Identity & Cookie Authentication (NoSQL uyumlu)
*   **Grafikler:** ApexCharts (JavaScript)
*   **Tema/Arayüz:** Spike-Admin Bootstrap Şablonu

---

## ⚙️ Kurulum ve Çalıştırma

1.  **MongoDB Bağlantı Ayarı:**
    `JadooTravel/appsettings.json` dosyasını açarak `DatabaseSettingsKey` altındaki `ConnectionString` değerini kendi MongoDB adresinizle güncelleyin.
    ```json
    "DatabaseSettingsKey": {
      "ConnectionString": "mongodb://localhost:27017",
      "DatabaseName": "JadooTravelDb"
    }
    ```

2.  **Yapay Zeka API Anahtarı (Opsiyonel):**
    Eğer sistem genelinde varsayılan bir çeviri anahtarı kullanmak isterseniz `appsettings.json` içerisine ekleyebilirsiniz:
    ```json
    "GeminiApiKey": "YOUR_GEMINI_API_KEY"
    ```

3.  **Uygulamayı Başlatma:**
    Terminal üzerinden projenin ana dizininde aşağıdaki komutu çalıştırarak uygulamayı ayağa kaldırın:
    ```bash
    dotnet run --project JadooTravel/JadooTravel.csproj
    ```
