# Restaurant API Projesi

Bu proje, bir restoranın menü öğelerini, kategorilerini, masalarını ve rezervasyonlarını yönetmek için bir ASP.NET 6 Web API uygulamasıdır.

## Kullanılan Teknolojiler ve Prensipler

- ASP.NET 6
- Entity Framework Core
- Serilog
- Onion Architecture
- JWT tabanlı Kimlik Doğrulama ve Yetkilendirme
- SOLID Prensiplerine Uygun Tasarım
- AutoMapper
- Generic Custom Exception Handling
- Repository Pattern
- Unit of Work Pattern
- Generic Response Modelleri
- Fluent Validation
- Yumuşak Silme (Soft Delete)
- Global Exception Handler Middleware
- Cross-Origin Resource Sharing (CORS) Politikaları

## Proje Amaçları

Bu proje, aşağıdaki amaçları hedefler:

- Kullanıcılara restoranın menü öğelerini ve kategorilerini görüntüleme olanağı sunar.
- Kullanıcılara mevcut masaların durumlarını (dolu, boş veya rezerve) gösterir.
- Kayıtlı olmayan kullanıcılar menü öğelerini ve kategorilerini görüntüleyebilir, masaların durumunu görebilir.
- Kayıtlı kullanıcılar rezervasyon yapabilir.
- Admin kullanıcıları, rezervasyonları onaylayabilir veya reddedebilir.
- Admin kullanıcıları, menü öğelerini, kategorileri, masaları ve diğer detayları düzenleyebilir.

## Kurulum

1. Bu depoyu yerel bilgisayarınıza klonlayın.
2. Gerekli bağımlılıkları yükleyiniz. (dotnet restore)
3. Veritabanını oluşturmak ve örnek verileri eklemek. (dotnet ef database update ve dotnet run seed)
4. Uygulamayı başlatmak için aşağıdaki komutu çalıştırın (dotnet run)
5. API, `https://localhost:7085` adresinde çalışmaya başlayacaktır.

## API Dökümantasyonu

API'ye erişim ve kullanım detayları için [API Dökümantasyonu](docs/api-docs.md) sayfasını inceleyebilirsiniz.

## Katkı Sağlama

Eğer projeye katkıda bulunmak isterseniz, lütfen [Katkı Sağlama Rehberi](CONTRIBUTING.md)ni inceleyin.

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.

---

Daha fazla bilgi ve yardım için lütfen [İletişim](mailto:contact@example.com) adresinden bizimle iletişime geçin.

