using UnityEngine;
using System.Collections.Generic;

public class EvKonisi : MonoBehaviour
{
    Dictionary<int, Vector3> idKonumlari = new Dictionary<int, Vector3>()
    {
        { 0, new Vector3(-51.2095337f, 25.7588348f, -56.8456039f) },
        { 1, new Vector3(-65.7904663f, 25.7588348f, -56.8456039f) },
        { 2, new Vector3(-80.3713989f, 25.7588348f, -56.8456039f) },
        { 3, new Vector3(-94.9523315f, 25.7588348f, -56.8456039f) },
        { 4, new Vector3(-109.5332641f, 25.7588348f, -56.8456039f) },
        { 5, new Vector3(-124.1141967f, 25.7588348f, -56.8456039f) },
        { 6, new Vector3(-138.6951293f, 25.7588348f, -56.8456039f) },
        { 7, new Vector3(-153.2760619f, 25.7588348f, -56.8456039f) },
        { 8, new Vector3(-167.8569945f, 25.7588348f, -56.8456039f) },
        { 9, new Vector3(-182.4379271f, 25.7588348f, -56.8456039f) },
        { 10, new Vector3(-53.7904663f, 53.5401039f, -76.0500641f) },
        { 11, new Vector3(-75.7904663f, 53.5401039f, -76.0500641f) },
        { 12, new Vector3(-97.7904663f, 53.5401039f, -76.0500641f) },
        { 13, new Vector3(-119.7904663f, 53.5401039f, -76.0500641f) },
        { 14, new Vector3(-141.7904663f, 53.5401039f, -76.0500641f) },
        { 15, new Vector3(-163.7904663f, 53.5401039f, -76.0500641f) },
        { 16, new Vector3(-185.7904663f, 53.5401039f, -76.0500641f) },
        { 17, new Vector3(-66.7904663f, 155.082764f, -124.949944f) },
        { 18, new Vector3(-100.790474f, 155.082764f, -124.949944f) },
        { 19, new Vector3(-135.790466f, 155.082764f, -124.949944f) },
        { 20, new Vector3(-163.209534f, 155.082764f, -124.949944f) },
        { 21, new Vector3(-84.2095337f, 189.540115f, -241.949951f) },
        { 22, new Vector3(-139.209534f, 189.540115f, -206.050064f) },
        { 23, new Vector3(5.79046631f, 90.5401077f, -161.050064f) },
        { 24, new Vector3(5.79046631f, 90.5401077f, -116.050064f) },
        { 25, new Vector3(5.79046631f, 90.5401077f, -71.0500641f) },
        { 26, new Vector3(46.2095375f, 90.5401077f, -71.0500641f) },
        { 27, new Vector3(87.2095337f, 90.5401077f, -71.0500641f) },
        { 28, new Vector3(-14.2095337f, 14.0827599f, 5.05006027f) },//-----
        { 29, new Vector3(14.5404663f, 14.0827599f, 5.05006027f) },
        { 30, new Vector3(43.2904663f, 14.0827599f, 5.05006027f) },
        { 31, new Vector3(72.0404663f, 14.0827599f, 5.05006027f) },
        { 32, new Vector3(100.7904663f, 14.0827599f, 5.05006027f) },
        { 33, new Vector3(129.5404663f, 14.0827599f, 5.05006027f) },
        { 34, new Vector3(158.2904663f, 14.0827599f, 5.05006027f) },
        { 35, new Vector3(187.0404663f, 14.0827599f, 5.05006027f) },
        { 36, new Vector3(215.790466f, 14.0827599f, 5.05006027f) },
        { 37, new Vector3(-14.2095337f, 14.0827599f, 49.0500641f) },//-----
        { 38, new Vector3(14.5404663f, 14.0827599f, 49.0500641f) },
        { 39, new Vector3(43.2904663f, 14.0827599f, 49.0500641f) },
        { 40, new Vector3(72.0404663f, 14.0827599f, 49.0500641f) },
        { 41, new Vector3(100.7904663f, 14.0827599f, 49.0500641f) },
        { 42, new Vector3(129.5404663f, 14.0827599f, 49.0500641f) },
        { 43, new Vector3(158.2904663f, 14.0827599f, 49.0500641f) },
        { 44, new Vector3(187.0404663f, 14.0827599f, 49.0500641f) }
    };
    float zaman = 0f;
    bool yukariCik = true;
    void Update()
    {
        Vector3 defaultPos = idKonumlari[FirstPersonLook.evNo];

        // zaman'ı sürekli arttırıyoruz.
        zaman += Time.deltaTime * 2f;

        if (yukariCik)
        {
            // Yukarı çıkış: defaultPos'dan y ekseninde +1 olan hedefe.
            transform.position = Vector3.Lerp(defaultPos, new Vector3(defaultPos.x, defaultPos.y + 1, defaultPos.z), zaman);

            // Eğer zaman 1 veya daha büyükse, animasyon tamamlanmış demektir.
            if (zaman >= 1f)
            {
                zaman = 0f;  // zamanı sıfırla
                yukariCik = false;  // şimdi inme zamanı
            }
        }
        else
        {
            // İnme: yukarı çıkış sonrası hedef pozisyonundan (defaultPos.y+1) tekrar defaultPos'a.
            transform.position = Vector3.Lerp(new Vector3(defaultPos.x, defaultPos.y + 1, defaultPos.z), defaultPos, zaman);

            if (zaman >= 1f)
            {
                zaman = 0f;  // zamanı sıfırla
                yukariCik = true;  // animasyonu tekrar başlatmak için
            }
        }
    }
}
