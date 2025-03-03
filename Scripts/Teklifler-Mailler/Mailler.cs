
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Collections;
[System.Serializable]

public class Mailler : MonoBehaviour {
    public RectTransform content;
    public static List<Mail> mailler = new List<Mail>{};
    public bool mailSeçili = false;
    public GameObject buttonPrefab;
    public GameObject mailPanel;
    public TextMeshProUGUI mailMesaj;
    public TextMeshProUGUI onayText;
    public TextMeshProUGUI redText;
    public int seçiliInd;
    public Button onayBtn;
    public Button reddetBtn;
    public static Dictionary<Araba, DateTime> kiradakiler = new Dictionary<Araba, DateTime>{};

    Dictionary<string, string> konular = new Dictionary<string, string>
    {
        { "test", "Test Sürüşü Talebi" },
        { "kira", "Araç Kiralamak" },
        { "özel", "Özel Üretim Araç Teklifi" },
        { "test sonuç", "Test Sürüşü Sonucu" },
        { "çalışma", "Çalışmak" },
        { "araç kontrol", "Araç Kontrol Sonucu" },
        { "satılık", "Satılık Ev İlanı" }
    };

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
        Cursor.lockState = CursorLockMode.None;
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
        for (int i = mailler.Count - 1; i >= 0; i--) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content, false);

            Mail mail = mailler[i];
            string first30 = mail.mesaj.Length > 30 ? mail.mesaj.Substring(0, 30) : mail.mesaj;
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} {1}", ("<b>"+konular[mail.tür]+"</b> - "+first30+"...").PadRight(206, ' '), mail.tarih);

            Button buttonComponent = button.GetComponent<Button>();

            // Geçici bir değişken ile i'nin değerini al
            int currentIndex = i;

            buttonComponent.onClick.AddListener(() => OnButtonClick(mail, currentIndex));

            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }
    }

    public void OnButtonClick(Mail mail, int index)
    {
        mailSeçili = true;
        mailPanel.SetActive(true);
        seçiliInd = index;
        onayBtn.interactable = true;
        reddetBtn.interactable = true;

        if (mail.tür == "test" || mail.tür == "kira" || mail.tür == "özel") {
            onayText.text = "<b>Onayla</b>";
            if ((mail.tür == "test" || mail.tür == "kira") && !AraçAlımSatım.arabalar.Contains(mail.araba)) {
                onayBtn.interactable = false;
            }
            if (mail.tür == "özel" && AraçAlımSatım.para < mail.araba.gelişFiyat) {
                onayBtn.interactable = false;
            }
        } else if (mail.tür == "test sonuç" || mail.tür == "araç kontrol") {
            if (mailler[seçiliInd].degerler == null || mailler[seçiliInd].degerler[0] == 1) {
                onayText.text = "<b>Onayla</b>";
            } else {
                onayText.text = "<b>Tamam</b>";
            }
        } else if (mail.tür == "çalışma") {
            if (Çalışanlar.çalışanlar.Count == 3) {
                onayBtn.interactable = false;
            }
        } else if (mail.tür == "satılık") {
            onayText.text = "<b>Satın Al</b>";
            if (AraçAlımSatım.para < mail.degerler[0]) {
                onayBtn.interactable = false;
            }
        }

        if (mail.tür == "test" || mail.tür == "kira"|| mail.tür == "test sonuç") {
            mailMesaj.text = string.Format("<b>Tarih:</b> {0}\n<b>Konu:</b> {1}\n<b>Mesaj:</b>\n\nAraba Bilgileri\nModel Derece: {2}\nGeliş Fiyatı: {3}\nSene: {4}\n\n{5}",
            mail.tarih, konular[mail.tür], mail.araba.modelDerece, mail.araba.gelişFiyat, mail.araba.sene == -1 ? "Özel Üretim" : mail.araba.sene, mail.mesaj);
        } else if (mail.tür == "özel" || mail.tür == "çalışma" || mail.tür == "araç kontrol" || mail.tür == "satılık") {
            mailMesaj.text = string.Format("<b>Tarih:</b> {0}\n<b>Konu:</b> {1}\n<b>Mesaj:</b>\n\n{2}",
            mail.tarih, konular[mail.tür], mail.mesaj);
        }
        

        StartCoroutine(BekleVeRestart());
    }

    public void Back()
    {
        
        if (mailSeçili) {
            mailPanel.gameObject.SetActive(false);
            mailSeçili = false;
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartPanel() {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject); // Çocuk nesneyi yok et
        }

        ElemanlarıEkle();
    }

    IEnumerator BekleVeRestart()
    {
        while (mailSeçili)
        {
            yield return null; // Bir frame bekle
        }

        RestartPanel();
    }

    public void Reddet() {
        mailler.RemoveAt(seçiliInd);
        Back();
    }

    public void Temizle() {
        mailler.Clear();
        RestartPanel();
    }

    public void KabulEt() {
        switch (mailler[seçiliInd].tür) {
            case "test sonuç":
                if (mailler[seçiliInd].degerler[0] == 1) {
                    AraçAlımSatım.Sat(mailler[seçiliInd].araba, (int)(mailler[seçiliInd].araba.gelişFiyat * 1.1));
                }
                Reddet();
                break;
            case "test":
                int sayi = UnityEngine.Random.Range(0, 2);
                if (sayi == 0) {
                    Mailler.mailler.Add(new Mail("test sonuç", mailler[seçiliInd].araba, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
                        "Yukarıda bilgilerini vermiş olduğum araç ile gerçekleştirdiğim test sürüşünden gayet memnum kaldım. Ancak şu an bir araba alma niyetim yok, teşekkürler.", new List<int> {0}));
                } else {
                    Mailler.mailler.Add(new Mail("test sonuç", mailler[seçiliInd].araba, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
                        "Yukarıda bilgilerini vermiş olduğum araç ile gerçekleştirdiğim test sürüşünden gayet memnum kaldım. Size bu araç için <b>" + (mailler[seçiliInd].araba.gelişFiyat * 1.1) + " lira</b> teklif ediyorum.", new List<int> {1}));
                }

                Reddet();
                RestartPanel();

                break;
            case "kira":
                AraçAlımSatım.arabalar.Remove(mailler[seçiliInd].araba);
                kiradakiler.Add(mailler[seçiliInd].araba, mailler[seçiliInd].kiraBitiş);
                if (AraçAlımSatım.sergiler.Contains(mailler[seçiliInd].araba)) {
                    AraçAlımSatım.sergiler[AraçAlımSatım.sergiler.IndexOf(mailler[seçiliInd].araba)] = null;
                }
                AraçAlımSatım.para += mailler[seçiliInd].degerler[0];

                Reddet();
                break;
            case "özel":
                if (AraçAlımSatım.para >= mailler[seçiliInd].degerler[0] && AraçAlımSatım.arabalar.Count < AraçAlımSatım.galeriMax) {
                    PlayerPrefs.SetInt("simüle", 1);
                    PlayerPrefs.SetInt("model", mailler[seçiliInd].araba.modelDerece);
                    
                    AraçAlımSatım.para -= mailler[seçiliInd].degerler[0];
                    AraçAlımSatım.arabalar.Add(new Araba(mailler[seçiliInd].degerler[0], mailler[seçiliInd].araba.modelDerece, -1, mailler[seçiliInd].degerler[1], mailler[seçiliInd].degerler[2]));
                    AlışGeçmişi.geçmişAlımlar.Insert(0, string.Format(
                        "<b>ÖZEL</b>\nFiyat: {0:N0}\nModel Derecesi: {1}\nKâr Beklentisi: %{2}\nKâr Oranı: %{3}", 
                        mailler[seçiliInd].degerler[0],
                        mailler[seçiliInd].araba.modelDerece,
                        mailler[seçiliInd].degerler[1],
                        mailler[seçiliInd].degerler[2]
                    ));

                    AlışGeçmişi.geçmişAlımlarFiyat += mailler[seçiliInd].degerler[0];

                    Reddet();
                    Back();
                }
                break;
            case "çalışma":
                Çalışanlar.çalışanlar.Add(new Çalışan(mailler[seçiliInd].degerler[0], mailler[seçiliInd].isim));
                PlayerPrefs.SetInt("çalışan", 0);
                PlayerPrefs.SetInt("görev var", 0);
                Reddet();
                break;
            case "araç kontrol":
                Reddet();
                break;
            case "satılık":
                AraçAlımSatım.para -= mailler[seçiliInd].degerler[0];
                FirstPersonLook.evNo = mailler[seçiliInd].degerler[1];
                Reddet();
                break;
        }
    }
}

