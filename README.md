# 🧠 .NET 101

Bu repo, **.NET platformunu derinlemesine öğrenmek** isteyen geliştiriciler için adım adım oluşturulmuş bir çalışma alanıdır.  

---

## 🚀 Amaç

Bu projenin amacı, sıfırdan başlayan veya .NET Core bilgisini pekiştirmek isteyen geliştiricilere **uygulamalı bir öğrenme yolu** sunmaktır.

**Kapsam:**
- .NET SDK kurulumu ve proje yapısı
- Minimal API kullanımı
- In-Memory ve Distributed Caching

---

## 🧩 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|------------|-----------|
| .NET 9 | Modern .NET platformu |
| C# 12 | Programlama dili |
| Swagger / Swashbuckle | API dokümantasyonu |
| Minimal API | Lightweight REST API yapısı |
|In-Memory Cache|Local Cache|
|Distributed Cache|Redis|

---

## ⚙️ Kurulum

### 1️⃣ .NET SDK’yı yükle
[.NET SDK](https://dotnet.microsoft.com/en-us/download)  
Kurulumdan sonra terminalde sürümü kontrol et:
```bash
docker run --name local-redis -p 1453:6379 -d redis
dotnet run
