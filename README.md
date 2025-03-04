# Galerici Sim

Galerici Sim, kullanıcıya araç galerisi yönetimi simülasyonu sunan, Unity ortamında geliştirilmiş bir 3D - FPS bilgisayar oyunudur. Başlangıçta boş olarak devraldığınız galerinize yapacağınız alım-satımlarla yeni araçlar ekleyebilir ve kazandığınız para ile daha modern araçlar alıp satabilirsiniz veya ev satın alma gibi diğer satın alma işlemlerini yapabilirsiniz.

---

### Galeri Yapısı
Oyunda kullanmakta olduğunuz galerinizin alt katında *garaj* ve araç sergilemek için kullanılan *6 adet platform* bulunur. Stoktaki araçlarınızdan istediklerinizi reklam amaçlı bu platformlarda sergileyebilirsiniz. **Garaj**, kabul ettiğiniz özel araç teklifinizden sonra aracın galerinize teslimatı için kullanılır. **Platformlarda** ise istediğiniz araçları sergileyebilirsiniz. Üst kattakı *ofisinizde*; bilgisayar masanız, gelen mailleri kontrol etmek ve çalışanlarınızı yönetmek için de iki monitör bulunur.

---
### Harita Tasarımı
Oyundaki haritada farklı tipteki evler, park, stadyum, plaza ve galeriniz bulunmaktadır. 

---
### Bilgisayar
Masaüstü bilgisayarınızdaki 5 seçenek ile galeriniz ile ilgili tüm işlemleri gerçekleştirebilirsiniz. Bunlar:
1. **Galerideki Araçlar**  
   - Galerinizdeki araçları görebilirsiniz. 
   - O anda kirada olan araçlar görüntülenmez, köşede not olarak belirtilir.
   - Araçlarınız hakkındaki bilgileri görebilir ve uygun bir platformda sergileyebilirsiniz.
2. **Teklifler**  
   - Galerinizdeki araçlara alıcılar tarafından düzenli olarak teklifler gelmektedir.
   - Bu teklifleri bu ekrandan görüntüleyebilir ve değerlendirebilirsiniz.
   - Alıcı ile pazarlık yapabilir, teklifini kabul veya reddedebilirsiniz.
3. **Alım-Satım Geçmişi**  
   - Yapmış olduğunuz araç alımlarını ve satımlarını burada görüntüleyebilirsiniz.
4. **Araç Pazarı**  
   - Satılıkta olan araçlar arasından bütçenize uygun olanları galerinize katabilirsiniz.
   - "Güncelle" seçeneği ile pazardaki araçları yenileriyle güncelleyebilirsiniz.
5. **Galeri İsmini Değiştirme**  
   - Galerinizin ismini en fazla 15 karakter olmak üzere değiştirebilirsiniz.
  
--- 
### Mailler ve Çalışanlar Kontrol Monitörleri
Size düzenli olarak gönderilen mailleri görüntülemek için ofisinizde bir adet monitör bulunmaktadır. *Araç kiralama, satılık ev, özel üretim araç teklifi, galeride çalışma teklifi, çalışanlarınızdan geri bildirimler...* gibi konularda alacağınız maillerinizi değerlendirebilirsiniz.

Çalışanlarınızı görüntüleyebileceğiniz diğer monitörünüzden ise çalışanlarınıza yapmaları için iş verebilirsiniz. Çalışanlar iş sonunda size geri bildirimde bulunabilirler.

---
### Örnek Araç Özellikleri
Oyunda kullanılan araçların satışlarda ve alışlarda değerlendirmenizi gerektirecek özellikleri şu şekildedir:
1. **Geliş Fiyatı**  
   - Satın aldığınızdaki fiyattır.
   - Alıcılar bu fiyattan daha yüksek veya daha düşük tekliflerde bulunabilirler.
2. **Sene**  
   - Aracın üretim senesidir.
   - Özel üretim araçlar bu özelliği barındırmaz.
3. **Model Derecesi**  
   - Oyunda 1 ile 7 arasında numaralandırılmış 7 adet araç modeli bulunur.
   - Model derecesi ile aracın gelişmişliği arasında bir bağlantı yoktur.
4. **Kâr Beklentisi**  
   - Aracın kâr getirme olasılığını belirtir. Örneğin aracın kâr beklentisi %50 ise, o araca gelecek teklifler %50 ihtimalle geliş fiyatından yüksek olacaktır.
5. **Kâr Oranı**  
   - Aracın kâr getirmesi durumunda ne kadar oranda kâr getireceğini belirtir. Örneğin aracın kâr oranı %60 ise, o araca gelen ve geliş fiyatından yüksek tekliflerde teklif miktarı geliş fiyatından yaklaşık %60 fazla olur.

Doğal olarak araç ne kadar gelişmişse geliş fiyatı, kâr beklentisi ve oranı o kadar yüksek olur.

---
### KadaPlaza
*KadaPlaza*, oyun haritasında bulunan en yüksek bina olup açık arttırma oturumlarına katılabileceğiniz ve kumar oynayarak paranızı katlayabileceğiniz bir mekandır.

Açık arttırma oturumlarında sergilenen araca en yüksek teklifi sunan alıcı aracı almaya hak kazanır. Bütçenizi aşmasına rağmen en yüksek teklif sizden gelirse araç sizden önceki en yüksek teklifi sunan alıcıya verilir.

Kumar seçeneği olarak sadece slot oyunu bulunmaktadır. 3 parçalı slotta parçaların hepsinin farklı, sadece ikisinin aynı ve hepsinin aynı renkte olması durumlarına göre oranlar verilir. Şansınıza bağlı olarak para kazanabilir veya kaybedebilirsiniz.

---
### Verilerinizin Depolanması
Oyundan çıktığınızda verilerinizin kaybolmaması ve tekrar girdiğinizde ulaşabilmeniz iiçn tüm veriler JSON dosyası şeklinde saklanmaktadır. Araçlarınız, bilgisayar verileriniz, mail geçmişiniz ve mevcut paranız bu şekilde oyundan çıktığınızda kaybolmaz.
