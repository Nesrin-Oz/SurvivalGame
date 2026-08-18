3D Hayatta Kalma Prototipi (Staj Projesi)

Bu proje, yaz stajı kapsamında geliştirilmekte olan, FPS (Birinci Şahıs) bakış açısına sahip bir 3D hayatta kalma oyunu prototipidir. Oyuncu, sınırları belirlenmiş küçük bir alanda kaynaklarını doğru yöneterek temel istatistiklerini (Can, Açlık, Susuzluk) dengede tutmaya çalışır.

Geliştirici: Nesrin Özdemir  
Platform: PC (Windows)  
Oyun Motoru: Unity 6000.3.12f1  
Programlama Dili: C#  

Projeyi Çalıştırma Adımları
1. Bu depoyu bilgisayarınıza klonlayın:
   `git clone https://github.com/Nesrin-Oz/SurvivalGame.git`
2. Unity Hub'ı açın ve Add > Add project from disk seçeneğine tıklayarak klonladığınız klasörü seçin.
3. Proje açıldığında Assets/Scenes klasörü altındaki ana sahneyi çift tıklayarak açın.
4. Editör üzerinden Play butonuna basarak oyunu test edebilirsiniz.

Kontroller
* W, A, S, D: Karakter Hareketi
* Fare: Etrafa Bakma / Kamera Kontrolü
* Space: Zıplama
* E Tuşu: Çevredeki objeleri (Su/Yemek) toplama ve etkileşim
* 1 ve 2 Tuşları: Envanterdeki eşyaları (Yiyecek/İçecek) tüketme

Tamamlanan Özellikler (Prototip Kapsamında)
* Karakter Kontrolcüsü: Fizik kurallarına ve yerçekimine uygun çalışan, kamera açısı sınırlandırılmış FPS karakter donanımı.
* Hayatta Kalma Sistemi: Zamanla azalan Açlık (100) ve Susuzluk (100) değerleri ile bu değerler sıfırlandığında düşmeye başlayan Can (100) mekaniği.
* Toplayıcılık ve Envanter: SphereCast kullanılarak alan taraması yapan, toplanan eşyaları basit metin tabanlı UI üzerinde tutan sistem.
* Dinamik Çevre: Performans odaklı (_Tint ve Gradient kullanılarak optimize edilmiş) pürüzsüz Gece/Gündüz döngüsü.
* Arayüz : Farklı çözünürlüklere uyumlu (Canvas Scaler ve Anchor ayarlı), ikonlarla desteklenmiş dinamik TextMeshPro durum barları.

Kapsam Dışı Bırakılanlar ve Planlanan Geliştirmeler
* Proje Kapsamı: Proje yönergeleri ve onaylanan Oyun Tasarım Dokümanı (GDD) doğrultusunda; temiz, stabil ve birbiriyle tutarlı çalışan bir prototip ortaya çıkarmak hedeflenmiştir. Bu sebeple Düşman Yapay Zekası, Hayvanlar/Avlanma Sistemi, Crafting, Kayıt Sistemi ve Büyük Açık Dünya gibi özellikler kasıtlı olarak kapsam dışında bırakılmıştır. Animal.cs dosyasını ilk hafta belirlediğimiz 'önceliğimiz temiz ve hatasız bir prototip olmalı' kuralına ve onaylanan GDD'ye sadık kalmak adına şimdilik projeden çıkardım. Ancak temel mekanikleri, UI sistemini ve optimizasyonları planladığım süreden erken bitirmem durumunda, hayvanları ve avlanma mekaniğini projeye ekstra bir özellik olarak yeniden entegre etmeyi planlıyorum.
* Gelecek Güncellemeler: İlerleyen haftalarda görsel/işitsel cilalamalar (eşya tüketim sesleri, can azalma ekran efektleri) ve oynanış dengelemeleri yapılacaktır, susuzluk için haritaya eklemeler yapılacaktır.
