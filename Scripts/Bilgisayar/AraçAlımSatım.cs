
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
[System.Serializable]


public class AraçAlımSatım : MonoBehaviour
{
    public static List<Araba> arabalar = new List<Araba> {};
    public static List<Araba> sergiler = new List<Araba> {new Araba(0,0,0,0,0),new Araba(0,0,0,0,0),new Araba(0,0,0,0,0),new Araba(0,0,0,0,0),new Araba(0,0,0,0,0),new Araba(0,0,0,0,0)};
    public static List<Araba> teklifArabalar = new List<Araba>{};
    public static List<int> teklifFiyatlar = new List<int>{};
    public static List<bool> teklifPazarlıkGeçmişi = new List<bool>{};
    public static int para = 500000;
    public static int galeriMax = 10;
    public TextMeshProUGUI modeText;
    public static int curr_id = 0;
    public static int curr_model = 0;
    public Button nextBtn;
    public Button preBtn;
    public Button sergileBtn;
    TMP_Text sergileBtnText;
    public TMP_Text kiradakiler;
    public GameObject panel;
    public bool sergiPanel = false;
    private System.Random rnd;  // Random nesnesi artık normal bir değişken olarak tanımlandı

    void Start()
    {
        sergileBtnText = sergileBtn.GetComponentInChildren<TMP_Text>();
        if (arabalar.Count == 0) {
            nextBtn.gameObject.SetActive(false);
            preBtn.gameObject.SetActive(false);
            sergileBtn.gameObject.SetActive(false);
            modeText.text = "Galerinizde şu an bir aracınız yok";
            kiradakiler.text = "";
        } else {
            rnd = new System.Random();  // System.Random nesnesi burada başlatılır
            if (arabalar[0].sene == -1) {
                modeText.text = string.Format("<b>Geliş Fiyatı:</b> {0:N0}\n<b>Model Derecesi:</b> {1}\n<b>Üretim Senesi:</b> Özel Üretim\n", arabalar[0].gelişFiyat, arabalar[0].modelDerece);
            } else {
                modeText.text = string.Format("<b>Geliş Fiyatı:</b> {0:N0}\n<b>Model Derecesi:</b> {1}\n<b>Üretim Senesi:</b> {2}\n", arabalar[0].gelişFiyat, arabalar[0].modelDerece, arabalar[0].sene);
            }
            curr_model = arabalar[0].modelDerece;
        }
    }

    void Update() {
        if (arabalar.Count > 0) {
            //next pre buton görünürlük
            if (curr_id == arabalar.Count-1) {nextBtn.interactable = false;} else {nextBtn.interactable = true;}
            if (curr_id == 0) {preBtn.interactable = false;} else {preBtn.interactable = true;}

            bool containsNull = false;
            bool containsTheCar = false;
            foreach (Araba araba in sergiler) {
                if (araba.gelişFiyat == 0) {
                    containsNull = true;
                }

                if (araba.gelişFiyat == arabalar[curr_id].gelişFiyat &&
                    araba.modelDerece == arabalar[curr_id].modelDerece &&
                    araba.sene == arabalar[curr_id].sene &&
                    araba.karBeklentisi == arabalar[curr_id].karBeklentisi &&
                    araba.karOrani == arabalar[curr_id].karOrani
                ) {
                    containsTheCar = true;
                }
            }
            if (containsTheCar || (!containsNull)) {
                if (containsTheCar) {
                    sergileBtnText.text = "Sergiden kaldır";
                } else {
                    sergileBtn.interactable = false;
                }
            } else {
                sergileBtnText.text = "Sergile";
            }

            //araç bilgisi
            if (sergiPanel) {
                modeText.text = "";
            } else {
                if (arabalar[curr_id].sene == -1) {
                    modeText.text = string.Format("Geliş Fiyatı: {0:N0}\nModel Derecesi: {1}\nÜretim Senesi: Özel Üretim\n", arabalar[curr_id].gelişFiyat, arabalar[curr_id].modelDerece);
                } else {
                    modeText.text = string.Format("Geliş Fiyatı: {0:N0}\nModel Derecesi: {1}\nÜretim Senesi: {2}\n", arabalar[curr_id].gelişFiyat, arabalar[curr_id].modelDerece, arabalar[curr_id].sene);
                }
            }

            //kiradakiler
            if (Mailler.kiradakiler.Count == 0) {
                kiradakiler.text = "Şu anda kirada bir araç yok";
            } else {
                kiradakiler.text = Mailler.kiradakiler.Count + " araç şu anda kirada";
            }
            
            //kira zamanları kontrolu
            List<KeyValuePair<Araba, DateTime>> geçiciKiradakiler = new List<KeyValuePair<Araba, DateTime>>(Mailler.kiradakiler);
            foreach (var eleman in geçiciKiradakiler)
            {
                if (eleman.Value <= DateTime.Now)
                {
                    Mailler.kiradakiler.Remove(eleman.Key);
                    arabalar.Add(eleman.Key);
                }
            }
        }
    }

