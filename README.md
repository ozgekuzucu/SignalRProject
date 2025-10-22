# 🍽️ SignalR ile QR Kodlu Restoran Sipariş Yönetimi

Bu proje, restoranların dijital dönüşümünü desteklemek amacıyla geliştirilen, **gerçek zamanlı veri akışı**, **QR kod ile sipariş verme** ve **dinamik yönetim paneli** özelliklerine sahip bir web uygulamasıdır.  
Uygulama, **ASP.NET Core 6.0**, **SignalR**, **Entity Framework Core** ve **N Katmanlı Mimari** prensipleriyle geliştirilmiştir.

---

## 🚀 Proje Özeti

Bu sistem, kullanıcıların masa üzerindeki QR kodları tarayarak doğrudan menüye erişebilmesini, sipariş verebilmesini ve rezervasyon oluşturabilmesini sağlar.  
Yönetici paneli üzerinden ise tüm sipariş, rezervasyon, ürün, kategori, masa ve istatistik işlemleri **gerçek zamanlı olarak** takip ve yönetilebilir.  

SignalR kütüphanesi sayesinde;  
- 🛎️ **Siparişler anlık olarak mutfağa iletilir**,  
- 💬 **Kullanıcı ve admin arasında mesajlaşma mümkündür**,  
- 📊 **Canlı istatistikler anında güncellenir**,  
- 🔔 **Bildirimler sayfa yenilenmeden gösterilir**.

---

## 🧩 Proje Mimarisi

Proje, **SOLID** prensiplerine uygun olarak **N Katmanlı Mimari** yapıda geliştirilmiştir:

### 🧱 Entity Layer  
Uygulamadaki temel varlıkların tanımlandığı katmandır.  
> Örneğin: `Product`, `Category`, `Reservation`, `Order`, `Table` vb.

### 💾 Data Access Layer (DAL)  
Veritabanı işlemlerinin gerçekleştirildiği katmandır.  
- Entity Framework Core kullanılmıştır.  
- Repository Design Pattern uygulanmıştır.  
- LINQ sorguları ile dinamik filtreleme sağlanmıştır.

### 🧠 Business Layer  
Uygulamanın iş kuralları burada yer alır.  
- Servis arayüzleriyle bağımlılıklar azaltılmıştır.  
- CRUD, validasyon ve özel mantık işlemleri bu katmanda gerçekleştirilir.

### 📦 DTO Layer  
Katmanlar arası veri transferini kolaylaştırmak için oluşturulmuştur.  
- AutoMapper kullanılarak dönüşümler otomatikleştirilmiştir.

### 🔗 API Layer  
RESTful mimarideki servislerin bulunduğu katmandır.  
- CRUD işlemleri API üzerinden gerçekleştirilir.  
- Swagger arayüzü ile test edilebilir yapıdadır.

### 🌐 Web UI Layer  
Kullanıcı arayüzü ve yönetim paneli bu katmandadır.  
- Razor Pages, AJAX, jQuery ve SignalR kullanılmıştır.  
- Mobil uyumlu, dinamik ve kullanıcı dostu tasarıma sahiptir.

---

## ✨ Öne Çıkan Özellikler

### 👨‍🍳 Kullanıcı Tarafı (Vitrin Paneli)
- 📋 Menü ve ürünleri görüntüleme  
- 📲 QR kod ile menüye erişim  
- 🛒 Masa seçimiyle sipariş oluşturma  
- 💳 Kupon indirimi ve ödeme adımları  
- 📅 Online rezervasyon oluşturma  
- 📧 Rezervasyon sonrası e-posta bildirimi  
- 💬 Mesaj gönderme ve iletişim formu  

### 🧑‍💼 Yönetici (Admin Paneli)
- 🧾 Ürün, kategori, indirim ve masa yönetimi  
- 🗂️ CRUD işlemleri (Ekleme, Güncelleme, Silme)  
- 🔔 Gerçek zamanlı bildirimler  
- 💬 Kullanıcılarla anlık mesajlaşma  
- 📊 Canlı istatistiksel veriler  
- 📅 Rezervasyon yönetimi  
- 📧 Mail gönderimi (MailKit ile)  
- 🪪 Admin bilgilerini düzenleme  
- 📱 Sosyal medya, hakkımızda, referans alanları yönetimi  
- 🧾 QR Kod oluşturma ve masa etiketleme  

---

## 🔄 Gerçek Zamanlı Özellikler (SignalR)
- 🛎️ Yeni rezervasyon ve sipariş bildirimleri  
- 📊 Anlık istatistik güncellemeleri  
- 💬 Canlı mesajlaşma  
- 🪑 Masa durumlarının gerçek zamanlı takibi  
- 🕒 Aktif kullanıcı sayısının canlı gösterimi  

---

## 🛠️ Kullanılan Teknolojiler

| Katman / Teknoloji | Açıklama |
|--------------------|----------|
| **Backend** | ASP.NET Core 6.0, Web API, SignalR |
| **Database** | MSSQL, Entity Framework Core (Code First) |
| **Frontend** | HTML, CSS, Bootstrap, JavaScript, jQuery, AJAX |
| **Mimari** | N Katmanlı Mimari (Entity, DAL, Business, DTO, API, UI) |
| **Doğrulama** | FluentValidation |
| **Kimlik Yönetimi** | ASP.NET Core Identity |
| **Eşleme** | AutoMapper |
| **Mail** | MailKit |
| **QR Kod** | QRCoder.dll |
| **Gerçek Zamanlı** | SignalR |
| **Dış API** | RapidAPI – TastyAPI Entegrasyonu |
| **Dokümantasyon** | Swagger |

---

## 🔐 Kimlik Doğrulama ve Yetkilendirme
- ASP.NET Identity ile kullanıcı giriş/çıkışı yönetilir.  
- Kullanıcı rolleri (Admin/User) tanımlanmıştır.  
- Yetkisiz erişimler otomatik olarak login sayfasına yönlendirilir.

---

## 📸 Ekran Görselleri
> 📷 Projeye ait görseller bu alanda paylaşılabilir.  
> Örneğin: “Vitrin Paneli”, “Admin Paneli”, “Masa Durumu”, “Gerçek Zamanlı Bildirimler” gibi başlıklarla eklenebilir.
