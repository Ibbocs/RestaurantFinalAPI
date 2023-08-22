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
2. Gerekli bağımlılıkları yüklemek için aşağıdaki komutu çalıştırın:
