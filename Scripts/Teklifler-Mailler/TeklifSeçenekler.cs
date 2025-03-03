using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TeklifSeçenekler : MonoBehaviour
{
    
    public static Araba araba;
    public static int teklif;
    public RectTransform panel; 

    public static int index = 0;
    public TextMeshProUGUI seçiliText;
    public TextMeshProUGUI karzarar;
    public Button pazarlıkButonu;

    void Update() {
        if (AraçAlımSatım.teklifArabalar.Count > 0)
            pazarlıkButonu.interactable = !AraçAlımSatım.teklifPazarlıkGeçmişi[index];
    }
    public void Sat()
    {
        AraçAlımSatım.Sat(araba, teklif);
        panel.gameObject.SetActive(false);
        Teklifler.teklifSeçili = false;
    }

    public void GeriÇevir() {
        AraçAlımSatım.GeriÇevir(araba, teklif);
        panel.gameObject.SetActive(false);
        Teklifler.teklifSeçili = false;
    }

    public void Pazarlık() {
        index = AraçAlımSatım.teklifArabalar.IndexOf(araba);

        double katsayı = UnityEngine.Random.Range(6,20)/10.0;

        int yeniTeklif = (int)(araba.gelişFiyat + (teklif-araba.gelişFiyat)*katsayı);
        int totalPay = 0;
        foreach (Çalışan çalışan in Çalışanlar.çalışanlar) {
            totalPay += çalışan.pay;
        }

        AraçAlımSatım.teklifFiyatlar[index] = yeniTeklif;
        AraçAlımSatım.teklifPazarlıkGeçmişi[index] = true;
        pazarlıkButonu.interactable = false;

        seçiliText.text = string.Format("<b>Model Derecesi:</b> {0}\n<b>Sene:</b> {1}\n<b>Geliş Fiyatı:</b> {2:N0}\n<b>Edilen Teklif:</b> {3:N0}", 
            araba.modelDerece, araba.sene == -1 ? "Özel Üretim" : araba.sene, araba.gelişFiyat, yeniTeklif);
            
        if (araba.gelişFiyat < yeniTeklif) {
            karzarar.text = string.Format("Bu satıştan <b>{0:N0} lira</b> (%{1:F2}) kâr edeceksiniz\nÇalışan payı: &{2} ({3:N0} lira)", 
            yeniTeklif - araba.gelişFiyat  - yeniTeklif*totalPay/100, ((float)(yeniTeklif - araba.gelişFiyat  - yeniTeklif*totalPay/100) / araba.gelişFiyat) * 100, totalPay, yeniTeklif*totalPay/100);
        } else {
            karzarar.text = string.Format("Bu satıştan <b>{0:N0} lira</b> (%{1:F2}) zarar edeceksiniz\nÇalışan payı: &{2} ({3:N0} lira)", 
            araba.gelişFiyat - yeniTeklif  + yeniTeklif*totalPay/100, ((float)(araba.gelişFiyat - yeniTeklif  + yeniTeklif*totalPay/100) / araba.gelişFiyat) * 100, totalPay, yeniTeklif*totalPay/100);
        }
    }
}
