# 🧱 1. Başlangıç Seviyesi

## 🎯 Hedef
.NET web uygulamasının temel yapı taşlarını refleks haline getirmek.

---

## 📚 Konular

### 🔹 C# (İleri Temel)
- Records +
- async / await
- LINQ
- Exception Handling
- Generics  (temel + constraints)
### Concurrency & Async Internals
- Task vs Thread
- ThreadPool
- ConfigureAwait
- Race condition
- lock / SemaphoreSlim

### Performance Awareness 
- Garbage Collection (temel mantık)
- IDisposable & using
- Boxing / Unboxing

### Collections Deep Dive

* Dictionary vs List vs HashSet
* Big-O basics
* Concurrent collections

### Language Deep Features

* Pattern matching (advanced)
* records vs struct vs class farkları
* init-only properties

### Reflection & Attributes

* Reflection kullanımı
* Custom attribute yazma

### 🔹 ASP.NET Core Temelleri
- Request Pipeline
- Dependency Injection (DI)
- Configuration
- Middleware

### 🔹 API Geliştirme
- Minimal API
- REST Prensipleri
  - Status Codes
  - DTO kullanımı
  - Pagination
  - Filtering

### 🔹 Caching
- In-Memory Cache
- Distributed Cache
- Redis

### 🔹 Diğer Temel Yapılar
- Options Pattern
- Swagger / OpenAPI
- Logging (temel)
- Global Exception Handling
- Validation

---

## 🛠️ Proje Fikirleri

### 📦 Product Catalog API
- Ürün listeleme, ekleme, güncelleme, silme

### ✅ Todo API (Redis Cache ile)
- Cache kullanımıyla performans optimizasyonu

### 🌦️ Weather API
- External API entegrasyonu
- 3rd party servis kullanımı

---

## 📌 Her Projede Zorunlu Olanlar

- CRUD endpointleri
- Request / Response DTO yapısı
- Validation (FluentValidation önerilir)
- Global Error Handling
- Basic Logging
- Basit Unit Test (xUnit / NUnit)

---

## 🎯 Amaç
Bu seviyenin sonunda:
- API geliştirirken düşünmeden doğru patternleri uygulayabilmek
- Temiz ve sürdürülebilir backend yazabilmek
- Production-ready temel yapı kurabilmek