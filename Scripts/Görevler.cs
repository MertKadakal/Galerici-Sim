using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class Görevler : MonoBehaviour {
    public RectTransform content;
    public GameObject buttonPrefab;

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

    List<string> görevler = new List<string>{
        "Garaja göz at"
    };

    void ElemanlarıEkle() {
        for (int i = 0; i < AraçAlımSatım.sergiler.Count; i++) {
            if (AraçAlımSatım.sergiler[i].gelişFiyat != 0) {
                GameObject button = Instantiate(buttonPrefab);
                button.transform.SetParent(content, false);

                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                button.GetComponentInChildren<TextMeshProUGUI>().text = $"{i+1} nolu platformdaki aracı kontrol et";

                Button buttonComponent = button.GetComponent<Button>();

                // Geçici bir değişken ile i'nin değerini al
                int currentIndex = i;

                buttonComponent.onClick.AddListener(() => OnButtonClick(button.GetComponentInChildren<TextMeshProUGUI>().text));
            }
        }

        for (int i = 0; i < görevler.Count; i++) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content, false);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = görevler[i];

            Button buttonComponent = button.GetComponent<Button>();

            buttonComponent.onClick.AddListener(() => OnButtonClick(button.GetComponentInChildren<TextMeshProUGUI>().text));
        }
    }

    public void OnButtonClick(string görev)
    {
        PlayerPrefs.SetInt("görev var", 1);
        if (görev.Split(" ")[^3] == "aracı") {
            PlayerPrefs.SetInt("görev no", int.Parse(görev.Split(" ")[0]));
        } else if (görev == "Garaja göz at") {
            PlayerPrefs.SetInt("görev no", 7);
        }
    }

    public void Kov() {
        Çalışanlar.çalışanlar.RemoveAt(PlayerPrefs.GetInt("çalışan")-1);
        PlayerPrefs.SetInt("görev var", 0);
        PlayerPrefs.SetInt("çalışan", 0);
    }
}