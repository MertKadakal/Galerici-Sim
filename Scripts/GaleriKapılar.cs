using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Vector3 kapaliKonum;        // Kapının kapalı olduğu konum
    public Vector3 acikKonum;          // Kapının açık olduğu konum
    public float hiz = 2f;             // Hareket hızı
    public float mesafeEsik = 3f;      // Oyuncunun kapı ile olan mesafe eşik değeri
    public float mesafeEsikCalisan = 2f;      // Oyuncunun kapı ile olan mesafe eşik değeri
    public GameObject player;          // Oyuncu referansı
    public GameObject calisan1;
    public GameObject calisan2;
    public GameObject calisan3;
    public GameObject kamyon;

    public float minX;
    public float maxX;
    public bool isHorizontal;

    private bool isMoving = false;     // Kapı hareket halinde mi?
    private bool doorOpen = false;     // Kapı açık mı?

    float zFark;
    float zFark1;
    float zFark2;
    float zFark3;
    float zFark4;

    void Start()
    {
        // Kapı başlangıçta kapalı konumda olsun.
        transform.position = kapaliKonum;
    }

    void Update()
    {
        // Oyuncu ile kapı arasındaki mesafeyi hesapla.
        if (isHorizontal) {
            zFark = Mathf.Abs(player.transform.position.z - transform.position.z);
            zFark1 = calisan1.activeSelf ? Mathf.Abs(calisan1.transform.position.z - transform.position.z) : 5f;
            zFark2 = calisan2.activeSelf ? Mathf.Abs(calisan2.transform.position.z - transform.position.z) : 5f;
            zFark3 = calisan3.activeSelf ? Mathf.Abs(calisan3.transform.position.z - transform.position.z) : 5f;
            zFark4 = kamyon.activeSelf ? Mathf.Abs(kamyon.transform.position.z - transform.position.z) : 10f;
        } else {
            zFark = Mathf.Abs(player.transform.position.x - transform.position.x);
            zFark1 = calisan1.activeSelf ? Mathf.Abs(calisan1.transform.position.x - transform.position.x) : 5f;
            zFark2 = calisan2.activeSelf ? Mathf.Abs(calisan2.transform.position.x - transform.position.x) : 5f;
            zFark4 = kamyon.activeSelf ? Mathf.Abs(kamyon.transform.position.x - transform.position.x) : 10f;
        }

/*
        if (gameObject.name == "garajkapı") {
            Debug.Log($"{!isMoving}  {doorOpen}  {zFark >= mesafeEsik}  {zFark1 >= mesafeEsikCalisan} {zFark2 >= mesafeEsikCalisan}  {zFark3 >= mesafeEsikCalisan}  {zFark4 >= 5f}");
        }
*/
        // Eğer kapı kapalı ve oyuncu eşik mesafesi içerisindeyse, kapıyı aç.
        if (!isMoving && !doorOpen && (zFark < mesafeEsik || zFark1 < mesafeEsikCalisan || zFark2 < mesafeEsikCalisan || zFark3 < mesafeEsikCalisan || zFark4 < 5f) && ((player.transform.position.x > minX && player.transform.position.x < maxX && isHorizontal) || (player.transform.position.z > minX && player.transform.position.z < maxX && !isHorizontal) ||
                                                                                                                                                                        (calisan1.transform.position.x > minX && calisan1.transform.position.x < maxX && !isHorizontal) ||
                                                                                                                                                                        (calisan2.transform.position.x > minX && calisan2.transform.position.x < maxX && !isHorizontal) ||
                                                                                                                                                                        (calisan3.transform.position.x > minX && calisan3.transform.position.x < maxX && !isHorizontal) ||
                                                                                                                                                                        (kamyon.transform.position.x > minX && kamyon.transform.position.x < maxX && isHorizontal)))
        {
            StartCoroutine(OpenDoor());
        }
        // Eğer kapı açıksa ve oyuncu eşik mesafesinin dışındaysa, kapıyı kapa
        else if (!isMoving && doorOpen && zFark >= mesafeEsik && zFark1 >= mesafeEsikCalisan && zFark2 >= mesafeEsikCalisan && zFark3 >= mesafeEsikCalisan && zFark4 >= 5f)
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isMoving = true;
        float zaman = 0f;
        Vector3 baslangicPoz = transform.position;
        // Kapıyı kapalı konumdan açık konuma doğru hareket ettir.
        while (zaman < 1f)
        {
            zaman += Time.deltaTime * hiz;
            transform.position = Vector3.Lerp(baslangicPoz, acikKonum, zaman);
            yield return null;
        }
        doorOpen = true;
        isMoving = false;
    }

    IEnumerator CloseDoor()
    {
        isMoving = true;
        float zaman = 0f;
        Vector3 baslangicPoz = transform.position;
        // Kapıyı açık konumdan kapalı konuma doğru hareket ettir.
        while (zaman < 1f)
        {
            zaman += Time.deltaTime * hiz;
            transform.position = Vector3.Lerp(baslangicPoz, kapaliKonum, zaman);
            yield return null;
        }
        doorOpen = false;
        isMoving = false;
    }
}
