Natro Domain Manager



Proje Özeti
Bu proje, kullanıcıların domain (alan adı) sorgulama ve favorilere ekleme gibi işlemleri gerçekleştirebileceği bir web uygulamasıdır. Projede temel olarak .NET Core ve Entity Framework kullanılarak bir backend API ve Bootstrap ile MVC tabanlı bir frontend geliştirilmiştir.

Başlıca Özellikler:
Domain Arama: Kullanıcılar bir domain adı girerek domainin kayıt durumunu sorgulayabilir.
Favorilere Ekleme/Çıkarma: Domain bilgilerini favorilere ekleme ve listeden çıkarma işlemleri yapılabilir.
JWT Tabanlı Kimlik Doğrulama: Güvenli bir şekilde kullanıcı girişini doğrulamak için JWT kullanılmıştır.
Kullanılan Teknolojiler
Backend: .NET Core, Entity Framework Core, SQL Server
Frontend: ASP.NET MVC, Bootstrap, JavaScript
Veritabanı: SQL Server
Kimlik Doğrulama: JSON Web Token (JWT)
Kurulum Adımları
Projenin yerel ortamınızda çalıştırılması için aşağıdaki adımları takip edebilirsiniz.

Gereksinimler
.NET 7 SDK
SQL Server
Git
Kurulum
Projeyi İndir:

bash
Kodu kopyala
git clone https://github.com/EKaraaslan95/NatroDomainManagerCase.git
cd proje-adi
Veritabanı Yapılandırması: SQL Server'da bir veritabanı oluşturun ve appsettings.json dosyasındaki veritabanı bağlantı ayarlarını güncelleyin:

json
Kodu kopyala
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProjeVeritabani;User Id=kullanici;Password=sifre;"
}
Gerekli Paketleri Yükleyin:

bash
Kodu kopyala
dotnet restore
Veritabanı Migrasyonlarını Uygula:

bash
Kodu kopyala
dotnet ef database update
Uygulamayı Çalıştır:

bash
Kodu kopyala
dotnet run
Uygulama, multi set startup yapılmalı. Hem MVC hem de WEBAPI aynı anda çalıştırılmalı.

MVC:https://localhost:7021
WEBAPI:https://localhost:7091  portalından ayağa kalkacaklar.

Kullanım
Giriş Yap: Seed verisi ile oluşturulan 3 kullanıcı ile giriş yapılabilir.
Domain Arama: Ana sayfada bir domain adı aratarak kaydının olup olmadığını sorgulayabilirsiniz.
Favorilere Ekle/Çıkar: Sonuçları favorilere ekleyip çıkarabilir ve favori domainlerinizi görüntüleyebilirsiniz.
Proje Mimarisi
Bu proje aşağıdaki katmanlardan oluşmaktadır:


Core Katmanı (Domain Layer): Uygulamanın temel iş mantığını ve entity'lerini içerir. Veritabanı işlemleri burada tanımlanmaz, yalnızca domain nesneleri ve iş kuralları yer alır.

Application Katmanı: Uygulamanın iş mantığını, servisleri ve DTO'ları içerir. Core katmanındaki domain nesnelerini işler ve API katmanına hizmet verir. API katmanı ve UI katmanı bu katmandan hizmet alır.

Infrastructure Katmanı: Veritabanı işlemleri, API istekleri ve dış servisler gibi teknik detayların yer aldığı katmandır. Entity Framework Core burada kullanılır, veritabanı işlemleri ve repository'ler burada tanımlanır.

API Katmanı (Web API Layer): UI'dan gelen istekleri alır ve Application katmanına ileterek yanıtları döner. API Katmanı, HTTP ile ilgili işlemleri yönetir ve UI ile doğrudan iletişim sağlar.

MVC Katmanı: Kullanıcı arayüzünü ve temel iş akışını sağlar.

Ekran Görüntüleri
![Login](https://github.com/user-attachments/assets/fbbe4ebd-f9c3-440e-9e5c-ef690fc86115)

![Search](https://github.com/user-attachments/assets/fd0ad441-f4bf-402a-b7ce-2744fc05f82f)

![Favorities](https://github.com/user-attachments/assets/be49b24d-312e-43c5-a29c-897b62bc70bd)



İletişim
Bu proje ile ilgili sorularınız için lütfen benimle iletişime geçin:

Email: mrkaraaslan95@gmail.com
