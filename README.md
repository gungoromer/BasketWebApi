# BasketWebApi
Sepete ürün eklemek için .net 5 web api ile yazılmış iş görüşmesi ödevi.

# Kullanılan Öğeler
.Net 5, Sql Server, EntityFrameworkCore, Codefirst, FluentValidation, Swagger, SeriLog, UnitOfWork

## Kurulum
- Programı ayağı kaldırmak için tek yapılması gereken BasketWebApi/BasketWebApi/BasketWebApi/appsettings.json dosyasındaki DefaultConnection özelliğini kendi lokal sql sunucunuza yönlendirmek. 

- Bunun sonrasında direkt projeyi "BasketWebApi" projesiyle birlikte çalıştırabilirsiniz. 
- 
- Migrate ile uğraşmamanız adına projenin startında direkt migrate işlemi yapılıyor.


## Genel Açıklamalar
- Kullanıcı ile ilgili bir yapı kurulmadığı için User işlemleri hiç yapılmadı.  Sadece sepetin aynı kullanıcıdan varolup olmadığı kontrolü için UserID kolonu eklendi ve 1 idsi kullanıldı. Kodun içerisinde de incelidğinde açıklamalar görebilirsiniz.
- Loglama işlemi dosya üzerine "BasketWebApi/logs" klasörüne otomatik yapılıyor.
- 
