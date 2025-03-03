using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class Teklifler : MonoBehaviour {
    public RectTransform content;
    public GameObject buttonPrefab;
    public RectTransform seçili;
    public TextMeshProUGUI seçiliText;
    public TextMeshProUGUI karzarar;
    public static bool teklifSeçili = false;

    //ScrollView bileşeninin olduğu obje
    [SerializeField]
    RectTransform scrollParent;
    //Referans olarak kullanacağımız pozisyon
    Vector3 boyutlandirmaMerkezi;

    //ScrollView alanının büyüklüğü
    float alanGenisligi;
    //Objelerin genel büyüklüğü
    float altObjeGenisligi;

    /*
    Boyutlandırma için animationCurve kullanıyoruz.
    Bu sayede istediğimiz mesafe için istediğimi boyutu elle
    ayarlayabiliyoruz.
    */
    [SerializeField]
    AnimationCurve boyutlandirmaOrani;

    //Kontrol
    [SerializeField]
    BoyutlandirmaYonu yon = BoyutlandirmaYonu.yatay;

    //Horizontal veya Vertical kullanılabileceğinden ikisini de kontrol etmek için bu tipi kullanıyoruz.
    HorizontalOrVerticalLayoutGroup layoutGroup;

    void Start () {
        //Scrollview pozisyonu merkez noktasında olduğundan 
        boyutlandirmaMerkezi = scrollParent.position;
        layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup> ();
        GorunurAlanGenisligiHesapla ();
        AltObjeBoyutuHesapla ();
        ElemanlarıEkle();
    }

    //ScrollView OnValueChange üzerinden atanması gereken fonksiyon. Bu sayede her kaydırma sonucunda bu fonksiyon çağırılacak.
    public void ObjeleriBoyutlandir () {
        float yakinlikOrani;
        //her bir altobjeyi dolanıyoruz
        foreach (Transform altObje in transform) {
            //merkez arasındaki mesafeyi alıp, görünür alanda olabileceği en uzak mesafeye oranını elde ediyoruz
            float mesafe = Vector3.Distance (boyutlandirmaMerkezi, altObje.position);
            mesafe = Mathf.Abs (mesafe);
            yakinlikOrani = mesafe / alanGenisligi;
            //animationcurve üzerinden belirlediğimiz orana göre değeri çekmiş oluyoruz.
            //Animation curve değerinin loop modunda olmamasına dikkat edin. Derste bahsetmemiştim ama önemli bir konu
            altObje.localScale = Vector3.one * boyutlandirmaOrani.Evaluate (yakinlikOrani);
        }
    }

    /*
    her elemanın eşit boyutta olduğunu kabul ederek
    ilk elemandan boyut değerlerini alıyoruz.
    */
    void AltObjeBoyutuHesapla () {

        if (yon == BoyutlandirmaYonu.yatay) {
            altObjeGenisligi = transform.GetChild (0).GetComponent<RectTransform> ().rect.width;
        } else {
            altObjeGenisligi = transform.GetChild (0).GetComponent<RectTransform> ().rect.height;
        }
    }
    //Scroll içerisinde objelerin görünür olduğu alanın boyutlarını alıyoruz.
    void GorunurAlanGenisligiHesapla () {
        if (yon == BoyutlandirmaYonu.yatay) {
            alanGenisligi = scrollParent.rect.width;
        } else {
            alanGenisligi = scrollParent.rect.height;
        }
    }

    void ElemanlarıEkle() {
        for (int i = 0; i < AraçAlımSatım.teklifArabalar.Count; i++) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content, false);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("<b>Model Derecesi:</b> {0}\n<b>Sene:</b> {1}\n<b>Geliş Fiyatı:</b> {2:N0}\n<b>Edilen Teklif:</b> {3:N0}", 
            AraçAlımSatım.teklifArabalar[i].modelDerece, AraçAlımSatım.teklifArabalar[i].sene == -1 ? "Özel Üretim" : AraçAlımSatım.teklifArabalar[i].sene, AraçAlımSatım.teklifArabalar[i].gelişFiyat, AraçAlımSatım.teklifFiyatlar[i]);

            Button buttonComponent = button.GetComponent<Button>();

            // Geçici bir değişken ile i'nin değerini al
            int currentIndex = i;

            buttonComponent.onClick.AddListener(() => OnButtonClick(AraçAlımSatım.teklifArabalar[currentIndex], AraçAlımSatım.teklifFiyatlar[currentIndex], currentIndex));
        }
    }

    public void OnButtonClick(Araba araba, int teklifDegeri, int ind)
    {
        seçili.gameObject.SetActive(true);
        teklifSeçili = true;
        int totalPay = 0;
        foreach (Çalışan çalışan in Çalışanlar.çalışanlar) {
            totalPay += çalışan.pay;
        }
        seçiliText.text = string.Format("<b>Model Derecesi:</b> {0}\n<b>Sene:</b> {1}\n<b>Geliş Fiyatı:</b> {2:N0}\n<b>Edilen Teklif:</b> {3:N0}", 
            araba.modelDerece, araba.sene == -1 ? "Özel Üretim" : araba.sene, araba.gelişFiyat, teklifDegeri);

        if (araba.gelişFiyat < teklifDegeri) {
            karzarar.text = string.Format("Bu satıştan <b>{0:N0} lira</b> (%{1:F2}) kâr edeceksiniz\nÇalışan payı: &{2} ({3:N0} lira)", 
            teklifDegeri - araba.gelişFiyat - teklifDegeri*totalPay/100, ((float)(teklifDegeri - araba.gelişFiyat  - teklifDegeri*totalPay/100) / araba.gelişFiyat) * 100, totalPay, teklifDegeri*totalPay/100);
        } else {
            karzarar.text = string.Format("Bu satıştan <b>{0:N0} lira</b> (%{1:F2}) zarar edeceksiniz\nÇalışan payı: &{2} ({3:N0} lira)", 
            araba.gelişFiyat - teklifDegeri + teklifDegeri*totalPay/100, ((float)(araba.gelişFiyat - teklifDegeri  + teklifDegeri*totalPay/100) / araba.gelişFiyat) * 100, totalPay, teklifDegeri*totalPay/100);
        }

        TeklifSeçenekler.araba = araba;
        TeklifSeçenekler.teklif = teklifDegeri;
        TeklifSeçenekler.index = ind;

        StartCoroutine(BekleVeRestart());
    }

    public void Back()
    {
        /*
        if (teklifSeçili) {
            seçili.gameObject.SetActive(false);
            teklifSeçili = false;
        } else {
            SceneManager.LoadScene(3);
        }*/
        SceneManager.LoadScene(2);
    }
    public void RestartPanel() {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject); // Çocuk nesneyi yok et
        }

        ElemanlarıEkle();
    }

    public void Temizle() {
        AraçAlımSatım.teklifArabalar.Clear();
        AraçAlımSatım.teklifFiyatlar.Clear();
        AraçAlımSatım.teklifPazarlıkGeçmişi.Clear();
        seçili.gameObject.SetActive(false);
        RestartPanel();
    }

    IEnumerator BekleVeRestart()
    {
        while (teklifSeçili)
        {
            yield return null; // Bir frame bekle
        }

        RestartPanel();
    }
}