using UnityEngine;
using System.Collections;
using TMPro;  // TextMeshPro için eklenmeli

public class AçıkArttırmaBro : MonoBehaviour
{
    Animator anim;
    public int id;
    public TMP_Text teklifText;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator IdleAnimasyonaDon()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        anim.SetTrigger("def");
        
    }

    IEnumerator Teklif()
    {
        if (AçıkArttırmaSatici.sonTeklif - AçıkArttırmaSatici.initTeklif < 50000) {
            AçıkArttırmaSatici.sonTeklif += 10000+UnityEngine.Random.Range(0, 10000);
            AçıkArttırmaSatici.benden = false;
            anim.SetTrigger("kaldır");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            yield return StartCoroutine(IdleAnimasyonaDon());
        }
        
    }

    void Update()
    {
        teklifText.text = $"Son Teklif: {AçıkArttırmaSatici.sonTeklif:N0}";
        if (AçıkArttırmaSatici.teklif && AçıkArttırmaSatici.i == id && AçıkArttırmaSatici.guncelTeklif < AçıkArttırmaSatici.teklifSayisi)
        {
            AçıkArttırmaSatici.teklif = false; // Teklif işlemi tetiklendiğinde sıfırla
            AçıkArttırmaSatici.i = -1;
            StartCoroutine(Teklif()); // Coroutine başlat
        }
    }
}
