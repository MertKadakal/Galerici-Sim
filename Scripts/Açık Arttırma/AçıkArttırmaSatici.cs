using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;  // TextMeshPro için eklenmeli

public class AçıkArttırmaSatici : MonoBehaviour
{
    Animator anim;
    public static int model;
    public static bool teklif = false;
    public static int i;
    public static double sonTeklif;
    public static double initTeklif;
    List<double> baslangiclar = new List<double>{100000,150000,300000,180000,120000,500000,90000};
    public static int teklifSayisi;
    public static int guncelTeklif = 0;
    public GameObject panel;
    public TMP_Text finalText;
    public TMP_Text infoText;
    public TMP_Text para;
    public static bool benden = false;
    Araba araba;
    int sene;
    int karBeklentisi;
    int karOranı;
    public GameObject anaCam;
    public GameObject cam2;
    public GameObject teklifSunPanel;
    
    void Update() {
        para.text = $"Para: {AraçAlımSatım.para:N0}";
    }
    
    void Start()
    {
        anaCam.SetActive(true);
        teklifSunPanel.SetActive(true);
        cam2.SetActive(false);

        teklifSayisi = UnityEngine.Random.Range(3, 7);
        model = UnityEngine.Random.Range(1, 8);
        sonTeklif = baslangiclar[model-1]*(UnityEngine.Random.Range(11, 16)/10.0);

        //arabayı oluştur
        System.Random random = new System.Random();

        int min, max;

        min = AraçPazarı.seneAraliklari[model][0];
        max = AraçPazarı.seneAraliklari[model][1];
        sene = random.Next(min, max+1);

        min = AraçPazarı.karBeklentileri[model][0];
        max = AraçPazarı.karBeklentileri[model][1];
        karBeklentisi = random.Next(min, max+1);

        min = AraçPazarı.karBeklentileri[model][0];
        max = AraçPazarı.karBeklentileri[model][1];
        karOranı = random.Next(min, max+1);

        infoText.text = $"Model Derece: {model}\nSene: {sene}\nKâr Beklentisi: %{karBeklentisi}\nKâr Oranı: %{karOranı}";
                
        anim = GetComponent<Animator>();
        
        initTeklif = sonTeklif;
        StartCoroutine(TeklifVermeSüreci());
    }

    IEnumerator IdleAnimasyonaDon()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        i = UnityEngine.Random.Range(0, 16);
        teklif = true;
        anaCam.SetActive(true);
        cam2.SetActive(false);
        teklifSunPanel.SetActive(true);
        anim.SetTrigger("def");
    }

    IEnumerator TeklifVermeSüreci()
    {
        while ((guncelTeklif < teklifSayisi) && (sonTeklif - initTeklif < 50000))
        {
            float bekleme = UnityEngine.Random.Range(5f, 10f);
            yield return new WaitForSeconds(bekleme);
            anaCam.SetActive(false);
            cam2.SetActive(true);
            teklifSunPanel.SetActive(false);
            anim.SetTrigger("say");
            guncelTeklif++;
            yield return StartCoroutine(IdleAnimasyonaDon());
        }

        panel.SetActive(true);
        if (benden) {
            if (sonTeklif > AraçAlımSatım.para) {
                finalText.text = "Yeterli paranız olmadığı için araç sizden önceki en yüksek teklifi veren alıcıya satıldı";
            } else {
                finalText.text = $"Tebrikler! Aracı {sonTeklif:N0} liraya satın aldınız";

                araba = new Araba((int)sonTeklif, model, sene, karBeklentisi, karOranı);

                AraçAlımSatım.arabalar.Add(araba);
                AraçAlımSatım.para -= (int)sonTeklif;

                AlışGeçmişi.geçmişAlımlar.Add(string.Format(
                    "<b>AÇIK ARTTIRMA</b>\n<b>Fiyat:</b> {0:N0}\n<b>Model Derecesi:</b> {1}\n<b>Sene:</b> {2}\n<b>Kâr Beklentisi:</b> %{3}\n<b>Kâr Oranı:</b> %{4}", 
                    araba.gelişFiyat,
                    araba.modelDerece,
                    araba.sene,
                    araba.karBeklentisi,
                    araba.karOrani
                ));
            }
        } else {
            finalText.text = $"Maalesef araç {sonTeklif:N0} liraya başkasına satıldı";
        }
        
    }

    public void Back() {
        SceneManager.LoadScene(0);
    }

    public void TeklifYap10() {
        sonTeklif += 10000;
        benden = true;
    }
    public void TeklifYap20() {
        sonTeklif += 20000;
        benden = true;
    }
    public void TeklifYap30() {
        sonTeklif += 30000;
        benden = true;
    }
    public void TeklifYap40() {
        sonTeklif += 40000;
        benden = true;
    }

    public void TeklifYap50() {
        sonTeklif += 50000;
        benden = true;
    }
}
