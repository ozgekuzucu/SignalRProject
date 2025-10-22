# 🍽️ SignalR ile QR Kodlu Restoran Sipariş Yönetimi

Bu proje, **gerçek zamanlı veri akışı**, **QR kod ile sipariş verme**, **dinamik yönetim paneli** ve **Google Gemini AI destekli akıllı restoran asistanı (chatbot)** özelliklerine sahip bir web uygulamasıdır.  
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

## 🤖 AI Chatbot - Restoran Asistanı
 Projede **Google Gemini AI** kullanılarak 7/24 hizmet veren akıllı bir chatbot yer almaktadır.  
 SignalR ile gerçek zamanlı iletişim sağlanarak sipariş takibi, menü bilgisi ve müşteri sorularına anında yanıt verir.
 
 Temel Özellikler:
 - 📦 Sipariş takibi ve anlık durum sorgulama
 - 🍽️ Menü ve ürün bilgilerini görüntüleme
 - 💬 Türkçe dil desteğiyle doğal konuşma
 - 🧠 Masa bilgisi hatırlama

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
Mimarideki servislerin bulunduğu katmandır.  
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
| **Database** | MSSQL, Entity Framework Core |
| **Frontend** | HTML, CSS, Bootstrap, JavaScript, jQuery, AJAX |
| **Mimari** | N Katmanlı Mimari (Entity, DAL, Business, DTO, API, UI) |
| **Doğrulama** | FluentValidation |
| **Kimlik Yönetimi** | ASP.NET Core Identity |
| **Eşleme** | AutoMapper |
| **Mail** | MailKit |
| **QR Kod** | QRCoder.dll |
| **Gerçek Zamanlı** | SignalR |
| **RapidAPI** | RapidAPI – TastyAPI Entegrasyonu |
| **Dokümantasyon** | Swagger |

---

## 🔐 Kimlik Doğrulama ve Yetkilendirme
- ASP.NET Identity ile kullanıcı giriş/çıkışı yönetilir.  
- Yetkisiz erişimler otomatik olarak login sayfasına yönlendirilir.

---

## 📸 Ekran Görselleri

>  ### 🏠 Ana Sayfa
 ![Ana Sayfa](/images/main.jpeg)

> ### 📲 Masa Erişimi
 ![QR Kod Menü](/images/default-masa.jpeg)

> ### 🍔 Ürün ve Kategori Görüntüleme
![Ürün Listesi](/images/menu.jpeg)

> ### 🤖 Canlı Chat / Chatbot
![Chatbot](/images/chatbot.png)
![Chatbot](/images/chatbot2.png)

> ### 🛒 Sepet Sayfası
![Sipariş Adımı](/images/default-basket.jpeg)

> ### 📅 Rezervasyon Formu
![Rezervasyon](/images/default-reservation.jpeg)

> ### ✉️ Rezervasyon Sonrası Mail Bildirimi
![Mail Bildirimi](/images/mail-page.jpeg)
![Mail Bildirimi](/images/mail.png)

> ### 💬 Canlı Mesajlaşma (SignalR)
![Mesajlaşma](/images/signalr-anlikmesajlaşma.jpeg)

> ### 🍲 Tarifler (Recipes)
![Tarifler](/images/recipes.jpeg)

> ### 📋 Menü Sayfası
![Menü](/images/menu.jpeg)
![Menü](/images/menu2.jpeg)

> ### ⚠️ Hata Sayfası (404 / Error Page)
![Hata Sayfası](/images/error.jpeg)

> ### 🧑‍💼 Admin Giriş Sayfası
![Admin Login](/images/login.png)

> ### 🧾 Admin Paneli - Sipariş Yönetimi
![Admin Siparişler](/images/admin-reservation-list.jpeg)
![Admin Siparişler](/images/admin-reservation-update.jpeg)
![Admin Siparişler](/images/admin-add-reservation.jpeg)

> ### 📊 Gerçek Zamanlı İstatistikler (SignalR)
![Gerçek Zamanlı Veriler](/images/admin-statistics.jpeg)

> ### 🪪 Admin Bilgi Güncelleme
![Admin Profili](/images/admin-settings.jpeg)

> ### 🧾 QR Kod Oluşturma Ekranı
![QR Kod Yönetimi](/images/qr-code.jpeg)

### ℹ️ Admin Hakkımızda Sayfası
![Hakkımızda Yönetimi](/images/admin-about.jpeg)

> ### 🏷️ Admin Kategori Yönetimi
![Kategori Yönetimi](/images/admin-category-update.jpeg)
![Kategori Yönetimi](/images/admin-add-category.jpeg) 

> ### 🌟 Admin Öne Çıkanlar / Özellikler
![Öne Çıkanlar](/images/admin-feature.jpeg)

> ### 🪑 Admin Masa Yönetimi
![Masa Yönetimi](/images/admin-masa.jpeg)

> ### 📅 Admin Rezervasyon Yönetimi
![Rezervasyon Yönetimi](/images/admin-reservation-list.jpeg)
![Rezervasyon Yönetimi](/images/admin-add-reservation.jpeg)
![Rezervasyon Yönetimi](/images/admin-reservation-update.jpeg)

> ### 🔔 Admin Bildirim ve Ayarlar
![Bildirim ve Ayarlar](/images/admin-notification.jpeg)

> ### 📱 Admin Sosyal Medya Yönetimi
![Sosyal Medya](/images/admin-social-media.jpeg)

> ### 📝 Admin Referans / Yorum Yönetimi
![Referanslar](/images/admin-referans.jpeg)

> ### 🍽️ Admin Ürün Yönetimi
![Ürün Yönetimi](/images/admin-product-list.jpeg)
![Ürün Yönetimi](/images/admin-update-product.jpeg)
![Ürün Yönetimi](/images/admin-add-product.jpeg)


