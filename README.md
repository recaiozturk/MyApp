# MyApp - N-Layer Architecture with .NET 8 and Angular 16

Bu proje, N-Layer mimaride geliştirilmiş bir .NET 8 Web API ve Angular 16 frontend uygulamasıdır.

## 🏗️ Proje Yapısı

### Backend (.NET 8)
- **MyApp.Core**: Domain entities, interfaces, DTOs
- **MyApp.Data**: Repository pattern, Entity Framework, DbContext
- **MyApp.Services**: Business logic, application services
- **MyApp.API**: Web API controllers, configuration

### Frontend (Angular 16)
- **MyApp.Web**: Angular SPA uygulaması

## 🚀 Kurulum ve Çalıştırma

### Backend Kurulumu

1. **Veritabanı kurulumu:**
   ```bash
   cd MyApp.Data
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. **API projesini çalıştırma:**
   ```bash
   cd MyApp.API
   dotnet run
   ```
   API varsayılan olarak `https://localhost:7001` adresinde çalışacaktır.

### Frontend Kurulumu

1. **Bağımlılıkları yükleme:**
   ```bash
   cd MyApp.Web
   npm install
   ```

2. **Angular uygulamasını çalıştırma:**
   ```bash
   ng serve
   ```
   Frontend varsayılan olarak `http://localhost:4200` adresinde çalışacaktır.

## 📋 Özellikler

- **Product Management**: CRUD işlemleri
- **Repository Pattern**: Generic repository implementasyonu
- **AutoMapper**: Entity-DTO mapping
- **Entity Framework Core**: SQL Server veritabanı desteği
- **Angular Reactive Forms**: Form validasyonu
- **Bootstrap 5**: Modern UI tasarımı

## 🔧 Teknolojiler

### Backend
- .NET 8
- Entity Framework Core 8
- AutoMapper
- SQL Server

### Frontend
- Angular 16
- TypeScript
- Bootstrap 5
- RxJS

## 📁 API Endpoints

- `GET /api/products` - Tüm ürünleri listele
- `GET /api/products/{id}` - ID'ye göre ürün getir
- `POST /api/products` - Yeni ürün oluştur
- `PUT /api/products/{id}` - Ürün güncelle
- `DELETE /api/products/{id}` - Ürün sil

## 🎯 Mimari Prensipler

- **Separation of Concerns**: Her katman kendi sorumluluğuna sahip
- **Dependency Injection**: Loose coupling
- **Repository Pattern**: Data access abstraction
- **DTO Pattern**: Data transfer objects
- **Async/Await**: Asynchronous programming

## 📝 Notlar

- .NET 9 henüz preview aşamasında olduğu için .NET 8 kullanılmıştır
- LocalDB connection string kullanılmıştır
- CORS policy Angular uygulaması için yapılandırılmıştır



