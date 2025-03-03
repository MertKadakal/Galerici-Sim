using UnityEngine;
using System.Collections;
using System;

public class AnimasyonOynat : MonoBehaviour
{
    public int id;
    Animator anim;
    Rigidbody rb;
    public float hareketHizi = 2f; 
    public float donusHizi = 5f; 
    private int mevcutHedefIndex = 0;
    private bool hareketEdiyor = false;
    private float sabitY; // Y konumunu saklamak için değişken
    private BoxCollider bc;
    public ÇalışanRotalar saklayıcı;
    public GameObject çalışanKonum;
    public GameObject çalışanKonum1;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("yuru", false);
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        sabitY = transform.position.y; // Nesnenin başlangıçtaki Y konumunu al
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("görev var") == 1 && PlayerPrefs.GetInt("çalışan") == id) // Space tuşu ile başlat/durdur
        {
            Debug.Log("aa");
            hareketEdiyor = !hareketEdiyor;
            anim.SetBool("yuru", hareketEdiyor);
            anim.SetBool("otur", false);
            if (hareketEdiyor) StartCoroutine(HareketEt());
            mevcutHedefIndex = 0;
            PlayerPrefs.SetInt("görev var", 0);
            PlayerPrefs.SetInt("çalışan", 0);
        }
    }

    IEnumerator HareketEt()
    {
        int görevno = PlayerPrefs.GetInt("görev no");
        if (görevno <= 7) {
            while (hareketEdiyor && mevcutHedefIndex < saklayıcı.rotalar[görevno-1].Length)
            {
                bc.size = new Vector3(3.7244072f,11.60206f,1f);

                GameObject hedef = saklayıcı.rotalar[görevno-1][mevcutHedefIndex];

                while (Vector3.Distance(transform.position, hedef.transform.position) > 0.5f)
                {
                    // Yumuşak dönüş
                    Vector3 hedefYon = (hedef.transform.position - transform.position).normalized;
                    Quaternion hedefRotation = Quaternion.LookRotation(hedefYon);
                    transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotation, donusHizi * Time.deltaTime);

                    // Hareket ederken y konumunu sabit tut
                    Vector3 yeniPozisyon = transform.position + transform.forward * hareketHizi * Time.deltaTime;
                    yeniPozisyon.y = sabitY; // Y eksenini sabit tut

                    rb.MovePosition(yeniPozisyon);

                    yield return null;
                }

                // Bir sonraki hedefe geç
                mevcutHedefIndex++;
            }

            anim.SetBool("yuru", false);
            hareketEdiyor = false;
            yield return new WaitForSeconds(1f);
            anim.SetBool("yuru", true);
            hareketEdiyor = true;
            mevcutHedefIndex = saklayıcı.rotalar[görevno-1].Length-1;

            //----------------------------------------
            //kontrol sonrası mail
            if (görevno <= 6) {
                string öneri = "";
                int platformdakiFiyt = AraçAlımSatım.sergiler[görevno-1].gelişFiyat;
                foreach (Araba araba in AraçAlımSatım.arabalar) {
                    if (araba.gelişFiyat > platformdakiFiyt) {
                        öneri = " Ayrıca araç stoğunuzda bu araçtan daha yüksek fiyata sahip araçlar olduğunu gördüm. Bu araçları sergilemek gelirinizi arttırmak için yardımcı olabilir.";
                    }
                }

                int rnd = UnityEngine.Random.Range(0, 2);
                int hasar = UnityEngine.Random.Range(5, 15);
                if (rnd == 1) {
                    Mailler.mailler.Add(new Mail("araç kontrol", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
                    $"Ben çalışanınız {Çalışanlar.çalışanlar[id-1].isim}, {görevno} numaralı platformdaki aracın kontrolü sonucunda birtakım aksaklıklar olduğunu gördüm ve gerekli tamiri gerçekleştirdim."+
                    $" Hasar kaydı eklendiği için aracın fiyatında {AraçAlımSatım.sergiler[görevno-1].gelişFiyat*hasar/100:N0} lira düşme oldu ({AraçAlımSatım.sergiler[görevno-1].gelişFiyat:N0} lira → {AraçAlımSatım.sergiler[görevno-1].gelişFiyat*(100-hasar)/100:N0} lira)."+öneri));

                    AraçAlımSatım.sergiler[görevno-1].gelişFiyat = (int)(AraçAlımSatım.sergiler[görevno-1].gelişFiyat * 0.9);
                } else {
                    Mailler.mailler.Add(new Mail("araç kontrol", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), 
                    $"Ben çalışanınız {Çalışanlar.çalışanlar[id-1].isim}, {görevno} numaralı platformdaki aracın kontrolü sonucunda aracın temiz olduğunu aktarıyorum."+öneri));
                }
            }
            //----------------------------------------

            while (hareketEdiyor && mevcutHedefIndex >= 0)
            {
                bc.size = new Vector3(3.7244072f,11.60206f,1f);

                GameObject hedef = saklayıcı.rotalar[görevno-1][mevcutHedefIndex];

                while (Vector3.Distance(transform.position, hedef.transform.position) > 0.5f)
                {
                    // Yumuşak dönüş
                    Vector3 hedefYon = (hedef.transform.position - transform.position).normalized;
                    Quaternion hedefRotation = Quaternion.LookRotation(hedefYon);
                    transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotation, donusHizi * Time.deltaTime);

                    // Hareket ederken y konumunu sabit tut
                    Vector3 yeniPozisyon = transform.position + transform.forward * hareketHizi * Time.deltaTime;
                    yeniPozisyon.y = sabitY; // Y eksenini sabit tut

                    rb.MovePosition(yeniPozisyon);

                    yield return null;
                }

                // Bir sonraki hedefe geç
                mevcutHedefIndex--;
            }

            //en son çalışan kendi yerine gider
            while (Vector3.Distance(transform.position, çalışanKonum.transform.position) > 0.5f)
            {
                // Yumuşak dönüş
                Vector3 hedefYon = (çalışanKonum.transform.position - transform.position).normalized;
                Quaternion hedefRotation = Quaternion.LookRotation(hedefYon);
                transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotation, donusHizi * Time.deltaTime);

                // Hareket ederken y konumunu sabit tut
                Vector3 yeniPozisyon = transform.position + transform.forward * hareketHizi * Time.deltaTime;
                yeniPozisyon.y = sabitY; // Y eksenini sabit tut

                rb.MovePosition(yeniPozisyon);

                yield return null;
            }
            while (Vector3.Distance(transform.position, çalışanKonum1.transform.position) > 0.5f)
            {
                // Yumuşak dönüş
                Vector3 hedefYon = (çalışanKonum1.transform.position - transform.position).normalized;
                Quaternion hedefRotation = Quaternion.LookRotation(hedefYon);
                transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotation, donusHizi * Time.deltaTime);

                // Hareket ederken y konumunu sabit tut
                Vector3 yeniPozisyon = transform.position + transform.forward * hareketHizi * Time.deltaTime;
                yeniPozisyon.y = sabitY; // Y eksenini sabit tut

                rb.MovePosition(yeniPozisyon);

                yield return null;
            }


            anim.SetBool("yuru", false);
            hareketEdiyor = false;

            
        }
    }
    
}