    public static void Sat(Araba araba, int teklif)
    {
        arabalar.Remove(araba);  // Silinecek araba parametre olarak gelen araba olmalı
        int totalPay = 0;
        foreach (Çalışan çalışan in Çalışanlar.çalışanlar) {
            totalPay += çalışan.pay;
        }
        para += teklif * (100-totalPay)/100;

        for (int i = teklifArabalar.Count - 1; i >= 0; i--)  // Geriye doğru döngü
        {
            if (teklifArabalar[i].gelişFiyat == araba.gelişFiyat &&
                teklifArabalar[i].sene == araba.sene &&
                teklifArabalar[i].modelDerece == araba.modelDerece)
            {
                teklifArabalar.RemoveAt(i);
                teklifFiyatlar.RemoveAt(i);
                teklifPazarlıkGeçmişi.RemoveAt(i);
            }
        }

        SatışGeçmişi.geçmişSatışlar.Insert(0, string.Format(
            "Geliş Fiyatı: {0:N0}\nSatış Fiyatı: {1:N0}\nEdilen Kâr/Zarar: {2:N0}\nKâr/Zarar Oranı: %{3:F2}", 
            araba.gelişFiyat, 
            teklif, 
            teklif * (100-totalPay)/100 - araba.gelişFiyat, 
            ((teklif * (100-totalPay)/100 - araba.gelişFiyat) / (double)araba.gelişFiyat) * 100 // Burada double dönüşümü önemli!
        ));

        if (sergiler.Contains(araba)) {
            sergiler[sergiler.IndexOf(araba)] = null;
        }
    }

    public static void GeriÇevir(Araba araba, int teklif)
    {
        for (int i = 0; i < teklifArabalar.Count; i++) {
            if (teklifArabalar[i].gelişFiyat == araba.gelişFiyat &&
                teklifArabalar[i].sene == araba.sene &&
                teklifArabalar[i].modelDerece == araba.modelDerece &&
                teklifFiyatlar[i] == teklif ) {
                    teklifArabalar.RemoveAt(i);
                    teklifFiyatlar.RemoveAt(i);
                    teklifPazarlıkGeçmişi.RemoveAt(i);
                    break;
            }
        }
        
    }

    public void Al(Araba araba)
    {
        arabalar.Add(araba);  // List'e araba eklerken Add kullanın
    }

    public void next() {
        if (curr_id < arabalar.Count - 1) {
            curr_id++;
            curr_model = arabalar[curr_id].modelDerece;
        }
    }

    public void pre() {
        if (curr_id > 0) {
            curr_id--;
            curr_model = arabalar[curr_id].modelDerece;
        }
    }

    public void Back() {
        if (sergiPanel) {
            panel.SetActive(false);
            sergiPanel = false;
            return;
        }
        SceneManager.LoadScene(2);
    }

    public void Sergile() {
        panel.SetActive(true);
        sergiPanel = true;
    }
}
