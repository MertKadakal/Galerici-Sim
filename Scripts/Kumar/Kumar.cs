using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kumar : MonoBehaviour
{
    public static List<int> renkler = new List<int> { -1, -1, -1 };
    public TMP_Text ayni;
    public TMP_Text farkli;
    public TMP_Text ikisi;
    public TMP_Text yatirilan_text;
    public TMP_Text para;
    public Slider slider;
    float yatirilan;
    float farkli_oran;
    float ayni_oran;
    float ikisi_oran;

    void Start() {
        slider.value = 5000;
        yatirilan = slider.value;
        OranlariGuncelle();
    }

    void Update() {
        para.text = $"Para: {AraçAlımSatım.para:N0}";
        yatirilan = slider.value;
        ayni.text = $"<b>Hepsi Aynı:</b> <i>{ayni_oran:F2}</i>\n{(int)(yatirilan * ayni_oran):N0} lira";
        farkli.text = $"<b>Hepsi Farklı:</b> <i>{farkli_oran:F2}</i>\n{(int)(yatirilan * farkli_oran):N0} lira";
        ikisi.text = $"<b>Sadece İkisi Aynı:</b> <i>{ikisi_oran:F2}</i>\n{(int)(yatirilan * ikisi_oran):N0} lira";
        yatirilan_text.text = $"{yatirilan:N0} lira yatırılacak";

        if (renkler[0] != -1 && renkler[1] != -1 && renkler[2] != -1) {
            AraçAlımSatım.para -= (int)slider.value;

            List<int> degerler = new List<int>{renkler[0]};
            if (renkler[1] != renkler[0]) {degerler.Add(renkler[1]);}
            if (!degerler.Contains(renkler[2])) {degerler.Add(renkler[2]);}

            if (degerler.Count == 1) {Debug.Log("üçü aynı"); AraçAlımSatım.para += (int)(slider.value * ayni_oran);}
            if (degerler.Count == 2) {Debug.Log("ikisi aynı"); AraçAlımSatım.para += (int)(slider.value * ikisi_oran);}
            if (degerler.Count == 3) {Debug.Log("üçü farklı"); AraçAlımSatım.para += (int)(slider.value * farkli_oran);}

            renkler[0] = -1;
            renkler[1] = -1;
            renkler[2] = -1;

            OranlariGuncelle();
        }
    }

    void OranlariGuncelle() {
        farkli_oran = UnityEngine.Random.Range(5f, 10f)/10f;
        ayni_oran = UnityEngine.Random.Range(20f, 30f)/10f;
        ikisi_oran = UnityEngine.Random.Range(11f, 20f)/10f;
    }

    public void Back() {
        SceneManager.LoadScene(0);
    }
}
