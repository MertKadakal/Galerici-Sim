using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class AraçPazarı : MonoBehaviour {
    
    // Model numaralarına göre üretim yılı aralıkları
    public static Dictionary<int, List<int>> seneAraliklari = new Dictionary<int, List<int>>()
    {
        {1, new List<int> {2005, 2015}},
        {2, new List<int> {1990, 2010}},
        {3, new List<int> {2005, 2015}},
        {4, new List<int> {2000, 2010}},
        {5, new List<int> {1990, 2000}},
        {6, new List<int> {2015, 2025}},
        {7, new List<int> {2005, 2015}}
    };

    // Model numaralarına göre fiyat aralıkları
    public static Dictionary<int, List<int>> fiyatAraliklari = new Dictionary<int, List<int>>()
    {
        {1, new List<int> {100000, 120000}},
        {2, new List<int> {150000, 180000}},
        {3, new List<int> {300000, 350000}},
        {4, new List<int> {180000, 200000}},
        {5, new List<int> {120000, 150000}},
        {6, new List<int> {500000, 600000}},
        {7, new List<int> {90000, 100000}}
    };

    // Model numaralarına göre kar beklentileri
    public static Dictionary<int, List<int>> karBeklentileri = new Dictionary<int, List<int>>()
    {
        {1, new List<int> {50, 60}},
        {2, new List<int> {50, 60}},
        {3, new List<int> {70, 80}},
        {4, new List<int> {60, 65}},
        {5, new List<int> {40, 50}},
        {6, new List<int> {90, 95}},
        {7, new List<int> {50, 60}}
    };

    // Model numaralarına göre kar oranları
    public static Dictionary<int, List<int>> karOranlari = new Dictionary<int, List<int>>()
    {
        {1, new List<int> {70, 80}},
        {2, new List<int> {70, 80}},
        {3, new List<int> {50, 60}},
        {4, new List<int> {45, 55}},
        {5, new List<int> {80, 90}},
        {6, new List<int> {40, 50}},
        {7, new List<int> {90, 95}}
    };

    public TextMeshProUGUI para;
    public static DateTime start = DateTime.Now;
    public static DateTime last = DateTime.Now.AddSeconds(-60);
    public RectTransform content;
    public GameObject buttonPrefab;
    
    public List<Araba> pazardakiArabalar = new List<Araba> {
        new Araba(100000, 1, 2010, 60, 70),
        new Araba(150000, 2, 2015, 50, 70),
        new Araba(150000, 2, 2015, 55, 75),
        new Araba(90000, 7, 2005, 60, 90),
        new Araba(100000, 7, 2006, 50, 95),
        new Araba(95000, 7, 2006, 50, 100),
        new Araba(600000, 6, 2020, 95, 45),
        new Araba(120000, 5, 2012, 45, 80),
        new Araba(140000, 5, 2013, 40, 90),
        new Araba(120000, 1, 2010, 50, 75),
    };
    public static int modelDerece = 0;
    public GameObject btn;
    public bool firstclick = false;
    public Araba seçiliAraba;
    public Button guncelle;
    public TextMeshProUGUI kalanZaman;
    public GameObject kpstArtttir;
    public Button arttirBtn;
    public TextMeshProUGUI kpstMesj;

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

    void Update() {
        if (modelDerece > 0)
            btn.SetActive(true);
        start = DateTime.Now;

        if ((start-last).TotalSeconds < 60) {
            guncelle.interactable = false;
            kalanZaman.text = (int)(61-(start-last).TotalSeconds) + " saniye içinde güncelleyebilirsiniz";
        } else {
            guncelle.interactable = true;
            kalanZaman.text = "";
        }
        
        para.text = string.Format("Para : {0:N0}", AraçAlımSatım.para);
    }

    void OnDestroy()
    {
        modelDerece = 0;
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
        foreach (Araba araba in pazardakiArabalar) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content, false);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = string.Format("<b>Fiyat:</b> {0:N0}\n<b>Model Derecesi:</b> {1}\n<b>Sene:</b> {2}\n<b>Kâr Beklentisi:</b> %{3}\n<b>Kâr Oranı:</b> %{4}", araba.gelişFiyat, araba.modelDerece, araba.sene, araba.karBeklentisi, araba.karOrani);

            if (AraçAlımSatım.para < araba.gelişFiyat || AraçAlımSatım.arabalar.Count == AraçAlımSatım.galeriMax) {
                // Butonun Image bileşenini al
                Image buttonImage = button.GetComponent<Image>();
                // Mevcut renk değerini al
                Color currentColor = buttonImage.color;

                // Alpha (şeffaflık) değerini değiştirelim (0.5f = %50 şeffaflık)
                currentColor.a = 0.1f;

                // Yeni rengi butona uygula
                buttonImage.color = currentColor;
            } else {
                Button buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => ButonTıklanmaOlayı(araba));
            }
        }
    }

    void ButonTıklanmaOlayı(Araba araba) {
        modelDerece = araba.modelDerece;
        seçiliAraba = araba;
    }
    
    public void Back() {
        SceneManager.LoadScene(2);
    }

    public void SatinAl() {
        AraçAlımSatım.para -= seçiliAraba.gelişFiyat;

        AraçAlımSatım.arabalar.Add(new Araba(seçiliAraba.gelişFiyat, seçiliAraba.modelDerece, seçiliAraba.sene, seçiliAraba.karBeklentisi, seçiliAraba.karOrani));
        AraçAlımSatım.teklifPazarlıkGeçmişi.Add(false);
        pazardakiArabalar.Remove(seçiliAraba);

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        AlışGeçmişi.geçmişAlımlar.Insert(0, string.Format(
            "<b>Fiyat:</b> {0:N0}\n<b>Model Derecesi:</b> {1}\n<b>Sene:</b> {2}\n<b>Kâr Beklentisi:</b> %{3}\n<b>Kâr Oranı:</b> %{4}", 
            seçiliAraba.gelişFiyat,
            seçiliAraba.modelDerece,
            seçiliAraba.sene,
            seçiliAraba.karBeklentisi,
            seçiliAraba.karOrani
        ));

        AlışGeçmişi.geçmişAlımlarFiyat += seçiliAraba.gelişFiyat;

        ElemanlarıEkle();

        modelDerece = 0;
        btn.SetActive(false);
    }

    public void PazarıGuncelle() {
        firstclick = true;
        pazardakiArabalar.Clear();
        for (int i = 0; i < 10; i++) {
            System.Random random = new System.Random();
            int model = random.Next(1, 8);

            int min = fiyatAraliklari[model][0];
            int max = fiyatAraliklari[model][1];
            int fiyat = random.Next(min, max+1);

            min = seneAraliklari[model][0];
            max = seneAraliklari[model][1];
            int sene = random.Next(min, max+1);

            min = karBeklentileri[model][0];
            max = karBeklentileri[model][1];
            int karBeklentisi = random.Next(min, max+1);

            min = karBeklentileri[model][0];
            max = karBeklentileri[model][1];
            int karOranı = random.Next(min, max+1);

            pazardakiArabalar.Add(new Araba(fiyat, model, sene, karBeklentisi, karOranı));
        }

        foreach (Transform child in content) { Destroy(child.gameObject); }
        modelDerece = 0;
        btn.SetActive(false);
        ElemanlarıEkle();
        
        last = start;
    }

    public void KpstArttr() {
        kpstArtttir.SetActive(true);
        kpstMesj.text = string.Format("<b>{0:N0} lira</b> karşılığında {1} olan galeri kapasitesi {2} olarak değiştirilsin mi?",(AraçAlımSatım.galeriMax+10)*10000,AraçAlımSatım.galeriMax,(AraçAlımSatım.galeriMax+10));
        if (AraçAlımSatım.para < (AraçAlımSatım.galeriMax+10)*10000) {
            arttirBtn.interactable = false;
        }
    }

    public void Arttir() {
        AraçAlımSatım.galeriMax += 10;
        AraçAlımSatım.para -= (AraçAlımSatım.galeriMax)*10000;
        kpstArtttir.SetActive(false);
    }

    public void ArttirIptal() {
        kpstArtttir.SetActive(false);
    }
}