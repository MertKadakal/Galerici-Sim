
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;
using System;
[System.Serializable]

public class AlışGeçmişi : MonoBehaviour {
    public static List<string> geçmişAlımlar = new List<string>();
    public static int geçmişAlımlarFiyat;
    public TextMeshProUGUI totalGider;
    public GameObject buttonPrefab;

    //ScrollView bileşeninin olduğu obje
    [SerializeField]
    public RectTransform content;
    public RectTransform scrollParent;
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
        foreach (string alış in geçmişAlımlar) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content, false);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = alış;
        }

        totalGider.text = string.Format("<b>Toplam Gider:</b> {0:N0}", geçmişAlımlarFiyat);
    }
    
    
    
    public void Back() {
        SceneManager.LoadScene(2);
    }
}