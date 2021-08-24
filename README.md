# Eğitim Modülü
Eğitim Modülü, eğitim kurumlarının öğretmenleri ve öğrencileri yöneterek kendi eğitim sistemini online olarak uygulayabilecekleri bir uygulamadır.

  - Kurumlar
  - Öğretmenler
  - Öğrenciler

# Ne yapılabilir?

  - Öğretmenler, kurumdaki öğrenciler için online sınavlar düzenleyebilir
  - Öğretmenler, kurumdaki öğrenciler için döküman paylaşımında bulunabilir
  - Öğretmenler, kurumdaki öğrencileri istatistiksel analizler ile takip edebilir
  - Öğretmenler, kurumdaki öğrenciler ile iletişime geçebilir(düşünülüyor)
  - Kurumlar, öğretmenlerini ve öğrencilerini takip edip istatistiksel analizler ile çıkarımlar yapabilir


Ayrıca:
  - Kurumlar, kendi oluşumlarına uygun planlara göre hizmet alabilir
  >Planları tamamen dinamik olarak belirleme düşünülüyor.
  

## Projenin Tasarlanması

**Wiki**

* [Proje planı] - Projenin içeriklerinin belirlenmesi
* [Gereksinim analizi] - Projede bulunması gereken fonksiyonel gereksinimlerin gösterilmesi

### Projede kullanılması planlanan teknolojiler
**Backend için [.NET CORE]**

**Temelde kullanılabilecek bazı paketler(Backend)**

- spaservices : Microsot'un SPA'lar için sunduğu paketler(React js için olanı)
- npgsql : PostgreSQL bağlantısı
- automapper : Entity Modellerinin DTO olarak kullanılması için mapping işlemi
  

> Not: Authentication işlemleri için **JWT** bazlı doğrulama düşünülebilir**

**Frontend için [ReactJs]**
### Kullanılabilecek kurulum paketleri
- create-react-app(Client Side Rendering, klasik ReactJs)
- NextJs(Server Side Rendering)

**Temelde kullanılabilecek bazı paketler(Frontend)**

- axios : HTTP Requests 
- redux : Yönetim kütüphanesi(Veri yönetimi)
- connected-react-router : Routing işlemleri(redux ile uyumlu) 

**Düşünülen CSS Frameworkleri**
- Semantic UI
- Bootstrap
- Material UI
- Tailwind

**Database için [PostgreSQL], [MySQL] ya da [MSSQL] düşünülüyor**

**Projenin ürün aşamasında [DOCKER] desteği düşünülüyor**


### Geliştirme süreci için araştırmalar devam ediyor...


   [Proje planı]: <https://github.com/ndodanli/SinavModulu/wiki/AnaSayfa>
   [Gereksinim analizi]: <https://github.com/ndodanli/SinavModulu/wiki/Gereksinim-Analizi>
   [.NET CORE]: <https://dotnet.microsoft.com/download>
   [ReactJs]: <https://reactjs.org//>
   [PostgreSQL]: <https://www.postgresql.org/>
   [MySQL]: <https://www.mysql.com/>
   [MSSQL]: <https://www.microsoft.com/tr-tr/sql-server/sql-server-2019>
   [DOCKER]: <https://www.docker.com/>
