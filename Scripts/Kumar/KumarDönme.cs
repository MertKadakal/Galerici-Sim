using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KumarDonme : MonoBehaviour
{
    float rotationSpeed;
    float duration;
    public int id;
    private bool isRotating = false; // Yeni flag
    public Button cevir;
    public Slider slider;

    void Start() {
        rotationSpeed = UnityEngine.Random.Range(500f, 1000f);
        duration = UnityEngine.Random.Range(1f, 4f);
    }

    void Update()
    {
        cevir.interactable = !isRotating && slider.value <= AraçAlımSatım.para;
        slider.interactable = !isRotating;
    }

    IEnumerator RotateForTime()
    {
        isRotating = true; // Dönüş başlatıldı
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Rastgele açılar ve dönüş
        List<Vector3> angles = new List<Vector3> {new Vector3(340,0,90), new Vector3(295,0,90), new Vector3(290,180,270), new Vector3(335,180,270)};
        int son = UnityEngine.Random.Range(0, 4); // 0-3 arası rastgele bir indeks alır
        transform.rotation = Quaternion.Euler(angles[son]); 
        Kumar.renkler[id] = son;
        isRotating = false; // Dönüş tamamlandı
    }

    public void Dondur() {
        StartCoroutine(RotateForTime());
    }
}
