# **Galerici Sim**

**Galerici Sim**, kullanıcıya bir **araç galerisi yönetim simülasyonu** sunan, **Unity ortamında geliştirilmiş 3D - FPS** türünde bir bilgisayar oyunudur. Oyuna **boş bir galeriyle** başlar, **araç alım-satımı yaparak** işinizi büyütür ve **kazandığınız parayla** daha modern araçlar satın alabilir ya da **ev gibi çeşitli mülkler edinebilirsiniz**.

---
## **Galeri Yapısı**
Oyun içinde yönettiğiniz **galeri**, iki ana kattan oluşmaktadır:

- **Alt Kat**:
  - **Garaj**: Özel araç tekliflerini kabul ettiğinizde teslimat burada gerçekleşir.
  - **6 Adet Platform**: Stoklarınızdaki araçları sergileyerek reklam yapabilirsiniz.

- **Üst Kat** (Ofis):
  - **Bilgisayar Masası**: Tüm işlemlerinizi yönetebileceğiniz 5 farklı seçenek içerir.
  - **Monitörler**: Gelen **mailleri** kontrol etmek ve **çalışanlarınızı yönetmek** için kullanılır.

---
## **Harita Tasarımı**
Oyun dünyasında farklı mekanlar bulunmaktadır:
- **Evler**
- **Park**
- **Stadyum**
- **Plaza**
- **Galeriniz**

---
## **Bilgisayar Kullanımı**
Galerinizin yönetimi için masaüstü bilgisayarınızdan aşağıdaki işlemleri gerçekleştirebilirsiniz:

1. **Galerideki Araçlar**  
   - Galerinizde bulunan araçları görüntüleyebilirsiniz.
   - Kirada olan araçlar gösterilmez ancak not olarak belirtilir.
   - Araç bilgilerini inceleyebilir ve platformlarda sergileyebilirsiniz.

2. **Teklifler**  
   - Galerinizdeki araçlara düzenli olarak **alıcı teklifleri** gelir.
   - Bu teklifleri değerlendirebilir, **pazarlık yapabilir** veya **kabul/red** edebilirsiniz.

3. **Alım-Satım Geçmişi**  
   - Yapmış olduğunuz tüm **araç alım ve satım işlemlerini** görüntüleyebilirsiniz.

4. **Araç Pazarı**  
   - Satışta olan araçları inceleyerek bütçenize uygun olanları satın alabilirsiniz.
   - "**Güncelle**" butonuyla piyasadaki araçları yenileyebilirsiniz.

5. **Galeri İsmini Değiştirme**  
   - Galerinizin ismini **en fazla 15 karakter olacak şekilde** değiştirebilirsiniz.

---
## **Mailler ve Çalışan Yönetimi**
Ofisinizde **iki monitör** bulunmaktadır:

- **Mail Monitörü**: Gelen mailleri inceleyerek **araç kiralama, özel üretim araç teklifleri, satılık evler, çalışan başvuruları** gibi konuları değerlendirebilirsiniz.
- **Çalışan Yönetimi Monitörü**: Çalışanlarınızı takip edip, onlara görevler verebilir ve **geri bildirimlerini** alabilirsiniz.

---
## **Araç Özellikleri**
Oyunda alım-satım yaparken dikkat etmeniz gereken **araç özellikleri** şunlardır:

1. **Geliş Fiyatı**  
   - Aracın satın alma fiyatıdır.
   - Alıcılar bu fiyattan **daha yüksek veya düşük** teklifler verebilir.

2. **Üretim Yılı**  
   - Aracın **üretildiği seneyi** gösterir.
   - **Özel üretim araçlarda** bu bilgi bulunmaz.

3. **Model Derecesi**  
   - Oyunda **1 ile 7 arasında** derecelendirilmiş **7 farklı araç modeli** vardır.
   - **Gelişmişlik seviyesiyle doğrudan bağlantılı değildir.**

4. **Kâr Beklentisi**  
   - Aracın kâr etme olasılığını belirtir. Örneğin, **%50 kâr beklentisi** olan bir araç için **yarı yarıya kâr etme veya zarar etme ihtimali vardır**.

5. **Kâr Oranı**  
   - Kâr edilen durumlarda **ne kadar kâr sağlanacağını** gösterir.
   - Örneğin, **%60 kâr oranı** olan bir araç **geliş fiyatından yaklaşık %60 daha fazla bir teklif alabilir**.

**Not**: **Gelişmiş araçlar, genellikle daha yüksek fiyat, kâr beklentisi ve kâr oranına sahiptir.**

---
## **KadaPlaza**
*KadaPlaza*, haritada bulunan **en yüksek bina** olup, **açık artırmalara katılabileceğiniz** ve **kumar oynayarak paranızı katlayabileceğiniz** bir mekandır.

- **Açık Artırmalar**: En yüksek teklifi veren alıcı aracı satın alır. Ancak **bütçenizi aşarsanız, araç bir önceki en yüksek teklif sahibine gider.**
- **Slot Makinesi**: 3 parçalı bir slot oyunu bulunur. **Farklı renkte, ikili eşleşen veya tamamen aynı renkte semboller** farklı kazanç oranları sağlar.

**Dikkat**: Kumar oynamak paranızı artırabileceği gibi **tamamen kaybetmenize de neden olabilir**.

---
## **Oyun Verilerinin Saklanması**
Oyundaki ilerlemeniz **JSON dosyaları** kullanılarak saklanır. **Galerinizdeki araçlar, alım-satım geçmişiniz, mailleriniz ve paranız** gibi tüm veriler **oyundan çıktığınızda kaybolmaz**.

---
## **ÖNEMLİ NOT: OYUNU ÇALIŞTIRMAK İÇİN**
GitHub’ın dosya yükleme sınırları nedeniyle, aşağıdaki **RESS dosyasını indirip** "**Galerici Sim_Data**" klasörüne koymanız gerekmektedir. Aksi takdirde **grafiklerde bozulmalar yaşayabilirsiniz**.

🔗 **[Gerekli Dosya Linki](https://drive.google.com/file/d/1ZeLCsqdqiTXAt6rI6DwENGt1Kl7ckAsa/view?usp=sharing)**

