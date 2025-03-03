using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class TeklifMailYöneticisi : MonoBehaviour
{
    private System.Random rnd = new System.Random();
    public CanvasGroup panelTeklif;  // CanvasGroup referansı
    public CanvasGroup panelMail;  // CanvasGroup referansı

    void Start()
    {
        // Coroutine'i başlat
        SetPanelVisibility(false,panelTeklif);
        SetPanelVisibility(false,panelMail);
        StartCoroutine(TeklifYapCoroutine());
        StartCoroutine(MailAtCoroutine());
    }

    IEnumerator TeklifYapCoroutine()
    {
        // Sürekli döngü ile teklif işlemi
        while (true)
        {
            float bekleme = (float)rnd.Next(30, 60);
            //float bekleme = (float)rnd.Next(0, 5);
            yield return new WaitForSeconds(bekleme);
            if (!FirstPersonLook.stopped && AraçAlımSatım.arabalar.Count > 0) {
                TeklifYap(AraçAlımSatım.arabalar[UnityEngine.Random.Range(0, AraçAlımSatım.arabalar.Count)]);
                SetPanelVisibility(true,panelTeklif);
                yield return new WaitForSeconds(2f);
                SetPanelVisibility(false,panelTeklif);
            }
        }
    }

    IEnumerator MailAtCoroutine()
    {
        // Sürekli döngü ile teklif işlemi
        while (true)
        {
            float bekleme = (float)rnd.Next(30, 60);
            //float bekleme = (float)rnd.Next(0, 5);
            yield return new WaitForSeconds(bekleme);

            if (!FirstPersonLook.stopped) {
                if (AraçAlımSatım.arabalar.Count == 0) {
                    Debug.Log("bb");
                    MailAt(new Araba(0,0,0,0,0));
                } else {
                    Debug.Log("aa");
                    MailAt(AraçAlımSatım.arabalar[rnd.Next(0, AraçAlımSatım.arabalar.Count)]);
                }
                SetPanelVisibility(true,panelMail);
                yield return new WaitForSeconds(2f);
                SetPanelVisibility(false,panelMail);
            }
            
            
        }
    }

    // Teklif işlemini yapma
    public void TeklifYap(Araba araba)
    {
        double karzarar = rnd.NextDouble();
        double teklif;

        if (karzarar*100 > araba.karBeklentisi) {
            teklif = araba.gelişFiyat - araba.gelişFiyat * ((100.0 - (araba.karOrani-10+rnd.NextDouble()*10)) / 100.0);
        } else {
            teklif = araba.gelişFiyat + araba.gelişFiyat * ((araba.karOrani-10+rnd.NextDouble()*10) / 100.0);
        }
        
        AraçAlımSatım.teklifArabalar.Add(araba);
        AraçAlımSatım.teklifFiyatlar.Add((int) teklif);  // Teklifi ekle
        AraçAlımSatım.teklifPazarlıkGeçmişi.Add(false);
    }

    public void MailAt(Araba araba)
    {
        int sayi;
        if (araba.gelişFiyat == 0) {
            sayi = UnityEngine.Random.Range(2, 5); // ilk 2 ihtimali dahil etme
        } else {
            sayi = UnityEngine.Random.Range(0, 5);
        }

        //test
        if (sayi == 0) {
            Mailler.mailler.Add(new Mail("test", araba, DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss"), 
                "Yukarıda bilgilerini vermiş olduğum araç için test sürüşü talep ediyorum", null));
            return;
        }

        //kira
        if (sayi == 1) {
            int saat = (int)(1 + 2 * rnd.NextDouble());
            int kira = (araba.gelişFiyat / 100) + (int)(3000 * rnd.NextDouble());
            Mailler.mailler.Add(new Mail("kira", araba, DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss"), 
                "Yukarıda bilgilerini vermiş olduğum aracı " + kira.ToString("N0") + " liraya " +
                saat + " saat kiralamak istiyorum", new List<int> {kira, saat}, DateTime.Now.AddMinutes(saat)));
            return;
        }

        //özel
        if (sayi == 2) {
            int model = UnityEngine.Random.Range(1, 8);
            int sene = UnityEngine.Random.Range(AraçPazarı.seneAraliklari[model][0], AraçPazarı.seneAraliklari[model][1]);
            int max = AraçPazarı.fiyatAraliklari[model][1];
            int maxKarB = AraçPazarı.karBeklentileri[model][1];
            int maxKarO = AraçPazarı.karOranlari[model][1];
            int yeniFiyat = (int)((max * 1.2) + ((max / 100.0) * rnd.NextDouble()));
            int yeniKarB = ((int)(maxKarB + (100 - maxKarB) * (rnd.NextDouble()/2)));
            int yeniKarO = ((int)(maxKarO + (100 - maxKarO) * (rnd.NextDouble()/2)));
            string karBmsj = "";
            string karOmsj = "";
            if (maxKarB == yeniKarB) {
                karBmsj =  "Aracın kâr beklentisini %"+maxKarB+ " olarak";
            } else {
                karBmsj = "Normalde maksimum %"+maxKarB+" olan kâr beklentisini bu özel araç için %"+yeniKarB; 
            }
            if (maxKarO == yeniKarO) {
                karOmsj =  "aracın kâr oranını ise %"+maxKarB+" olarak belirledik.";
            } else {
                karOmsj = "normalde maksimum %"+maxKarO+" olan kâr oranını ise %"+yeniKarO+" olarak belirledik."; 
            }

            Mailler.mailler.Add(new Mail("özel", new Araba(yeniFiyat, model, sene, yeniKarB, yeniKarO), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
            "Merhaba, size " + model + " model dereceli özel üretim bir araç sunuyoruz. Normalde maksimum " +max.ToString("N0") + " liradan satılan bu özel üretim araç için talebimiz " +yeniFiyat.ToString("N0") + " lira. "+ karBmsj + " ve " + karOmsj +
            "\n\nBu teklifimizi kabul edersen aracı galerine teslim edebiliriz.", new List<int> {yeniFiyat, yeniKarB, yeniKarO}));
            
            return;
        }

        //çalışma
        if (sayi == 3 && Çalışanlar.çalışanlar.Count < 3) {
            List<string> isimler = new List<string>
            {
                "Ahmet", "Mehmet", "Mustafa", "Ali", "Emre",
                "Burak", "Can", "Eren", "Kerem", "Yusuf",
                "Hakan", "Ömer", "Tuncay", "Baran", "Fatih",
                "İbrahim", "Kaan", "Serkan", "Uğur", "Mert",
                "Zeynep", "Elif", "Ayşe", "Fatma", "Merve",
                "Gamze", "Ece", "Ceren", "Sude", "İrem",
                "Büşra", "Deniz", "Derya", "Selin", "Melis",
                "Sena", "Ezgi", "Hande", "Tuğçe", "Gizem"
            };

            int pay = UnityEngine.Random.Range(5, 16);
            string isim = isimler[UnityEngine.Random.Range(0, 41)];

            Mailler.mailler.Add(new Mail("çalışma", null, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
            "Merhaba, ben "+isim+". Şirketinizde çalışmak istiyorum. Gelir olarak satış başına %"+pay+" pay talep ediyorum. En kısa sürede geri dönüşünüzü bekliyorum.\n\nİyi günler.", new List<int> {pay}, isim));
            
            return;
        }

        //satılık ev
        if (sayi == 4) {
            Dictionary<int, List<int>> fiyatAraliklari = new Dictionary<int, List<int>>()
            {
                {0, new List<int> {500000, 1000000}},
                {1, new List<int> {1000000, 5000000}},
                {2, new List<int> {3500000, 7000000}},
                {3, new List<int> {6000000, 10000000}},
                {4, new List<int> {8000000, 15000000}},
                {5, new List<int> {12000000, 2000000}}
            };

            Dictionary<int, List<int>> idAraliklari = new Dictionary<int, List<int>>()
            {
                {0, new List<int> {0, 9}},
                {1, new List<int> {10, 16}},
                {2, new List<int> {17, 20}},
                {3, new List<int> {21, 23}},
                {4, new List<int> {24, 28}},
                {5, new List<int> {28, 44}}
            };

            //List<string> binaTipIsimleri = new List<string> {"0","1","2","3","4"};

            int daireTip = UnityEngine.Random.Range(0, 5);
            int fiyat = UnityEngine.Random.Range(fiyatAraliklari[daireTip][0], fiyatAraliklari[daireTip][1]+1);
            int id = UnityEngine.Random.Range(idAraliklari[daireTip][0], idAraliklari[daireTip][1]+1);

            Mailler.mailler.Add(new Mail("satılık",null, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
            $"Merhaba, emlakçınız olarak ilgilenebileceğiniz yeni bir konutun satılığa çıktığının haberini vermek istedim."+
            $" Konut {UnityEngine.Random.Range(1, 6)}. tip ve fiyatı da {fiyat:N0} lira. Umarım beğenirsiniz.", new List<int>{fiyat, id}));
            
            return;
        }
    }

    // Panel görünürlüğünü ayarlama
    public void SetPanelVisibility(bool isVisible, CanvasGroup panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        cg.alpha = isVisible ? 1f : 0f;
        cg.interactable = isVisible;
        cg.blocksRaycasts = isVisible;
    }
}